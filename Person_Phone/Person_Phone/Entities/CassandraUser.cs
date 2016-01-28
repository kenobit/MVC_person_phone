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

        public List<CassandraPhone> Phones { get; set; }

        public CassandraUser()
        {
            Phones = new List<CassandraPhone>();
        }
    }
}