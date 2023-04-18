using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NoteApp.BL.Controller.NoteController;
using NoteApp.BL.Controller.UserController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteApp.WPF;

public partial class MainWindow : Window, IMainWindow
{
    private readonly ILogger<MainWindow> _log;
    private readonly IConfiguration _config;
    private readonly IUserController _userController;
    private readonly INoteController _noteController;

    public ResourceManager ResourceManager { get; }

    public string InputName { get; set; }
    public INoteController NoteController => _noteController;

    public BindingList<string> NoteTitles { get => noteTitles; }

    BindingList<string> noteTitles = new BindingList<string>();

    public int SelectedNoteIndex { get; private set; } = -1;

    public enum Buttons
    {
        EditNote,
        AddNewNote,
    }

    public MainWindow(
            ILogger<MainWindow> log,
            IConfiguration config,
            IUserController userController,
            INoteController noteController)
    {
        InitializeComponent();

        _log = log;
        _config = config;
        _userController = userController;
        _noteController = noteController;

        ResourceManager = new ResourceManager("NoteApp.WPF.Language.Messages", typeof(App).Assembly);

        if (string.IsNullOrWhiteSpace(userNameTextBlock.Text))
        {
            ChangeButtonEnabled(false);
        }

        #region Задание текста кнопкам и текстовым блокам
        this.Title = ResourceManager.GetString("MainWindowTitle");
        notesListTextBlock.Text = ResourceManager.GetString("NotesList");
        logInButton.Content = ResourceManager.GetString("LogIn");
        changeTextBlock.Text = ResourceManager.GetString("ChangeUser").Split(" ")[0];
        userTextBlock.Text = ResourceManager.GetString("ChangeUser").Split(" ")[1];
        deleteButton.Content = ResourceManager.GetString("Delete");
        editButton.Content = ResourceManager.GetString("Edit");
        addButton.Content = ResourceManager.GetString("Add");
        #endregion

    }

    private void notesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ListBox listBox = (ListBox)sender;

        if(listBox.SelectedItem != null)
        {
            var noteTitle = listBox.SelectedItem.ToString();

            noteText.Text = _noteController.GetCurrentUserNoteBook().Notes.SingleOrDefault(note => note.Title == noteTitle).Text;

            deleteButton.IsEnabled = true;
            SelectedNoteIndex = _noteController.GetCurrentUserNoteBook().Notes.FindIndex(note => note.Title == noteTitle);
        }
        else 
        {
            noteText.Text = string.Empty;
            deleteButton.IsEnabled = false;
            editButton.IsEnabled = false;
            SelectedNoteIndex = -1;
        }
    }

    private void logInButton_Click(object sender, RoutedEventArgs e)
    {
        while (true)
        {
            new AutorizationWindow(this).ShowDialog();

            if (string.IsNullOrWhiteSpace(InputName))
                return;
           
            _userController.FillFields(InputName);

            if (_userController.CurrentUser == null)
            {
                var userAnswer = MessageBox.Show(ResourceManager.GetString("AddUser?"), "NoteApp", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (userAnswer == MessageBoxResult.Yes)
                {
                    _userController.AddNewUser(InputName);
                    _log.LogInformation("Добавлен новый пользователь");
                    break;
                }
                else 
                {
                    InputName = String.Empty;
                    continue;
                }
                
            }
            else break;
        }

        userNameTextBlock.Text = InputName;
        ChangeButtonEnabled(true);

        _userController.FillFields(InputName);
        _noteController.GetUserData(_userController.CurrentUser!);

        var userNoteBook = _noteController.GetCurrentUserNoteBook();

        notesList.ItemsSource = NoteTitles;

        foreach (var note in userNoteBook.Notes)
        {
            NoteTitles.Add(note.Title);
        }
    }

    private void addButton_Click(object sender, RoutedEventArgs e)
    {
        new NewNoteWindow(this, ResourceManager, Buttons.AddNewNote).Show();
        
    }

    private void ChangeButtonEnabled(bool argument)
    {
        changeUserButton.IsEnabled = argument;
        addButton.IsEnabled = argument;
        editButton.IsEnabled = argument;
        deleteButton.IsEnabled = argument;
        logInButton.IsEnabled = !argument;
    }

    private void editButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedNoteIndex == -1)
            return;

        new NewNoteWindow(this, ResourceManager, Buttons.EditNote).Show();
    }

    private void deleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedNoteIndex == -1)
            return;

        _noteController.DeleteNote(SelectedNoteIndex);

        NoteTitles.RemoveAt(SelectedNoteIndex);
    }
}
