using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NHibernate.Proxy;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers
{
    public class NHibernateContractResolver : DefaultContractResolver
    {
        private readonly string[] _exceptMemberName;//例外
        private readonly string[] _excludeMemberName;//排除

        private static readonly MemberInfo[] NHibernateProxyInterfaceMembers = typeof(INHibernateProxy).GetMembers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptMemberName">原本下级全部不取出来 例外的属性</param>
        /// <param name="excludeMemberName">排除的属性 比如一些富文本的字段</param>
        public NHibernateContractResolver(string[] exceptMemberName = null, string[] excludeMemberName = null)
        {
            this._exceptMemberName = exceptMemberName;
            this._excludeMemberName = excludeMemberName;
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            if (typeof(NHibernate.Proxy.INHibernateProxy).IsAssignableFrom(objectType))
                return base.CreateContract(objectType.BaseType);

            return base.CreateContract(objectType);
        }


        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var members = new List<PropertyInfo>(objectType.GetProperties());
            members.RemoveAll(memberInfo =>
                // (IsMemberNames(memberInfo))||
                (IsMemberPartOfNHibernateProxyInterface(memberInfo)) ||
                (IsMemberDynamicProxyMixin(memberInfo)) ||
                (IsMemberMarkedWithIgnoreAttribute(memberInfo, objectType)) ||
                (IsInheritedISet(memberInfo)) ||
                (IsInheritedEntity(memberInfo)) ||
               (IsExcludeMemberName(memberInfo))
                );

            return (from memberInfo in members
                    where memberInfo.DeclaringType != null && memberInfo.DeclaringType.BaseType != null
                    let infos = memberInfo.DeclaringType.BaseType.GetMember(memberInfo.Name)
                    select infos.Length == 0 ?
                    memberInfo :
                    infos[0]).ToList();
        }
        private static bool IsMemberDynamicProxyMixin(PropertyInfo memberInfo)
        {
            return memberInfo.Name == "__interceptors";
        }

        private static bool IsMemberPartOfNHibernateProxyInterface(PropertyInfo memberInfo)
        {
            return Array.Exists(NHibernateProxyInterfaceMembers, mi => memberInfo.Name == mi.Name);
        }

        private static bool IsMemberMarkedWithIgnoreAttribute(PropertyInfo memberInfo, Type objectType)
        {

            var infos = typeof(INHibernateProxy).IsAssignableFrom(objectType) ?
                objectType.BaseType.GetMember(memberInfo.Name) :
                objectType.GetMember(memberInfo.Name);
            return infos[0].GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Length > 0;

        }

        private bool IsExceptMember(PropertyInfo memberInfo)
        {
            if (_exceptMemberName == null)
                return false;
            return Array.Exists(_exceptMemberName, i => memberInfo.Name == i);
        }

        private bool IsExcludeMemberName(PropertyInfo memberInfo)
        {
            if (_excludeMemberName == null)
                return false;
            return Array.Exists(_excludeMemberName, i => memberInfo.Name == i);
        }

        private bool IsInheritedISet(PropertyInfo memberInfo)
        {
            return (memberInfo.PropertyType.Name == "ISet`1" && !IsExceptMember(memberInfo));
        }


        private bool IsInheritedEntity(PropertyInfo memberInfo)
        {
            return new[] { "Entity", "Entity`1" }.Contains(FindBaseType(memberInfo.PropertyType).Name) && !IsExceptMember(memberInfo);
        }
        private static Type FindBaseType(Type type)
        {
            if (!type.IsClass)
                return type;
            if (type.Name == "Entity" || type.Name == "Object" || type.Name == "Entity`1")
            {
                return type;
            }
            return FindBaseType(type.BaseType);
        }
    }
}
