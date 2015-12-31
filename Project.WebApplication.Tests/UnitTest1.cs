using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Map.PermissionManager;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
        }

        public class  a
        {
            public decimal? ad { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var axxx=new a();

            if (axxx .ad== null)
            {

            }


            var t = DateTime.UtcNow;
           // var t = AppDomain.CurrentDomain.BaseDirectory;
              UserInfoService.GetInstance().Test();
        }


        [TestMethod]
        public void Add()
        {
            UserInfoEntity t=new UserInfoEntity();
            t.UserCode = "1111";
            t.UserName = "2222";
            t.Password = "1111";
            UserInfoService.GetInstance().Add(t);
        }


        [TestMethod]
        public void Update()
        {
            var t = UserInfoService.GetInstance().GetModel(1);
            t.UserName = DateTime.Now.ToString();
            UserInfoService.GetInstance().Update(t);


            var t222 = UserInfoService.GetInstance().GetModel(1);
        }


        [TestMethod]
        public void Delete()
        {
            var t = UserInfoService.GetInstance().GetModel(3);
         
           // UserInfoService.GetInstance().Delete(t);

        }

    }
}
