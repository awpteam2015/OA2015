using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;

namespace Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results
{
    public class AbpJsonResult : JsonResult
    {

        private IContractResolver ContractResolver { get;set;}

        /// <summary>
        /// Constructor.
        /// </summary>
        public AbpJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// Constructor with JSON data.
        /// </summary>
        /// <param name="data">JSON data</param>
        public AbpJsonResult(object data)
            : this()
        {
            Data = data;
        }

        public AbpJsonResult(object data, IContractResolver contractResolver)
            : this()
        {
            ContractResolver = contractResolver;
            Data = data;
        }

        /// <inheritdoc/>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
   
                //var jsonSerializerSettings = new JsonSerializerSettings
                //{
                //    ContractResolver =ContractResolver?? new CamelCasePropertyNamesContractResolver()
                //};

                //response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, jsonSerializerSettings));

                response.Write(JsonHelper.JsonSerializer(Data, ContractResolver));
            }
        }
    }


    public class AbpJsonResult<T> : JsonResult
    {

        private IContractResolver ContractResolver { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public AbpJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// Constructor with JSON data.
        /// </summary>
        /// <param name="data">JSON data</param>
        public AbpJsonResult(AjaxResponse<T> data)
            : this()
        {
            Data = data;
        }

        public AbpJsonResult(AjaxResponse<T> data, IContractResolver contractResolver)
            : this()
        {
            ContractResolver = contractResolver;
            Data = data;
        }

        public AbpJsonResult(Tuple<bool,T> data )
            : this()
        {
            var result = new AjaxResponse<T>();
            if (data.Item1)
            {
                 result = new AjaxResponse<T>()
                {
                    success =true,
                    result = data.Item2
                };
            }
            else
            {
                result = new AjaxResponse<T>()
                {
                    success = false,
                    error = new ErrorInfo() {message = data.Item2.ToString()}
                };
            }
            ContractResolver = new NHibernateContractResolver(new string[] { "result", "error" });
            Data = result;
        }

        /// <inheritdoc/>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {

                //var jsonSerializerSettings = new JsonSerializerSettings
                //{
                //    ContractResolver =ContractResolver?? new CamelCasePropertyNamesContractResolver()
                //};

                //response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, jsonSerializerSettings));

                response.Write(JsonHelper.JsonSerializer(Data, ContractResolver));
            }
        }
    }
}
