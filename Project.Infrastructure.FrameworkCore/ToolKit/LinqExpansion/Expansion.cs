using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion
{
    public static class Expansion
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertibleValue"></param>
        /// <returns></returns>
        public static T ConvertTo<T>(this IConvertible convertibleValue)
        {
            if (convertibleValue == null || ReferenceEquals(convertibleValue, ""))
                return default(T);
            if (!typeof(T).IsGenericType)
                return (T)Convert.ChangeType(convertibleValue, typeof(T));
            Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(Nullable<>))
            {
                return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));
        }

        /// <summary>
        /// 循环
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T obj in collection)
            {
                action(obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="keyExtractor"></param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> list, T item, Func<T, object> keyExtractor)
        {
            return list.Contains(item, new KeyEqualityComparer<T>(keyExtractor));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyExtractor"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, object> keyExtractor)
        {
            return list.Distinct(new KeyEqualityComparer<T>(keyExtractor));
        }


		public static IQueryable<T> Sort<T>(this IQueryable<T> query,
											 string sortField,
											 SortDirection direction) {
			if (direction==SortDirection.Ascending)
				return query.OrderBy(s => s.GetType()
										   .GetProperty(sortField));
			return query.OrderByDescending(s => s.GetType()
												 .GetProperty(sortField));

		} 
    }
}
