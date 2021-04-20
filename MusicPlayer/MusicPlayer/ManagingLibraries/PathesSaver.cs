using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{ // клас збереження бібліотеки
    public class PathesSaver
    {
        public void SavePathes(ICollection<Song> library, string pathSave) // приймає об'єкт інтерфейсу ICollection
        {
            using (StreamWriter writer = new StreamWriter(pathSave)) // StreamWriter об'єкт для запису
            {
                foreach (var song in library)
                    writer.WriteLine(song.PathToSong); // записує в кожний новий рядок шлях до файлу
            }
        }
    }
}
