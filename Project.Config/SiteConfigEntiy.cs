using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Config
{
    public class SiteConfigEntiy
    {
        public string JsParamter {
            get { return ConfigurationManager.AppSettings["JsParamter"]; }
        }


    }


}
