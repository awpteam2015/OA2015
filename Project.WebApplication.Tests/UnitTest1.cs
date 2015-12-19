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


        [TestMethod]
        public void TestMethod1()
        {
            var t = AppDomain.CurrentDomain.BaseDirectory;
              UserService.GetInstance().Test();
        }


        [TestMethod]
        public void Add()
        {
            UserInfoEntity t=new UserInfoEntity();
            t.UserCode = "1111";
            t.UserName = "2222";
            t.Password = "1111";
            UserService.GetInstance().Add(t);
        }


        [TestMethod]
        public void Update()
        {
            var t = UserService.GetInstance().GetModel(1);
            t.UserName = DateTime.Now.ToString();
            UserService.GetInstance().Update(t);


            var t222 = UserService.GetInstance().GetModel(1);
        }


        [TestMethod]
        public void Delete()
        {
            var t = UserService.GetInstance().GetModel(3);
         
            UserService.GetInstance().Delete(t);

        }

    }
}
