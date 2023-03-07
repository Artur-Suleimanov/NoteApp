using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NoteApp.BL.Model.User;
using NoteApp.BL.Controller.UserController;
using NoteApp.BL.Controller.NoteController;
using System.Resources;

namespace NoteApp.CMD
{
    public class GreetingSevice : IGreetingSevice
    {
        private readonly ILogger<GreetingSevice> _log;
        private readonly IConfiguration _config;
        private readonly IUserController _userController;
        private readonly INoteController _noteController;

        ResourceManager _resourceManager;


        public GreetingSevice(
            ILogger<GreetingSevice> log, 
            IConfiguration config,
            IUserController userController,
            INoteController noteController)
        {
            _log = log;
            _config = config;
            _userController = userController;
            _noteController = noteController;

            _resourceManager = new ResourceManager("NoteApp.CMD.Language.Messages", typeof(Program).Assembly);
        }

        /// <summary>
        /// Основной метод программы.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(_resourceManager.GetString("Hello"));

                    // Авторизация или добавление нового пользователя:
                    Autorization();

                    while (true)
                    {
                        Console.WriteLine(_resourceManager.GetString("What do you want to do?"));
                        Console.WriteLine(_resourceManager.GetString("AddNote"));
                        Console.WriteLine(_resourceManager.GetString("ShowAllNotes"));
                        Console.WriteLine(_resourceManager.GetString("DeleteNote"));
                        Console.WriteLine(_resourceManager.GetString("Exit"));

                        var key = Console.ReadKey();
                        Console.WriteLine();
                        Console.WriteLine();

                        _noteController.GetUserData(_userController.CurrentUser);

                        switch (key.Key)
                        {
                            case ConsoleKey.A:
                                AddNote();
                                break;

                            case ConsoleKey.S:
                                if (ShowAllNotes() == true) SelectNotes();
                                break;

                            case ConsoleKey.D:
                                if (ShowAllNotes() == true) DeleteNote();
                                break;

                            case ConsoleKey.Q:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine(_resourceManager.GetString("IncorrectAnswer"));
                                break;
                        }
                    }
                }
                catch(Exception ex)
                {
                    _log.LogError(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Удаление заметки.
        /// </summary>
        private void DeleteNote()
        {
            try
            {
                Console.Write(_resourceManager.GetString("NoteNumberToDelete"));
                var userInput = Convert.ToInt32(Console.ReadLine());
                _noteController.DeleteNote(userInput);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                _log.LogError($"При выборе заметки возникла ошибка:\n {ex.Message}");
                Console.WriteLine(_resourceManager.GetString("IncorrectAnswer"));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Выбрать заметку для просмотра.
        /// </summary>
        private void SelectNotes()
        {
            Console.Write(_resourceManager.GetString("SelectNote"));

            try
            {
                var inputNoteNumber = Convert.ToInt32(Console.ReadLine());
                var selectedNote = _noteController.GetCurrentUserNoteBook().Notes[inputNoteNumber - 1];
                Console.WriteLine();
                Console.WriteLine(selectedNote.Title);
                Console.WriteLine(selectedNote.Text);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                _log.LogError($"При удалении заметки возникла ошибка:\n {ex.Message}");
                Console.WriteLine(_resourceManager.GetString("IncorrectAnswer"));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Показать список заметок пользователя.
        /// </summary>
        /// <returns>true - заметки есть, false - заметок нет.</returns>
        private bool ShowAllNotes()
        {
            if (_noteController.GetCurrentUserNoteBook().Notes.Count > 0)
            {
                for (int i = 0; i < _noteController.GetCurrentUserNoteBook().Notes.Count; i++)
                {
                    Console.WriteLine($"{i+1} - {_noteController.GetCurrentUserNoteBook().Notes[i]}");
                }
                Console.WriteLine();
                return true;
            }
            else
            {
                Console.WriteLine(_resourceManager.GetString("NoNotes"));
                Console.WriteLine();
                return false;
            }
            
        }

        /// <summary>
        /// Добавление заметки.
        /// </summary>
        private void AddNote()
        {
            string noteTitle = null;
            string noteText = null;
            while (true)
            {
                Console.Write(_resourceManager.GetString("EnterNoteTitle") + ": ");
                noteTitle = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(noteTitle) == true)
                {
                    Console.WriteLine(_resourceManager.GetString("ErrorNoteTitle"));
                }
                else break;
            }
            
            Console.WriteLine();

            while (true)
            {
                Console.Write(_resourceManager.GetString("EnterNoteText") + ": ");
                noteText = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(noteText) == true)
                {
                    Console.WriteLine(_resourceManager.GetString("ErrorNoteText")); 
                }
                else break;
            }
            _noteController.Add(noteTitle, noteText);
            Console.WriteLine();
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        private void Autorization()
        {
            while (true)
            {
                try
                {
                    Console.Write(_resourceManager.GetString("EnterName") + ": ");
                    var inputName = Console.ReadLine();
                    _userController.FillFields(inputName);

                    if(_userController.CurrentUser == null)
                    {
                        Console.WriteLine(_resourceManager.GetString("NoUser"));
                        Console.WriteLine($"{_resourceManager.GetString("AddUser?")} \"{inputName}\"?");

                        if (AskAboutAddingNewUser())
                        {
                            _userController.AddNewUser(inputName);
                            _log.LogInformation("Добавлен новый пользователь");
                            break;
                        }
                    }
                    else break;
                }
                catch (ArgumentException ex)
                {
                    _log.LogError(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Проводит отпрос: добавить ли нового пользователя. Если true, то добавляется.
        /// </summary>
        /// <param name="inputName">Введенное значение</param>
        private bool AskAboutAddingNewUser()
        {
            while (true)
            {
                Console.Write($"{_resourceManager.GetString("Yes")}, {_resourceManager.GetString("No")}: ");
                var inputAnswer = Console.ReadLine();
                if (inputAnswer.ToLower() == _resourceManager.GetString("Yes").ToLower())
                    return true;

                else if (inputAnswer.ToLower() == _resourceManager.GetString("No").ToLower())
                    return false;

                else
                    Console.WriteLine(_resourceManager.GetString("IncorrectAnswer"));
            }
        }
    }
}