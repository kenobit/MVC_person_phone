using EntityRepo;
using Person_Phone.NHibernateRepo;
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
                        repository = new AdoRepo.AdoNetRepository(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Git\Valtech\MVC_person_phone\Person_Phone\Person_Phone\App_Data\Default Connection.mdf';Integrated Security=True;Connect Timeout=30");
                        break;
                    }
                case "NH":
                    {
                        repository = new NHibernateRepository();
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