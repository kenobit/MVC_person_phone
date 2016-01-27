using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    public class CassandraPhone
    {
        public int Id { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        
        public virtual CassandraUser user { get; set; }
    }
}