using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        /// <summary>
        /// 转换为Json
        /// </summary>
        /// <returns></returns>
        //public string ToJson()
        //{
        //    var rows = JsonConvert.SerializeObject(this.rows);
        //    var footer = JsonConvert.SerializeObject(this.footer);
        //    var jsonBuilder = new StringBuilder();
        //    if (this.total == 0 && this.footer == null)
        //    {
        //        jsonBuilder.AppendFormat("{{\"rows\":{0}}}", rows);
        //    }
        //    else if (this.total == 0)
        //    {
        //        jsonBuilder.AppendFormat("{{\"rows\":{0}, \"footer\":{1}}}", rows, footer);
        //    }
        //    else if (this.footer == null)
        //    {
        //        jsonBuilder.AppendFormat("{{\"total\":{0}, \"rows\":{1}}}", this.total, rows);
        //    }
        //    else
        //    {
        //        jsonBuilder.AppendFormat("{{\"total\":{0}, \"rows\":{1}, \"footer\":{2}}}", this.Total, rows, footer);
        //    }
        //    return jsonBuilder.ToString();
        //}
    }
}
