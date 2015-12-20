using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace  Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public class ConfigHelper
    {
        #region 写入Web.Config配置文件
        /// <summary>
        /// 写入Web.Config的AppSettings
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">写入的值</param>
        public static void WriteAppSettings(string key, string value)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 写入Web.Config的ConnectionStrings
        /// </summary>
        /// <param name="connection"></param>
        public static void WriteConnectionStrings(DatabaseConnection connection)
        {
            string connectionName = connection.ConnectionName;
            string connectionString = CreateSqlConnectionString(connection);
            string providerName = DatabaseProviderHelper.AdoNetProviderName(connection.Provider);

            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            config.ConnectionStrings.ConnectionStrings.Remove(connectionName);
            ConnectionStringSettings sqlSettings = new ConnectionStringSettings(connectionName, connectionString, providerName);
            config.ConnectionStrings.ConnectionStrings.Add(sqlSettings);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }

        /// <summary>
        /// 创建数据库连接字符串
        /// Data Source=.;Initial Catalog=CAS;User ID=sa;Password=7132189
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static string CreateSqlConnectionString(DatabaseConnection connection)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = connection.DataSource;
            builder.InitialCatalog = connection.Database;
            builder.UserID = connection.UserId;
            builder.Password = connection.Password;
            builder.ConnectTimeout = connection.ConnectionLifetime;
            builder.MinPoolSize = connection.MinPoolSize;
            builder.MaxPoolSize = connection.MaxPoolSize;
            return builder.ConnectionString;
        }
        #endregion

        #region 读取Web.Config
        /// <summary>
        /// 从WEB.Config中的appSettings中取add...的参数值
        /// </summary>
        /// <param name="key">add key='myKey' 中的key值</param>
        /// <param name="defaultValue">预设值，当没有key或key没有值时，返回defaultValue</param>
        /// <returns>key的对应值(add key='myKey' value='myVal'中的myVal)</returns>
        public static string GetString(String key, String defaultValue)
        {
            string appSettingValue = ConfigurationManager.AppSettings[key] as string;
            return string.IsNullOrEmpty(appSettingValue) ? defaultValue : appSettingValue;
        }

        #region 读取appSettings衍生方法
        /// <summary>
        /// 从WEB.Config中的appSettings中取add...的参数值
        /// </summary>
        /// <param name="key">add key='myKey' 中的key值</param>
        /// <returns>key的对应值(add key='myKey' value='myVal'中的myVal)</returns>
        public static string GetString(String key)
        {
            return GetString(key, "");
        }

        /// <summary>
        /// 从WEB.Config中的appSettings中取add...的参数值
        /// </summary>
        /// <param name="key">add key='myKey' 中的key值</param>
        /// <param name="defaultValue">预设值，当没有key或key没有值时，返回defaultValue</param>
        /// <returns>key的对应值(add key='myKey' value='myVal'中的myVal)</returns>
        public static int GetInt(String key, String defaultValue)
        {
            return int.Parse(GetString(key, defaultValue));
        }
        #endregion





        /// <summary>
        /// 从WEB.Config中的connectionStrings中取 connectionStrings 的参数值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DatabaseConnection GetConnectionString(String connectionName)
        {
            DatabaseConnection connection = new DatabaseConnection();
            connection.ConnectionName = connectionName;

            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            return connection;
        }

        #endregion
    }


    public class DatabaseConnection
    {
        /// <summary>
        /// 数据库连接名称
        /// </summary>
        public string ConnectionName { get; set; }
        /// <summary>
        /// 数据库服务器地址
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseProvider Provider { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectionLifetime { get; set; }
        /// <summary>
        /// 池中允许的最小连接数
        /// </summary>
        public int MinPoolSize { get; set; }
        /// <summary>
        /// 池中允许的最大连接数
        /// </summary>
        public int MaxPoolSize { get; set; }
    }


    public enum DatabaseProvider
    {
        MsSQL,
        Access,
        PostgreSQL,
        MySQL,
        Oracle,
        DB2,
        Informix,
        SQLite,
        Firebird
    }

    public class DatabaseProviderHelper
    {
        /// <summary>
        /// 用于数据绑定
        /// </summary>
        public static Dictionary<string, string> dictDatabaseProvider = new Dictionary<string, string> { 
                                                            { "MsSQL", "Microsoft SQL Server" }, 
                                                            { "Access", "Access" },
                                                            { "PostgreSQL", "PostgreSQL" },
                                                            { "MySQL", "MySQL" },
                                                            { "Oracle", "Oracle" },
                                                            { "DB2", "DB2" },
                                                            { "Informix", "Informix" },
                                                            { "SQLite", "SQLite" },
                                                            { "Firebird", "Firebird" }
                                                        };

        public static string AdoNetProviderName(DatabaseProvider provider)
        {
            string providerName = string.Empty;
            switch (provider)
            {
                case DatabaseProvider.MsSQL:
                    providerName = "System.Data.SqlClient";
                    break;
                case DatabaseProvider.Access:
                    providerName = "System.Data.OleDb";
                    break;
            }
            return providerName;
        }

        public static string IBatisProviderName(DatabaseProvider provider)
        {
            string providerName = string.Empty;
            switch (provider)
            {
                case DatabaseProvider.MsSQL:
                    providerName = "SqlServer";
                    break;
                case DatabaseProvider.Access:
                    providerName = "OleDb";
                    break;
            }
            return providerName;
        }
    }

}
