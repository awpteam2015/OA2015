using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// 枚举处理
    /// </summary>
    public static class EnumHelper
    {

        //是否存在
        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }
        //判断是否相等
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }
        //添加
        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "不能添加类型 '{0}'",
                        typeof(T).Name
                        ), ex);
            }
        }

        //移除
        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "不能移除类型 '{0}'",
                        typeof(T).Name
                        ), ex);
            }
        }

        /// <summary>
        /// 转成列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ToList<T>()
        {
            var fields = typeof(T).GetFields();
            var list = (from f in fields
                        where f.FieldType == typeof(T)
                        select (T)f.GetRawConstantValue()
                ).ToList();
            return list;
        }
    }
}
