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
    public partial class AutorizationWindow : Window
    {
        private readonly IMainWindow _mainWindow;

        public AutorizationWindow(IMainWindow mainWindow)
        {
            InitializeComponent();

            if(string.IsNullOrWhiteSpace(userInput.Text))
                okButton.IsEnabled = false;
            _mainWindow = mainWindow;

            this.Title = _mainWindow.ResourceManager.GetString("AutorizationWindowTitle");
            okButton.Content = _mainWindow.ResourceManager.GetString("Ok");
            cancelButton.Content = _mainWindow.ResourceManager.GetString("Cancel");
            enterNameTextBlock.Text = _mainWindow.ResourceManager.GetString("EnterName");

            userInput.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            userInput.Text = String.Empty;
            this.Close();
        }

        private void userInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
                okButton.IsEnabled = false;
            else
                okButton.IsEnabled = true;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.InputName = userInput.Text;
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(okButton.IsEnabled)
                {
                    _mainWindow.InputName = userInput.Text;
                    this.Close();
                }
            }
        }
    }
}
