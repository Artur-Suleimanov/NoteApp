using NoteApp.BL.Model.Note;
using NoteApp.BL.Model.User;

namespace NoteApp.BL.Model.NoteBook
{
    public interface INoteBook
    {
        IUser User { get; }
        List<INote> Notes { get; }

        void Add(INote note);
        void FillProperties(IUser user);
    }
}