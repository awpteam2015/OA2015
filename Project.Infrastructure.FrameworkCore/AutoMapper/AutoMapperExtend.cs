using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Project.Infrastructure.FrameworkCore.AutoMapper
{
    public static class AutoMapperExtend
    {
        /// <summary>
        /// 忽略空值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNull<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            expression.ForAllMembers(o => o.Condition(c => c.PropertyMap.SourceMember != null && !c.IsSourceValueNull));
            return expression;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="expression"></param>
        /// <param name="exceptMemberName">例外的字段</param>
        /// <param name="excludeMemberName">排除的属性</param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
 (this IMappingExpression<TSource, TDestination> expression, string[] exceptMemberName = null, string[] excludeMemberName = null)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var destinationProperties = typeof(TDestination).GetProperties(flags);
            foreach (var property in destinationProperties)
            {
                if (exceptMemberName != null && exceptMemberName.Contains(property.Name))
                {
                    continue;
                }

                //if (excludeMemberName != null && excludeMemberName.Contains(property.Name))
                //{
                //    expression.ForMember(property.Name, opt => opt.Ignore());
                //}

                //object[] keys = property.GetCustomAttributes(typeof(PkidAttribute), true);
                //if (keys.Length == 1 && !isIncludePkid)
                //{
                //    expression.ForMember(property.Name, opt => opt.Ignore());
                //    continue;
                //}

                if ((new[] { "Entity", "Entity`1" }.Contains(FindBaseType(property.PropertyType).Name)
                  || property.PropertyType.Name == "ISet`1"
                   || property.Name == "Id"
                   || property.Name == "Version")
                  )
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
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


    //   public static class AutoMapperExtend2
    //   {
    //       public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
    //(this IMappingExpression<TSource, TDestination> expression)
    //       {
    //           var flags = BindingFlags.Public | BindingFlags.Instance;
    //           var destinationProperties = typeof(TDestination).GetProperties(flags);
    //           foreach (var property in destinationProperties)
    //           {
    //               if (new[] { "Entity", "Entity`1" }.Contains(FindBaseType(property.PropertyType).Name)
    //                   || property.PropertyType.Name == "ISet`1"
    //                    || property.Name == "Id"
    //                    || property.Name == "Version"
    //                   )
    //               {
    //                   expression.ForMember(property.Name, opt => opt.Ignore());
    //               }
    //           }
    //           return expression;
    //       }

    //       private static Type FindBaseType(Type type)
    //       {
    //           if (!type.IsClass)
    //               return type;
    //           if (type.Name == "Entity" || type.Name == "Object" || type.Name == "Entity`1")
    //           {
    //               return type;
    //           }
    //           return FindBaseType(type.BaseType);
    //       }

    //       //好像无效
    //       private static void IgnoreDtoIdAndVersionPropertyToEntity()
    //       {

    //           foreach (TypeMap map in Mapper.GetAllTypeMaps())
    //           {   PropertyInfo memberInfo = map.DestinationProperty.MemberInfo as PropertyInfo;
    //               foreach (var property in map.)
    //               {
    //                   if (new[] { "Entity", "Entity`1" }.Contains(FindBaseType(property.PropertyType).Name)
    //                       || property.PropertyType.Name == "ISet`1"
    //                        || property.Name == "Id"
    //                        || property.Name == "Version"
    //                       )
    //                   {
    //                       expression.ForMember(property.Name, opt => opt.Ignore());
    //                   }
    //               }
    //               map.GetExistingPropertyMapFor(new PropertyAccessor(idProperty)).Ignore();

    //           }
    //       }

    //好像无效
    //private static void IgnoreDtoIdAndVersionPropertyToEntity()
    //{
    //    PropertyInfo idProperty = typeof(Entity).GetProperty("Id");
    //    PropertyInfo versionProperty = typeof(Entity).GetProperty("Version");
    //    foreach (TypeMap map in Mapper.GetAllTypeMaps())
    //    {
    //        if (typeof(Entity).IsAssignableFrom(map.SourceType)
    //            && typeof(Entity).IsAssignableFrom(map.DestinationType))
    //        {
    //            map.FindOrCreatePropertyMapFor(new PropertyAccessor(idProperty)).Ignore();
    //            map.FindOrCreatePropertyMapFor(new PropertyAccessor(versionProperty)).Ignore();
    //        }
    //    }
    //}
    //   }

}
