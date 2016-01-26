using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace Person_Phone.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
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
    }
}
