using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Model.User
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Serializable]
    public class User : IUser
    {
        private string _name;

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Создание нового пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя</param>
        /// <exception cref="ArgumentException"></exception>
        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Имя пользователя не может быть пустым или содержать только пробел.", nameof(name));
            }

            _name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
