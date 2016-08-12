using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using AutoMapper;
using Project.Model.HRManager;
using Project.Service.HRManager;

namespace Project.WebApplication
{
    /// <summary>
    /// OutService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OutService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]

        public List<AttendanceResponse> GetAttendanceList(string EmployeeCode, string EmployeeName, string State, string Date)
        {
            var where = new AttendanceEntity();
            where.EmployeeCode = EmployeeCode;
            where.EmployeeName = EmployeeName;
            where.State = State;
            if (!string.IsNullOrWhiteSpace(Date))
            {
                where.Date = DateTime.Parse(Date);
            }
            var list = AttendanceService.GetInstance().GetList(where);
            var newlist = new List<AttendanceResponse>();
            var mergInfo = Mapper.Map(list, newlist);
            return mergInfo;
        }

        [WebMethod]
        public List<EmployeeInfoResponse> GetEmployeeList(string DepartCode)
        {
            var where = new EmployeeInfoEntity();
            where.DepartmentCode = DepartCode;
            var list = EmployeeInfoService.GetInstance().GetList(where);
            var newlist = new List<EmployeeInfoResponse>();
            var mergInfo = Mapper.Map(list, newlist);
            return mergInfo;
        }
    }



}
