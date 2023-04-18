using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApp.BL.Model.User;

namespace NoteApp.BL.Model.Note
{
    [Serializable]
    public class Note : INote
    {
        private string _text;
        private string _title;

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string Text { get { return _text; } set { _text = value; } }

        /// <summary>
        /// Название заметки.
        /// </summary>
        public string Title { get { return _title; } set { _title = value; } }

        /// <summary>
        /// Создание заметки.
        /// </summary>
        /// <param name="title">Название заметки</param>
        /// <param name="text">Текст заметки</param>
        /// <exception cref="ArgumentException"></exception>
        public Note(string title, string text)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"Заголовок заметки не может быть пустым или содержать только пробел.", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"Текст заметки не может быть пустым или содержать только пробел.", nameof(text));
            }

            _title = title;
            _text = text;
        }

        public override string ToString()
        {
            return _title;
        }
    }
}
