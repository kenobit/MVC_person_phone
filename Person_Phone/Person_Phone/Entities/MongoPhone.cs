using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    public class MongoPhoneModel
    {
        [BsonId]
        public int Id { get; set; }
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("PhoneType")]
        public string PhoneType { get; set; }
    }
}