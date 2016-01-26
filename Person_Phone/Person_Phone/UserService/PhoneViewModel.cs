using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Phone.UserService
{
    public class PhoneViewModel
    {
        [Key]
        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneType { get; set; }

        public int UserId { get; set; }

        public PhoneViewModel()
        {

        }

        public PhoneViewModel(string type, string number, int userId)
        {
            this.PhoneType = type;
            this.PhoneNumber = number;
            this.UserId = userId;
        }
    }
}