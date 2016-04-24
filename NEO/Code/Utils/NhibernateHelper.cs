using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NEO.Code.Model;
using NHibernate;
using NHibernate.Context;

namespace NEO.Code.Utils
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            if (HttpRuntime.AppDomainAppId != null)
            {
                var fluentConfiguration = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.ConnectionStrings[1].ConnectionString).ShowSql())
                                          .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AsteroidModel>())
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                                          .ExposeConfiguration(cfg => cfg.SetProperty("current_session_context_class", "web").SetProperty("format_sql", "true").SetProperty("generate_statistics", "true"));

                var config = fluentConfiguration.BuildConfiguration();
                //foreach (PersistentClass persistentClass in config.ClassMappings)
                //{
                //    persistentClass.DynamicUpdate = true;
                //}

                _sessionFactory = config.BuildSessionFactory();
            }
            else
            {
                var fluentConfiguration = Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConfigurationManager.ConnectionStrings[1].ConnectionString).ShowSql())
                                                  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AsteroidModel>())
                    //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                                                  .ExposeConfiguration(cfg => cfg.SetProperty("current_session_context_class", "thread_static").SetProperty("format_sql", "true").SetProperty("generate_statistics", "true"));

                var config = fluentConfiguration.BuildConfiguration();
                //foreach (PersistentClass persistentClass in config.ClassMappings)
                //{
                //    persistentClass.DynamicUpdate = true;
                //}

                _sessionFactory = config.BuildSessionFactory();
            }

            //NHibernate.Glimpse.Plugin.RegisterSessionFactory(_sessionFactory);
        }

        public static void CloseSession()
        {
            var session = CurrentSessionContext.Unbind(SessionFactory);
            if (session != null) session.Close();
        }

        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }

            return SessionFactory.GetCurrentSession();
        }
    }
}