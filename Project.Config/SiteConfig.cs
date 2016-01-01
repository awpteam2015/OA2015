using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Config
{
    public static class SiteConfig
    {
        private static SiteConfigEntiy _siteConfigEntiy;


        public static SiteConfigEntiy GetConfig()
        {
            return _siteConfigEntiy ?? (_siteConfigEntiy = new SiteConfigEntiy());
        }
    }
}
