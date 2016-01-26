using MongoDB.Bson.Serialization.Attributes;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    public class MongoUserModel
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }

        public virtual List<MongoPhoneModel> Phones { get; set; }

        public MongoUserModel()
        {
            Phones = new List<MongoPhoneModel>();
        }
    }
}