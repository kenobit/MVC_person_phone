using Cassandra.Data.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person_Phone.Entities
{
    
    public class CassandraUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public virtual List<CassandraPhone> Phones { get; set; }
    }
}