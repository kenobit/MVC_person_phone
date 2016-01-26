using PersonDB_project;
using PersonDB_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person_Phone.UserService
{
    public interface IService
    {
        //IRepository<User> repository { get; set; }
        IEnumerable<UserViewModel> GetAll();
        UserViewModel GetById(int id);
        void Create(UserViewModel item);
        void Update(UserViewModel item);
        void Delete(int id);
        void Save();
    }
}
