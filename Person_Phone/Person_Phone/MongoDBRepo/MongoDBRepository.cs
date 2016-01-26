using MongoDB.Driver;
using PersonDB_project;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.MongoDBRepo
{
    public class MongoDBRepository : IRepository<UserDocument>
    {

        //MongoClient Client;
        //MongoDatabase database;

        //public MongoDBRepository(string connectionString)
        //{
        //    server = MongoServer.Create(connectionString);
        //    database = server.GetDatabase("Person");
        //    database.CreateCollection("Persons");
        //}

        public void Create(UserDocument item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDocument> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserDocument GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(UserDocument item)
        {
            throw new NotImplementedException();
        }
    }
}