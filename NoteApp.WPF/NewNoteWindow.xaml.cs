using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static NoteApp.WPF.MainWindow;

namespace NoteApp.WPF
{
    public partial class NewNoteWindow : Window
    {
        private readonly IMainWindow _mainWindow;
        public ResourceManager ResourceManager { get; }

        Buttons Button { get; }

        public NewNoteWindow(IMainWindow mainWindow, ResourceManager resourceManager, Buttons button)
        {
            InitializeComponent();

            ResourceManager = resourceManager;
            Button = button;

            if (Button == Buttons.AddNewNote)
            {
                noteTitle.Foreground = Brushes.Gray;
                noteText.Foreground = Brushes.Gray;
                this.Title = ResourceManager.GetString("NewNote");
                noteTitle.Text = ResourceManager.GetString("Title");
                noteText.Text = ResourceManager.GetString("Text");
                addButton.Content = ResourceManager.GetString("Add");
                cancelButton.Content = ResourceManager.GetString("Cancel");
            }
            else if(Button == Buttons.EditNote)
            {
                this.Title = ResourceManager.GetString("EditNote");
                noteTitle.Text = mainWindow.NoteController.GetCurrentUserNoteBook().Notes[mainWindow.SelectedNoteIndex].Title;
                noteText.Text = mainWindow.NoteController.GetCurrentUserNoteBook().Notes[mainWindow.SelectedNoteIndex].Text;
                addButton.Content = ResourceManager.GetString("Ok");
                cancelButton.Content = ResourceManager.GetString("Cancel");
            }


            
            _mainWindow = mainWindow;

            
        }

        private void noteTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(noteTitle.Text))
            {
                noteTitle.Foreground = Brushes.Gray;
                noteTitle.Text = "Заголовок";
                
            }
        }

        private void noteTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            if (noteTitle.Foreground == Brushes.Gray)
            {
                noteTitle.Foreground = Brushes.Black;
                noteTitle.Text = String.Empty;
            }
        }

        private void textBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (noteTitle.Foreground != Brushes.Gray && noteText.Foreground != Brushes.Gray && !string.IsNullOrWhiteSpace(noteTitle.Text) && !string.IsNullOrWhiteSpace(noteText.Text))
                addButton.IsEnabled = true;

            else
                addButton.IsEnabled = false;

        }

        private void noteText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(noteText.Text))
            {
                noteText.Foreground = Brushes.Gray;
                noteText.Text = "Текст";
            }
        }

        private void noteText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (noteText.Foreground == Brushes.Gray)
            {
                noteText.Foreground = Brushes.Black;
                noteText.Text = String.Empty;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (Button == Buttons.AddNewNote)
            {
                _mainWindow.NoteController.Add(noteTitle.Text, noteText.Text);
                _mainWindow.NoteTitles.Add(noteTitle.Text);
                this.Close();
            }
            else if (Button == Buttons.EditNote)
            {
                var indexOfTitle = _mainWindow.NoteTitles.IndexOf(_mainWindow.NoteController.GetCurrentUserNoteBook().Notes[_mainWindow.SelectedNoteIndex].Title);

                _mainWindow.NoteController.EditNote(_mainWindow.SelectedNoteIndex, noteTitle.Text, noteText.Text);
                _mainWindow.NoteTitles[indexOfTitle] = noteTitle.Text;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
