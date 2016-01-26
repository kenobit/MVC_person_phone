using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
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
        //ISessionFactory sessionFactory = NHibernateHelper.OpenSession();
        
        public void Create(User item)
        {
            using (var session = NHibernateHelper.OpenSession())
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
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Users WHERE ID='" + id.ToString() + "'");
                    user = query.List<User>()[0];
                    session.Delete(user);
                    transact.Commit();
                }
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                session.BeginTransaction();
                //string h_stmt = "FROM Users";
                //IQuery query = session.CreateQuery(h_stmt);
                //IList<User> userList = query.List<User>();
                //return userList.ToList();
                List<User> users = (List<User>)session.CreateQuery("FROM User").List<User>();
                session.Transaction.Commit();
                return users;
            }
        }

        public User GetById(int id)
        {
            User user = null;
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Users WHERE ID='" + id.ToString() + "'");
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
            using (var session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transact = session.BeginTransaction())
                {
                    IQuery query = session.CreateQuery("FROM Users WHERE ID='" + item.Id.ToString() + "'");
                    User user = query.List<User>()[0];
                    user.FirstName = item.FirstName;
                    user.LastName = item.LastName;
                    user.Age = item.Age;
                    session.Update(user);
                }
            }
        }
    }
}