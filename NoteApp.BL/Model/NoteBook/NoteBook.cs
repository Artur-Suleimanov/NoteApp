using NoteApp.BL.Model.Note;
using NoteApp.BL.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Model.NoteBook
{
    [Serializable]
    public class NoteBook : INoteBook
    {
        private List<INote> _notes;
        private IUser _user;

        /// <summary>
        /// Пользователь.
        /// </summary>
        public IUser User => _user;

        /// <summary>
        /// Список заметок пользователя.
        /// </summary>
        public List<INote> Notes => _notes;

        /// <summary>
        /// Заполнение данных пользователя.
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void FillProperties(IUser user)
        {
            _user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым.", nameof(user));

            _notes = new List<INote>();
        }

        /// <summary>
        /// Добавление заметки.
        /// </summary>
        /// <param name="note"></param>
        public void Add(INote note)
        {
            _notes.Add(note);
        }
    }
}
