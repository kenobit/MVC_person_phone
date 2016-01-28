using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    public class PhoneCyber
    {
        public int Id { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("userCyber")]
        public int cyberuserId { get; set; }

        public UserCyber userCyber { get; set; }
    }
}