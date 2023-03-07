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
        public NewNoteWindow()
        {
            InitializeComponent();

            noteTitle.Foreground = Brushes.Gray;
            noteTitle.Text = "Заголовок";
        }

        private void noteTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void noteTitle_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(noteTitle.IsFocused == false)
            {
                
            }
            else
            {
                noteTitle.Foreground = Brushes.Black;
                noteTitle.Text = String.Empty;
            }
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
            if (noteTitle.Text == "Заголовок")
            {
                noteTitle.Foreground = Brushes.Black;
                noteTitle.Text = String.Empty;
            }
        }
    }
}
