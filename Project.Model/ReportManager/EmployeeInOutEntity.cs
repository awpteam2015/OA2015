using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.ReportManager
{
    public class EmployeeInOutEntity
    {
        public virtual string ParnetDepartmentName { get; set; }

        /// <summary>
        /// 增加小计
        /// </summary>
        public virtual int Zjxj { get; set; }
        /// <summary>
        /// 本系统调入
        /// </summary>
        public virtual int Bxdl { get; set; }
        /// <summary>
        /// 新录用
        /// </summary>
        public virtual int Ly { get; set; }
        /// <summary>
        /// 调入
        /// </summary>
        public virtual int Dl { get; set; }
        /// <summary>
        /// 减少小计
        /// </summary>
        public virtual int Jsxj { get; set; }
        /// <summary>
        /// 退休
        /// </summary>
        public virtual int Tx { get; set; }
        /// <summary>
        /// 辞职
        /// </summary>
        public virtual int Cz { get; set; }
        /// <summary>
        /// 调出
        /// </summary>
        public virtual int Bxttc { get; set; }

    }
}
