using Moq;
using PersonDB_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonDB_project.Models;
using PersonDB_project.Controllers;
using System.Web.Mvc;
using Person_Phone.UserService;
using NUnit.Framework;

namespace Person_Phone.Tests
{
    [TestFixture]
    public class UserControllerTest
    {

        private Mock<IRepository<User>> _personRepository;
        private IService _service;

        public UserControllerTest()
        {
            _personRepository = new Mock<IRepository<User>>();
            _service = new ServiceUser(_personRepository.Object);
        }

        [Test]
        public void MultiIndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IService>();
            List<UserViewModel> list = new List<UserViewModel>();
            mock.Setup(a => a.GetAll()).Returns(list);
            UserController controller = new UserController(mock.Object);

            // Act
            ViewResult result = controller.MultiIndex() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void DetailsViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IService>();
            UserViewModel user = new UserViewModel();
            mock.Setup(a => a.GetById(user.Id)).Returns(user);
            UserController controller = new UserController(mock.Object);

            // Act
            ViewResult result = controller.Details(user.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void CreateIndexViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IService>();
            
            
            UserController controller = new UserController(mock.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void EditViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IService>();
            UserViewModel user = new UserViewModel();
            mock.Setup(a => a.GetById(user.Id)).Returns(user);
            UserController controller = new UserController(mock.Object);

            // Act
            ViewResult result = controller.Edit(user.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void DeleteViewModelNotNull()
        {
            // Arrange
            var mock = new Mock<IService>();
            UserViewModel user = new UserViewModel();
            mock.Setup(a => a.GetById(user.Id)).Returns(user);
            UserController controller = new UserController(mock.Object);

            // Act
            ViewResult result = controller.Delete(user.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void GetAll()
        {
            List<User> testList = new List<User>();
            testList.Add(new User { Id = 1, FirstName = "Anton", LastName = "Reznyk", Age = 25 });

            //Arrange
            _personRepository.Setup(p => p.GetAll()).Returns(testList);

            //Act
            List<UserViewModel> actual = _service.GetAll() as List<UserViewModel>;

            //Assert           
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Count);
        }
    }
}
