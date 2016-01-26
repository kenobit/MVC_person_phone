using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Phone.UserService
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public virtual List<PhoneViewModel> Phones { get; set; }
        public UserViewModel()
        {
            Phones = new List<PhoneViewModel>();
        }
    }
}