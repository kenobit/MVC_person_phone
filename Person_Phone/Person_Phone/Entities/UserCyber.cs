using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    public class UserCyber
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual List<PhoneCyber> Phones { get; set; }

        public UserCyber()
        {
            Phones = new List<PhoneCyber>();
        }
    }
}