
using NoteApp.BL.Controller.SerializableSaver;
using NoteApp.BL.Model.Note;
using NoteApp.BL.Model.NoteBook;
using NoteApp.BL.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Controller.NoteController
{
    [Serializable]
    public class NoteController : SerializableSaver.SerializableSaver, INoteController
    {
        private IUser user;
        private List<INoteBook> _noteBooks;
        //private INoteBook _newNoteBook;

        /// <summary>
        /// Записная книжка нового пользователя.
        /// </summary>
        //public INoteBook NewNoteBook => _newNoteBook;

        /// <summary>
        /// Записные книжки всех пользователей.
        /// </summary>
        public List<INoteBook> NoteBooks => _noteBooks;

        /// <summary>
        /// Получение записной книжки пользователя.
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void GetUserData(IUser user)
        {
            this.user = user ?? throw new ArgumentNullException("Пользователь не может быть пустым.", nameof(user));

            GetUserNoteBook();
        }

        /// <summary>
        /// Добавление заметки в записную книжку пользователя.
        /// </summary>
        /// <param name="note">Заметка</param>
        public void Add(string title, string text)
        {
            var note = new Note(title, text);

            _noteBooks.SingleOrDefault(nb => nb.User.Name == user.Name)
                .Add(note);

            Save();
        }

        /// <summary>
        /// Получить записную книжку текущего пользователя (Для UI).
        /// </summary>
        /// <returns></returns>
        public INoteBook GetCurrentUserNoteBook()
        {
            return _noteBooks.SingleOrDefault(nb => nb.User.Name == user.Name);
        }

        /// <summary>
        /// Удаление заметки.
        /// </summary>
        /// <param name="noteNumber">Номер заметки, которую нужно удалить.</param>
        public void DeleteNote(int noteNumber)
        {
            GetCurrentUserNoteBook().Notes.RemoveAt(noteNumber - 1);
            Save();
        }

        /// <summary>
        /// Получение записной книжки пользователя. Если записная кникжка уже есть, то поле _newNoteBook останеться пустым.
        /// </summary>
        /// <returns></returns>
        private void GetUserNoteBook()
        {
            _noteBooks = Load<INoteBook>() ?? new List<INoteBook>();

            var noteBook = _noteBooks.SingleOrDefault(nb => nb.User.Name == user.Name);

            if (noteBook == null)
            {
                noteBook = new NoteBook();
                noteBook.FillProperties(user);
                _noteBooks.Add(noteBook);
                Save();
            }
        }

        /// <summary>
        /// Сохранение записной книжки пользователя.
        /// </summary>
        private void Save()
        {
            Save(_noteBooks);
        }
    }
}
