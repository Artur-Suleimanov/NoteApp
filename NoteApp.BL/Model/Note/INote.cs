namespace NoteApp.BL.Model.Note
{
    public interface INote
    {
        string Text { get; set; }
        string Title { get; set; }

        string ToString();
    }
}