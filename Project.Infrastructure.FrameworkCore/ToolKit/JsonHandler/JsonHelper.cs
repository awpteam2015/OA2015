using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;


namespace Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler
{
    public static class JsonHelper
    {
        public static string ReturnMsg(bool status, string msg = "", object extension = null)
        {
            var json = new JavaScriptSerializer().Serialize(new
            {
                success = status,
                msg = msg,
                extension = extension
            });
            return json;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonEntiy"></param>
        /// <param name="contractResolver"></param>
        /// <returns></returns>
        public static string JsonSerializer(object jsonEntiy, IContractResolver contractResolver = null, string dateFormat = "yyyy-MM-dd")
        {
            try
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
            catch (Exception e)
            {
                
                throw;
            }
          
           
        }



    }
}
