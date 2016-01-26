using MongoDB.Driver;
using Person_Phone.Entities;
using PersonDB_project;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;

namespace Person_Phone.MongoDBRepo
{
    public class MongoDBRepository : IRepository<User>
    {

        IMongoClient client;
        IMongoDatabase db;

        public MongoDBRepository()
        {
            client = new MongoClient();
            db = client.GetDatabase("MongoUsers");
        }

        public void Create(User item)
        {
            MongoUserModel document = new MongoUserModel();
            document = UserToMongoUser(item);
            string hashString = document.FirstName + document.LastName + document.Age.ToString();
            document.Id = hashString.GetHashCode();
            var collection = db.GetCollection<MongoUserModel>("MongoUser");
            collection.InsertOne(document);
        }

        public void Delete(int id)
        {
            var collection = db.GetCollection<MongoUserModel>("MongoUser");
            collection.DeleteOne(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            var collection = db.GetCollection<MongoUserModel>("MongoUser");
            var list = collection.Find(_ => true).ToList();
            foreach (var item in list)
            {
                var user = MongoUserToUser(item);
                users.Add(user);
            }

            return users;
        }

        public User GetById(int id)
        {
            var collection = db.GetCollection<MongoUserModel>("MongoUser");
            MongoUserModel mongoUser =  collection.Find(x => x.Id == id).Single();
            User user = new User();
            user = MongoUserToUser(mongoUser);

            return user;
        }

        public void Save()
        {
            return;
        }

        public void Update(User item)
        {
            var collection = db.GetCollection<MongoUserModel>("MongoUser");
            var filter = Builders<MongoUserModel>.Filter.Eq("Id", item.Id);
            collection.FindOneAndReplace(filter, UserToMongoUser(item));
        }

        private MongoUserModel UserToMongoUser(User user)
        {
            MongoUserModel MongoUser = new MongoUserModel();
        
            MongoUser.Id = user.Id;
            MongoUser.FirstName = user.FirstName;
            MongoUser.LastName = user.LastName;
            MongoUser.Age = user.Age;

            foreach (var phone in user.Phones)
            {
                MongoPhoneModel mongoPhone = PhoneToMongoPhone(phone);
                MongoUser.Phones.Add(mongoPhone);
            }

            return MongoUser;
        }

        private User MongoUserToUser(MongoUserModel mongoUser)
        {
            User user = new User();

            user.Id = mongoUser.Id;
            user.FirstName = mongoUser.FirstName;
            user.LastName = mongoUser.LastName;
            user.Age = mongoUser.Age;

            foreach (var mongoPhone in mongoUser.Phones)
            {
                Phone phone = MongoPhoneToPhone(mongoPhone);
                user.Phones.Add(phone);
            }

            return user;
        }

        private MongoPhoneModel PhoneToMongoPhone(Phone phone)
        {
            MongoPhoneModel MongoPhone = new MongoPhoneModel();

            MongoPhone.Id = phone.Id;
            MongoPhone.PhoneNumber = phone.PhoneNumber;
            MongoPhone.PhoneType = phone.PhoneType;

            return MongoPhone;
        }

        private Phone MongoPhoneToPhone(MongoPhoneModel mongoPhone)
        {
            Phone phone = new Phone();

            phone.Id = mongoPhone.Id;
            phone.PhoneNumber = mongoPhone.PhoneNumber;
            phone.PhoneType = mongoPhone.PhoneType;

            return phone;
        }
    }
}