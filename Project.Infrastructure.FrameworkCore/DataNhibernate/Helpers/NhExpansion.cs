using System;
using System.Text.RegularExpressions;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers
{
    public static class NhExpansion
    {
        public static bool IsLike(this string source, string pattern)
        {
            pattern = Regex.Escape(pattern);
            pattern = pattern.Replace("%", ".*?").Replace("_", ".");
            pattern = pattern.Replace(@"\[", "[").Replace(@"\]", "]").Replace(@"\^", "^");
            return Regex.IsMatch(source, pattern);
        }

        public static bool IsBetweenAnd(this object projection, object lo, object hi)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="lo"></param>
        /// <returns></returns>
        public static bool GreaterthanEqual(this string projection, object lo)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="lo"></param>
        /// <returns></returns>
        public static bool Greaterthan(this string projection, object lo)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }


        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="lo"></param>
        /// <returns></returns>
        public static bool LessthanEqual(this string projection, object lo)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="lo"></param>
        /// <returns></returns>
        public static bool Lessthan(this string projection, object lo)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }



        public static NhExpansionBetweenBuilder IsBetween(this object projection, object lo)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }

        public class NhExpansionBetweenBuilder
        {
            public bool And(object hi)
            {
                throw new Exception("Not to be used directly - use inside QueryOver expression");
            }
        }

        public static string ToOracleChar(this DateTime? projection, string format)
        {
            throw new Exception("Not to be used directly - use inside QueryOver expression");
        }


    }
}
