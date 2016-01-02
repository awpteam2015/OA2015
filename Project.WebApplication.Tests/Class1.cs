using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project.WebApplication.Tests
{
    [TestClass]
    public class UnitTest2
    {
        public UnitTest2()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
           List<string> list1=new List<string>(){"111","222"};

               List<string> list2=new List<string>();

            var tt = (from a in list1
                from b in list2
                where a == b
                select a).ToList();

        }

        [TestMethod]
        public void TestMethod2()
        {
            List<string> list1 = new List<string>() { "111", "222" };

            List<string> list2 = new List<string>() { "111", "3333" };

            var tt = (from a in list1
                      from b in list2
                      where a == b
                      select a).ToList();

        }


        [TestMethod]
        public void TestMethod3()
        {
            List<string> list1 = new List<string>() { "111", "222" };

            List<string> list2 = new List<string>();

            var tt = (from a in list1
                join b in list2 on a equals b into joinlist
                    //  from ur in joinlist.DefaultIfEmpty()

                      select a).ToList();

        }

    }
}
