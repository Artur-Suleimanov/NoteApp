//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NoteApp.BL.Controller.NoteController;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NoteApp.BL.Controller.UserController;
//using NoteApp.BL.Model.Note;

//namespace NoteApp.BL.Controller.NoteController.Tests
//{
//    [TestClass()]
//    public class NoteControllerTests
//    {
//        [TestMethod()]
//        public void AddTest()
//        {
//            // Arrange
//            var userName = "test3";
//            var userController = new UserController.UserController();
//            userController.FillFields(userName);

//            // Act
//            var noteBookController = new NoteController();
//            var note = new Note(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
//            noteBookController.GetUserData(userController.CurrentUser);
//            noteBookController.Add(note);

//            // Assert
//            var userNoteBook = noteBookController.NoteBooks.SingleOrDefault(nb => nb.User.Name == userName);
//            Assert.AreEqual(note, userNoteBook.Notes.SingleOrDefault(n => n.Title == note.Title));
//        }
//    }
//}