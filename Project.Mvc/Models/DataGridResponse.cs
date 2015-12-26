using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.Enum;
using Project.Model.ExtendUi;

namespace Project.Mvc.Models
{
    public class DataGridResponse
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 数据行
        /// </summary>
        public dynamic rows { get; set; }
        /// <summary>
        /// 表尾
        /// </summary>
        public dynamic footer { get; set; }
    }


    public class DataGridTreeResponse<T> where T : ITree
    {

        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 数据行
        /// </summary>
        public IList<T> rows { get; set; }


        public DataGridTreeResponse(int total,IList<T> rows)
        {
            if (total == 1)
            {
                rows.ForEach(p => p._parentId = TreeInvalidCodeEnum.Invalid.ToString());
            }

            this.total = total;
            this.rows = rows;
        }
    }


}
