using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Util;
using Project.Model.PermissionManager;
using Project.Model.ReportManager;
using Project.Service.PermissionManager;
using Project.Service.ReportManager;

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

        

            //  DepartmentService.GetInstance().GetChildDepartmentCode();

            //HrReportService.GetInstance().GerAttendanceReport1(new AttendanceViewEntity(){Attr_StartDate = DateTime.Now},0,10);
            //List<string> list1 = new List<string>() { "111", "222" };

            //List<string> list2 = new List<string>();



            //var tt = (from a in list1
            //          from b in list2
            //          where a == b
            //          select a).ToList();



        }

        [TestMethod]
        public void TestMethod2()
        {
            List<string> list1 = new List<string>() { "111", "222" };

            List<string> list2 = new List<string>() { "111", "3333" };

            List<string> tt = (from a in list1
                               from b in list2
                               where a == b
                               select a).ToList();

            var ttttt = list1.SelectMany(left => list2, (t, s) => new { t, s }).Where(p => p.t == p.s).ToList();


            List<string> newList = new List<string>();
            foreach (var a in list1)
            {
                foreach (var b in list2)
                {
                    if (a == b)
                    {
                        newList.Add(a);
                    }
                }
            }


            List<string> tt2 = list1.Where(p => list2.Any(x => x == p)).ToList();

            List<string> newList2 = new List<string>();
            foreach (var a in list1)
            {
                foreach (var b in list2)
                {
                    if (a == b)
                    {
                        newList2.Add(a);
                    }
                }
            }

        }


        [TestMethod]
        public void TestMethod3()
        {
            List<string> list1 = new List<string>() { "111", "222" };

            List<A> list2 = new List<A>()
            {
                new A() { code = "111", name = "222" },
                new A() { code = "111", name = "333" }
            };

            var tt = (from a in list1
                      join b in list2 on a equals b.code into joinlist
                      from ur in joinlist.DefaultIfEmpty()
                      where ur != null && ur.name == "222"
                      select new { a, ur }).ToList();



            var tt2 = list1.Where(p => list2.Any(x => x.code == p));
        }


        public class A
        {
            public string code { get; set; }
            public string name { get; set; }
        }

    }
}
