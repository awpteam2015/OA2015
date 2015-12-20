using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler
{
    public static class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonEntiy"></param>
        /// <param name="contractResolver"></param>
        /// <returns></returns>
        public static string JsonSerializer(object jsonEntiy, DefaultContractResolver contractResolver = null, string dateFormat = "yyyy-MM-dd")
        {
            var t = JsonConvert.SerializeObject(jsonEntiy, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new JsonConverter[] { new IsoDateTimeConverter { DateTimeFormat = dateFormat },
                    new StringEnumConverter() { } },
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = contractResolver
            });
            return t;
        }

        /// <summary>
        /// 获取EasyUi列表格式
        /// </summary>
        /// <param name="total"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static string GetEasyUiFormatJson(int total, string rows)
        {
            var jsonBuilder = new StringBuilder();
            jsonBuilder.AppendFormat("{{\"total\":{0},\"rows\":{1}}}", total, rows);
            return jsonBuilder.ToString();
        }

        /// <summary>
        /// 把json反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 解析json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject Parse(string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// json转Dictionary
        /// </summary>
        /// <param name="json">json</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(string json)
        {
            var result = Deserialize<Dictionary<string, object>>(json);
            return result;
        }

        /// <summary>
        /// 返回json信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="msg">信息</param>
        /// <returns></returns>
        public static string ReturnMsg(bool status, string msg = "")
        {
            var json = JsonSerializer(new
            {
                status = status ? "success" : "fail",
                msg
            });
            return json;
        }

        /// <summary>
        /// 返回json信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="rows">行项目</param>
        /// <param name="total">总行数</param>
        /// <param name="msg">信息</param>
        /// <param name="dateFormat">日期格式化</param>
        /// <returns></returns>
        public static string ReturnMsg(bool status, object rows, int total, string msg = "", string dateFormat = "yyyy-MM-dd")
        {
            var json = JsonSerializer(new
            {
                status = status ? "success" : "fail",
                msg,
                total,
                rows
            }, null, dateFormat);
            return json;
        }
    }
}
