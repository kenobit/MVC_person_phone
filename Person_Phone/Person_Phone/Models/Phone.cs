using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonDB_project.Models
{
    public class Phone
    {
        [Key]
        public virtual int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        // [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина телефона должна быть от {1} до {0} символов")]
        public virtual string PhoneNumber { get; set; }

        // [StringLength(35, MinimumLength = 1, ErrorMessage = "Длина типа телефона должна быть от {1} до {0} символов")]
        public virtual string PhoneType { get; set; }

        [ForeignKey("user")]
        public virtual int UserId { get; set; }

        public virtual User user { get; set; }

        public Phone()
        {

        }
        public Phone(string type, string number, int userId)
        {
            this.PhoneType = type;
            this.PhoneNumber = number;
            this.UserId = userId;
        }
    }
    public class PhoneMap : ClassMap<Phone>
    {
        public PhoneMap()
        {
            Id(x => x.Id);
            Map(x => x.PhoneType);
            Map(x => x.PhoneNumber);
            Map(x => x.UserId);
            References(x => x.user).Column("UsersId");
            Table("Phones");
        }
    }



}