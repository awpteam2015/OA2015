using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var t = AppDomain.CurrentDomain.BaseDirectory;
              UserService.GetInstance().Test();
        }
    }
}
