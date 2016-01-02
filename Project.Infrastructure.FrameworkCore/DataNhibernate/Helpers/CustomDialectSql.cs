using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers
{
     public class CustomDialectSql : MsSql2008Dialect
    {
        public CustomDialectSql()
        {
            RegisterFunction("Convert.ToInt64", new SQLFunctionTemplate(NHibernateUtil.String, "CHAR(?1 as int)"));
            RegisterFunction("Convert.ToInt32", new SQLFunctionTemplate(NHibernateUtil.String, "CHAR(?1 as int)"));
            RegisterFunction("Convert.ToDateTime", new SQLFunctionTemplate(NHibernateUtil.String, "convert(smalldatetime,'?1')"));
            RegisterFunction("NhExpansion.IsBetween", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 between ?2 "));
            RegisterFunction("And", new SQLFunctionTemplate(NHibernateUtil.String, "?2 and ?1"));
        }
    }

}
