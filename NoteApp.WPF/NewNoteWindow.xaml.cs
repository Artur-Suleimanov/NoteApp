using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteApp.WPF
{
    public partial class NewNoteWindow : Window
    {
        private readonly IMainWindow _mainWindow;

        public NewNoteWindow(IMainWindow mainWindow)
        {
            InitializeComponent();

            noteTitle.Foreground = Brushes.Gray;
            noteTitle.Text = "Заголовок";

            noteText.Foreground = Brushes.Gray;
            noteText.Text = "Текст";
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
            
            _mainWindow.NoteController.Add(noteTitle.Text, noteText.Text);
            _mainWindow.NoteTitles.Add(noteTitle.Text);
            this.Close();

        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
