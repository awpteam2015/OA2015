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

       /// <summary>
        /// 缺勤汇总
       /// </summary>
       /// <param name="where"></param>
       /// <param name="skipResults"></param>
       /// <param name="maxResults"></param>
        public Tuple< IList<AttendanceViewEntity>,int> GerAttendanceReport1(AttendanceViewEntity where, int skipResults, int maxResults)
       {
        var list=   _HrReportRepository.GerAttendanceReport1(where, skipResults, maxResults);
        return new System.Tuple<IList<AttendanceViewEntity>, int>(list.Item1, 0);
       }


    }
}
