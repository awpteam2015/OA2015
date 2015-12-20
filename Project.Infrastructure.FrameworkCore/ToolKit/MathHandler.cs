using System;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// 算数处理
    /// </summary>
    public static class MathHandler
    {
        /// <summary>
        /// 保留小数位数（只要后面的小数不为0，就进1）
        /// </summary>
        /// <param name="num"></param>
        /// <param name="n">小数点后几位</param>
        /// <returns></returns>
        public static Decimal RoundData(Decimal num, int n)
        {
            Decimal newNum = num * Convert.ToDecimal(Math.Pow(10, n));
            if (newNum == (Int32)newNum)
            {
                //如果只有一位小数
                newNum = (Int32)newNum / Convert.ToDecimal(Math.Pow(10, n));
            }
            else
            {
                //如果有多位小数，就进1
                newNum = (Int32)(newNum + 1) / Convert.ToDecimal(Math.Pow(10, n));
            }
            return newNum;
        }


    }
}
