using Microsoft.VisualStudio.TestTools.UnitTesting;
using NoteApp.BL.Controller.UserController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Controller.UserController.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void FillFieldsTest()
        {
            // Arrange
            var userName = Guid.NewGuid().ToString();
            var userController = new UserController();

            // Act
            userController.FillFields(userName);


            // Assert
            Assert.AreEqual(null, userController.CurrentUser);
        }

        [TestMethod()]
        public void FillFieldsTest2()
        {
            // Arrange
            var userName = "test3";
            var userController = new UserController();
            userController.AddNewUser(userName);

            // Act
            userController.FillFields(userName);


            // Assert
            Assert.AreEqual(userName, userController.CurrentUser.Name);
        }


        [TestMethod()]
        public void AddNewUserTest()
        {
            // Arrange
            var userName = Guid.NewGuid().ToString();
            var userController = new UserController();

            // Act
            userController.AddNewUser(userName);

            // Assert
            Assert.AreEqual(userName, userController.CurrentUser.Name);
        }
    }
}