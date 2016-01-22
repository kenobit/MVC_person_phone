using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonDB_project.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Phone> phones { get; set; }
    }
}