using System;
using System.IO;
using System.Reflection;
using System.Web;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.DataNhibernate.SessionStorage;

namespace Project.Infrastructure.FrameworkCore.DataNhibernate
{
    public static class SessionFactoryManager
    {

        /// <summary>
        /// Session Factory
        /// </summary>
        public static ISessionFactory SessionFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        static SessionFactoryManager()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            var path = "";
            if (HttpContext.Current != null)
            {
                path = HttpContext.Current.Server.MapPath("/Config/hibernate.cfg.xml");
            }
            else
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Config\",
                    @"hibernate.cfg.xml");
            }

            var config = new Configuration().Configure(path);
            //config.Properties[Environment.CollectionTypeFactoryClass]
            //       = typeof(Net4CollectionTypeFactory).AssemblyQualifiedName;
             config.LinqToHqlGeneratorsRegistry<HqlGeneratorForMethodExtend>();
            // config.SetInterceptor();
            var t = Fluently.Configure(config);
            try
            {
                var mapAssembly = Assembly.Load("Project.Map");
                SessionFactory = Fluently.Configure(config)
                 .Mappings(m => m.FluentMappings.AddFromAssembly(mapAssembly)
                     //.ExportTo(@"E:\XmlMappings2")
                 ).BuildSessionFactory();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///  currentSession.FlushMode = FlushMode.Commit;
        /// </summary>
        /// <returns></returns>
        private static ISession GetNewSession()
        {
            ISession currentSession = SessionFactory.OpenSession();
            currentSession.FlushMode = FlushMode.Commit;
            return currentSession;
        }

        /// <summary>
        ///  currentSession.FlushMode = FlushMode.Auto;
        /// </summary>
        /// <returns></returns>
        private static ISession GetNewSessionDefault()
        {
            return SessionFactory.OpenSession();
        }


        public static ISession GetCurrentSession()
        {
            // return SessionFactory.GetCurrentSession();
            ISessionStorageContainer _sessionStorageContainer = SessionStorageFactory.GetStorageContainer();

            ISession currentSession = _sessionStorageContainer.GetCurrentSession();

            if (currentSession == null)
            {
                currentSession = GetNewSession();
                _sessionStorageContainer.Store(currentSession);
            }
            return currentSession;
        }

        public static void Clear()
        {
            ISessionStorageContainer _sessionStorageContainer = SessionStorageFactory.GetStorageContainer();
            ISession currentSession = _sessionStorageContainer.GetCurrentSession();
            if (currentSession != null)
            {
                currentSession.Clear();
                _sessionStorageContainer.Remove();
            }
        }

        public static void Dispose()
        {
            SessionStorageFactory.Dispose();
        }

    }
}
