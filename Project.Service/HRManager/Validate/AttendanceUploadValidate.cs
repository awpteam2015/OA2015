using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager.Validate
{
    public class AttendanceUploadValidate
    {
        #region
        private static readonly AttendanceUploadValidate Instance = new AttendanceUploadValidate();
        private readonly AttendanceUploadRecordRepository _attendanceUploadRecordRepository;

        private AttendanceUploadValidate()
        {
            _attendanceUploadRecordRepository = new AttendanceUploadRecordRepository();
        }

        public static AttendanceUploadValidate GetInstance()
        {
            return Instance;
        }
        #endregion

        public Tuple<bool, string> IsCanUpload(string departmentCode, DateTime date)
        {
            var expr = PredicateBuilder.True<AttendanceUploadRecordEntity>();
            expr = expr.And(p => p.DepartmentCode == departmentCode);
            expr = expr.And(p => p.Date == date);

            var list = _attendanceUploadRecordRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();

            if (list.Any())
            {
                return new Tuple<bool, string>(false, "考勤记录已经上传过！");
            }
            else
            {
                return new Tuple<bool, string>(true, "");
            }
        }

    }
}
