using FluentNHibernate.Mapping;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonDB_project.Models
{
    public class User
    {
        [Key]
        public virtual int Id { get; set; }

        [Display(Name = "Имя")]
        //[StringLength(50,MinimumLength =3,ErrorMessage = "имя не может быть длиннее {0} и короче {1} символов")]
        public virtual string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        //[StringLength(50,MinimumLength =3,ErrorMessage = "Фамилия не может быть длиннее {0} и короче {1} символов")]
        public virtual string LastName { get; set; }

        // [Range(0,100,ErrorMessage = "Возраст не может быть больше {1} и меньше {0} лет ")]
        [Display(Name = "Возраст")]
        public virtual int Age { get; set; }

        private IList<Phone> _phones;
        [Display(Name = "Телефоны")]
        public virtual IList<Phone> Phones
        {
            get
            {
                return _phones ?? (_phones = new List<Phone>());
            }
            set
            {
                _phones = value;
            }
        }

        public User()
        {

        }
    }

    public class MapUser : ClassMap<User>
    {
        public MapUser()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Age);
            HasMany(x => x.Phones);
            Table("Users");
        }
    }

    public class UserDocument
    {

        //public ObjectId id { get; set; }
        [BsonId]
        public int Id { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Age")]
        public int Age { get; set; }
        public static explicit operator User(UserDocument udoc)
        {
            return new User() { Id = udoc.Id, FirstName = udoc.FirstName, LastName = udoc.LastName, Age = udoc.Age };
        }

        public virtual List<Phone> Phones { get; set; }

        public UserDocument()
        {
            Phones = new List<Phone>();
        }
    }
}