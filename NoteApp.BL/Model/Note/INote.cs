namespace NoteApp.BL.Model.Note
{
    public interface INote
    {
        string Text { get; }
        string Title { get; }

        string ToString();
    }
}