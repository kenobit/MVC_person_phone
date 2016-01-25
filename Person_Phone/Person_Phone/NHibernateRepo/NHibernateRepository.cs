using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PersonDB_project;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.NHibernateRepo
{
    public class NHibernateRepository : IRepository<User>
    {
        ISessionFactory sessionFactory;
        public NHibernateRepository(string connectionString)
        {
            sessionFactory = CreateSessionFactory(connectionString);
        }
        public void Create(User item)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    session.Save(item);
                    transact.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            User user = null;
            using (var session = sessionFactory.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Person WHERE ID='" + id.ToString() + "'");
                    user = query.List<User>()[0];
                    session.Delete(user);
                    transact.Commit();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var session = sessionFactory.OpenSession())
            {
                string h_stmt = "FROM Person";
                IQuery query = session.CreateQuery(h_stmt);
                IList<User> userList = query.List<User>();
                return userList.ToList<User>();
            }
        }

        public User GetById(int id)
        {
            User user = null;
            using (var session = sessionFactory.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Person WHERE ID='" + id.ToString() + "'");
                    user = query.List<User>()[0];
                }
            }
            return user;
        }

        public void Save()
        {
            return;
        }

        public void Update(User item)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Person WHERE ID='" + item.Id.ToString() + "'");
                    User user = query.List<User>()[0];
                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.Age = item.Age;
                    session.Update(user);
                }
            }
        }

        private ISessionFactory CreateSessionFactory(string connectionString)
        {
            ISessionFactory isessionfact = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql7
                .ConnectionString(connectionString))
                .Mappings(m => m
                .FluentMappings.AddFromAssemblyOf<User>())
                .BuildSessionFactory();
            return isessionfact;
        }
    }
}