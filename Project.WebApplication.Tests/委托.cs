using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Project.WebApplication.Tests
{
    [TestClass]
    public class 委托
    {
        public 委托()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            Func<string, string, string> funcSapsalesMark = (splx, salesMark) => { return ""; };

            var t = funcSapsalesMark("11", "222");
        }

        [TestMethod]
        public void TestMethod2()
        {
            //Func<string, string, string> funcSapsalesMark = (splx, salesMark) =>
            //{
            //    return funcSapsalesMark(splx, salesMark);
            //};

            //var t = funcSapsalesMark("11", "222");
        }

        [TestMethod]
        public void TestMethod3()
        {
           // Expression<Func<int>> axxx = () =>
           // {
           //     return 222;
           // };

           // Func<int> axxx2 = () =>
           // {
           //     return 222;
           // };


           // Expression<Func<int>> a = () => 5;

           // a.Compile();


           //Func<int> a2 = () => 5;

            //Func<string, string, string> funcSapsalesMark = (splx, salesMark) =>
            //{
            //    return funcSapsalesMark(splx, salesMark);
            //};

            //var t = funcSapsalesMark("11", "222");
        }



        [TestMethod]
        public void TestMethod4()
        {

            MethodInfo method = typeof (string).GetMethod("StartsWith", new[] {typeof (string)});
            var target = Expression.Parameter(typeof (string), "x");
            var methodArg = Expression.Parameter(typeof(string), "y");
            Expression[] methodArgs = new[] { methodArg };
            Expression call = Expression.Call(target, method, methodArgs);

            var lambarParamter = new[] {target, methodArg};
            var lambda = Expression.Lambda<Func<string, string, bool>>(call, lambarParamter);

            var compile = lambda.Compile();



            //Func<string, string, string> funcSapsalesMark = (splx, salesMark) =>
            //{
            //    return funcSapsalesMark(splx, salesMark);
            //};

            //var t = funcSapsalesMark("11", "222");
        }
    }
}
