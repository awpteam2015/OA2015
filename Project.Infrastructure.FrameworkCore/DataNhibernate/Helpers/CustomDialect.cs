//using System.Data;
//using NHibernate;
//using NHibernate.Dialect;
//using NHibernate.Dialect.Function;

//namespace SyncSoft.ROM.Infrastructure.DataNhibernate.Helpers
//{
//    public class CustomDialect : Oracle10gDialect
//    {
//        public CustomDialect()
//        {
//            //RegisterFunction("NhExpansion.IsLike", new SQLFunctionTemplate(NHibernateUtil.String, "like '?1'"));
//            RegisterFunction("NhExpansion.ToOracleChar", new SQLFunctionTemplate(NHibernateUtil.String, "to_char(?1,?2)"));
//            RegisterFunction("Convert.ToInt64", new SQLFunctionTemplate(NHibernateUtil.String, "to_number(?1)"));
//            RegisterFunction("Convert.ToInt32", new SQLFunctionTemplate(NHibernateUtil.String, "to_number(?1)"));
//            RegisterFunction("Convert.ToDateTime", new SQLFunctionTemplate(NHibernateUtil.String, "to_date('?1','yyyy-mm-dd hh24:mi:ss')"));
//            RegisterFunction("NhExpansion.IsBetween", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 between ?2 "));
//            RegisterFunction("And", new SQLFunctionTemplate(NHibernateUtil.String, "?2 and ?1"));
//            RegisterFunction("NhExpansion.GreaterthanEqual", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 >= ?2"));
//            RegisterFunction("NhExpansion.Greaterthan", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 > ?2"));
//            RegisterFunction("NhExpansion.LessthanEqual", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 <= ?2"));
//            RegisterFunction("NhExpansion.Lessthan", new SQLFunctionTemplate(NHibernateUtil.Serializable, "?1 < ?2"));

//        }

//        protected override void RegisterCharacterTypeMappings()
//        {
//            RegisterColumnType(DbType.AnsiStringFixedLength, "CHAR(255)");
//            RegisterColumnType(DbType.AnsiStringFixedLength, 2000, "CHAR($l)");
//            RegisterColumnType(DbType.AnsiString, "VARCHAR2(255)");
//            RegisterColumnType(DbType.AnsiString, 4000, "VARCHAR2($l)");
//            RegisterColumnType(DbType.StringFixedLength, "NCHAR(255)");
//            RegisterColumnType(DbType.StringFixedLength, 2000, "NCHAR($l)");
//            RegisterColumnType(DbType.String, "VARCHAR2(255)");
//            RegisterColumnType(DbType.String, 4000, "VARCHAR2($l)");
//        }
//    }
//}
