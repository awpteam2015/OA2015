using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Infrastructure.FrameworkCore.Logging;

namespace Project.WebApplication.Tests
{
      [TestClass]
  public  class logtest
    {
          [TestMethod]
          public void TestMethod1()
          {
              LoggerHelper.Error(LogType.InfoLogger,"111");
          }
    }
}
