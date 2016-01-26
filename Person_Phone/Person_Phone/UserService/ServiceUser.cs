using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonDB_project;
using PersonDB_project.Models;

namespace Person_Phone.UserService
{
    public class ServiceUser : IService
    {
        public IRepository<User> repository;

        public ServiceUser()
        {
            this.repository = new EntityRepo.EntityRepository();
        }

        public ServiceUser(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public void Create(UserViewModel item)
        {
            this.repository.Create(ToModel(item));
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            List<UserViewModel> userVMs = new List<UserViewModel>();
            foreach (User user in repository.GetAll())
            {
                userVMs.Add(ToViewModel(user));
            }
            return userVMs;
        }

        public UserViewModel GetById(int id)
        {
            UserViewModel userVM = new UserViewModel();
            userVM = ToViewModel(repository.GetById(id));
            return userVM;
        }

        public void Save()
        {
            repository.Save();
        }

        public void Update(UserViewModel item)
        {
            repository.Update(ToModel(item));
        }

        private UserViewModel ToViewModel(User user)
        {
            UserViewModel userVM = new UserViewModel();
            userVM.Id = user.Id;
            userVM.FirstName = user.FirstName;
            userVM.LastName = user.LastName;
            userVM.Age = user.Age;

            foreach (PersonDB_project.Models.PhoneViewModel phone in user.Phones)
            {
                PhoneViewModel phoneVM = ToPhoneViewModel(phone);
                userVM.Phones.Add(phoneVM);
            }

            return userVM;
        }

        private User ToModel(UserViewModel userVM)
        {
            User user = new User();

            user.Id = userVM.Id;
            user.FirstName = userVM.FirstName;
            user.LastName = userVM.LastName;
            user.Age = userVM.Age;

            foreach (PhoneViewModel phoneVM in userVM.Phones)
            {
                PersonDB_project.Models.PhoneViewModel phone = ToPhoneModel(phoneVM);
                user.Phones.Add(phone);
            }

            return user;
        }

        private List<UserViewModel> ToListViewModel(List<User> users)
        {
            List<UserViewModel> userVMs = new List<UserViewModel>();

            foreach (User item in users)
            {
                ToViewModel(item);
            }

            return userVMs;
        }

        private PhoneViewModel ToPhoneViewModel(PersonDB_project.Models.PhoneViewModel phone)
        {
            PhoneViewModel phoneVM = new PhoneViewModel();

            if (phone == null)
            {
                phoneVM = null;
                return phoneVM;
            }
            else
            {
                phoneVM.Id = phone.Id;
                phoneVM.UserId = phone.UserId;
                phoneVM.PhoneNumber = phone.PhoneNumber;
                phoneVM.PhoneType = phone.PhoneType;
            }

            return phoneVM;
        }

        private PersonDB_project.Models.PhoneViewModel ToPhoneModel(PhoneViewModel phoneVM)
        {
            PersonDB_project.Models.PhoneViewModel phone = new PersonDB_project.Models.PhoneViewModel();

            if (phoneVM == null)
            {
                phone = null;
                return phone;
            }
            else
            {
                phone.Id = phoneVM.Id;
                phone.UserId = phoneVM.UserId;
                phone.PhoneNumber = phoneVM.PhoneNumber;
                phone.PhoneType = phoneVM.PhoneType;
            }

            return phone;
        }
    }
}