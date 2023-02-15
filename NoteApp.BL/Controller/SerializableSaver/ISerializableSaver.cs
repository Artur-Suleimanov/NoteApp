namespace NoteApp.BL.Controller.SerializableSaver
{
    public interface ISerializableSaver
    {
        List<T> Load<T>() where T : class;
        void Save<T>(List<T> item) where T : class;
    }
}