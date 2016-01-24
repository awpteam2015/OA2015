using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;

namespace Project.Infrastructure.FrameworkCore.WebMvc.Models
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


        public DataGridTreeResponse(int total, IList<T> rows)
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
