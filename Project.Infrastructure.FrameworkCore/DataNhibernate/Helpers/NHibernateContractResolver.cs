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

        private static readonly MemberInfo[] NHibernateProxyInterfaceMembers = typeof(INHibernateProxy).GetMembers();

        public NHibernateContractResolver(string[] exceptMemberName=null)
        {
            this._exceptMemberName = exceptMemberName;
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
                (IsInheritedEntity(memberInfo))
                );

            return (from memberInfo in members 
                    where  memberInfo.DeclaringType != null && memberInfo.DeclaringType.BaseType != null
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
