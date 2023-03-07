using System.Resources;

namespace NoteApp.WPF
{
    public interface IMainWindow
    {
        void InitializeComponent();
        string InputName { get; set; }

        public ResourceManager ResourceManager { get; }
    }
}