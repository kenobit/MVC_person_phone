using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.NHibernateRepo
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename='C:\Git\Valtech\MVC_personDB_Project\PersonDB_project\PersonDB_project\App_Data\EntityRepo.EntityUserContext.mdf';Integrated Security=True")
            .ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}