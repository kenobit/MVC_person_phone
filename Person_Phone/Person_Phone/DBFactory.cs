using EntityRepo;
using Person_Phone;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonDB_project
{
    public class DBFactory
    {
        public IRepository<User> GetInstance(string ORM)
        {
            IRepository<User> repository;

            switch (ORM)
            {
                case "Entity":
                    {
                        repository = new EntityRepository();
                        break;
                    }
                case "Ado":
                    {
                        repository = new AdoRepo.AdoNetRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Git\Person_phone\Person_Phone\Person_Phone\App_Data\Default Connection.mdf';Integrated Security=True;Connect Timeout=30");
                        break;
                    }
                case "NH":
                    {
                        repository = new Person_Phone.NHibernateRepo.NHibernateRepository(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename='C:\Git\Valtech\MVC_personDB_Project\PersonDB_project\PersonDB_project\App_Data\EntityRepo.EntityUserContext.mdf';Integrated Security=True");
                        break;
                    }
                default:
                    {
                        repository = new EntityRepository();
                        break;
                    }
            }


            return repository;
        }

       
    }
}