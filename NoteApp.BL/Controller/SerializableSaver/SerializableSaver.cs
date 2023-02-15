using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.BL.Controller.SerializableSaver
{
    /// <summary>
    /// Базовый класс для всех контроллеров. В этом классе описаны метода сохранения и загрузки информации.
    /// </summary>
    public abstract class SerializableSaver : ISerializableSaver
    {
        /// <summary>
        /// Получение данных из .dat файла.
        /// </summary>
        /// <typeparam name="T">Класс.</typeparam>
        /// <returns></returns>
        public List<T> Load<T>() where T : class
        {
            var formatter = new BinaryFormatter();
            var fileName = typeof(T).Name;

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<T> items)
                {
                    return items;
                }
                else
                {
                    return new List<T>();
                }
            }

        }

        /// <summary>
        /// Сериализация данных в файл.
        /// </summary>
        /// <typeparam name="T">Класс.</typeparam>
        /// <param name="item">Экземпляп класса T.</param>
        public void Save<T>(List<T> item) where T : class
        {
            var formatter = new BinaryFormatter();
            var fileName = typeof(T).Name;

            using (var fr = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fr, item);
            }
        }


        public void Save<T>(T item) where T : class
        {
            var formatter = new BinaryFormatter();
            var fileName = typeof(T).Name;

            using (var fr = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fr, item);
            }
        }
    }
}
