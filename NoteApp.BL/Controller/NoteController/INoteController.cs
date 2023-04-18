using NoteApp.BL.Model.Note;
using NoteApp.BL.Model.NoteBook;
using NoteApp.BL.Model.User;

namespace NoteApp.BL.Controller.NoteController
{
    public interface INoteController
    {
        //INoteBook NewNoteBook { get; }
        List<INoteBook> NoteBooks { get; }

        void Add(string title, string text);
        INoteBook GetCurrentUserNoteBook();
        void GetUserData(IUser user);
        void DeleteNote(int noteNumber);
        void EditNote(int noteNumber, string newTitle, string newText);
    }
}