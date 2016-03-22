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
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport1(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerAttendanceReport1(where, skipResults, maxResults, ifGetALL);
            return list;
        }

        /// <summary>
        /// 缺勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        public Tuple<IList<AttendanceViewEntity2>, int> GerAttendanceReport2(AttendanceViewEntity2 where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerAttendanceReport2(where, skipResults, maxResults, ifGetALL);
            return list;
        }

        /// <summary>
        /// 人员出入统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<HREmployeeViewEntity>, int> GetHREmployeeReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerInOutEmployeeReport(where, skipResults, maxResults, ifGetALL);
            return list;
        }


        /// <summary>
        /// 党员统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<HREmployeeViewEntity>, int> GerEmployeeDYReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerEmployeeDYReport(where, skipResults, maxResults, ifGetALL);
            return list;
        }

        /// <summary>
        /// 学历统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<HREmployeeViewEntity>, int> GerEmployeeXLReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerEmployeeXLReport(where, skipResults, maxResults, ifGetALL);
            return list;
        }

        /// <summary>
        /// 缺勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport3(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerAttendanceReport3(where, skipResults, maxResults, ifGetALL);
            return list;
        }


        /// <summary>
        /// 缺勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport4(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var list = _HrReportRepository.GerAttendanceReport4(where, skipResults, maxResults, ifGetALL);
            return list;
        }


    }
}
