using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.ReportManager;
using Project.Repository.ReportManager;

namespace Project.Service.ReportManager
{
   public class HrReportService
    {
        #region 构造函数
        private readonly HrReportRepository _HrReportRepository;
        private static readonly HrReportService Instance = new HrReportService();

        public HrReportService()
        {
            this._HrReportRepository = new HrReportRepository();
        }

        public static HrReportService GetInstance()
        {
            return Instance;
        }
        #endregion

       public void GetList(AttendanceViewEntity where)
       {
           _HrReportRepository.GerAttendanceView(where, 0, 10);
       }


    }
}
