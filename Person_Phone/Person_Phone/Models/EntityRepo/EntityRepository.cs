using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonDB_project;
using PersonDB_project.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;

namespace EntityRepo
{
    public class EntityRepository : IRepository<User>
    {
        EntityUserContext db = new EntityUserContext();

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            db.Users.Remove(GetById(id));
            db.Phones.RemoveRange(db.Phones.Where(p => p.UserId == id));
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(int id)
        {
            return db.Users.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(User item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            int Id = item.Id;
            User user = GetById(Id);
         
            user.FirstName = item.FirstName;
            user.LastName = item.LastName;
            user.Age = item.Age;

            db.SaveChanges();
        }
    }
}
