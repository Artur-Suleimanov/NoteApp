using NoteApp.BL.Model.User;

namespace NoteApp.BL.Controller.UserController
{
    public interface IUserController
    {
        User? CurrentUser { get; }
        List<User> Users { get; }

        bool AddNewUser(string userName);
        void FillFields(string userName);
    }
}