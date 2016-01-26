using Person_Phone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonDB_project.Models
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

        public UserViewModel(int Id, string firstName, string lastName, int age)
        {
            Phones = new List<PhoneViewModel>();
            this.Id = Id;
            this.FirstName = firstName;
            this.LastName = LastName;
            this.Age = age;
        }
    }
}