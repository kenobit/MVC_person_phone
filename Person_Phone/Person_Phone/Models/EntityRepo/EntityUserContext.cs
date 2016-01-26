using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityRepo
{
    class EntityUserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PhoneViewModel> Phones { get; set; }

        public EntityUserContext() : base("Default Connection")
        {

        }
    }
}
