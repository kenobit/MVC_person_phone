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
        public int Id { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Длина телефона должна быть от {1} до {0} символов")]
        public string PhoneNumber { get; set; }

        [StringLength(35, MinimumLength = 1, ErrorMessage = "Длина типа телефона должна быть от {1} до {0} символов")]
        public string PhoneType { get; set; }

        public int? UserId { get; set; }

        public User user { get; set; }

        public Phone()
        {

        }
        public Phone(string type, string number, int? userId)
        {
            this.PhoneType = type;
            this.PhoneNumber = number;
            if (userId == null)
            {
                this.UserId = userId;
            }
        }
    }

}