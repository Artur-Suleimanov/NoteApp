using NoteApp.BL.Controller.NoteController;
using System.ComponentModel;
using System.Resources;

namespace NoteApp.WPF
{
    public interface IMainWindow
    {
        void InitializeComponent();
        string InputName { get; set; }
        INoteController NoteController { get; }
        BindingList<string> NoteTitles { get; }

        public ResourceManager ResourceManager { get; }
    }
}