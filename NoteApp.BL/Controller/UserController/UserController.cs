using NoteApp.BL.Controller.SerializableSaver;
using NoteApp.BL.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Controller.UserController
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController : SerializableSaver.SerializableSaver, IUserController
    {
        private User? _currentUser = null;
        private List<User> _users;

        /// <summary>
        /// Список пользователей приложения.
        /// </summary>
        public List<User> Users => _users;
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public User? CurrentUser => _currentUser;

        /// <summary>
        /// Создание контроллера пользователя.
        /// </summary>
        public UserController()
        {
            _users = GetUsersData();
        }

        /// <summary>
        /// Заполнение полей класса.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <exception cref="ArgumentException"></exception>
        public void FillFields(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException($"Имя пользователя не может быть пустым или содержать только пробел.", nameof(userName));
            }

            _currentUser = Users.SingleOrDefault(u => u.Name == userName);
        }

        /// <summary>
        /// Получение списка зарегистрированных пользователей.
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData()
        {
            return Load<User>() ?? new List<User>();
        }

        /// <summary>
        /// Добавление нового пользователя. Если пользователь добавлен, то true. Если такой пользователь уже существует, то false.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        public bool AddNewUser(string userName)
        {
            var isNameTaken = IsNameTaken(userName);

            if(IsNameTaken(userName) == false)
            {
                Users.Add(new User(userName));
                _currentUser = Users.SingleOrDefault(u => u.Name == userName);
                Save();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверяет наличие имени в списке пользователей. Если имя не найдено, то false.
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <returns></returns>
        private bool IsNameTaken(string userName)
        {
            var user = Users.FirstOrDefault(u => u.Name == userName);

            if (user == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Сохранение пользователей.
        /// </summary>
        private void Save()
        {
            Save(Users);
        }
    }
}
