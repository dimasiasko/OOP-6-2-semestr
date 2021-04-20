using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{ // клас для завантаження збереженої бібліотеки аудіо-файлів
    public class PathesLoader
    {
        public ObservableCollection<Song> LoadPathes(string path) // метод повертає колекцію типу Song
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                ObservableCollection<Song> songs = new ObservableCollection<Song>();

                string line;

                while ((line = streamReader.ReadLine()) != null) // зчитування кожного рядка з файлу
                    songs.Add(new Song(System.IO.Path.GetFileNameWithoutExtension(line), line)); 

                return songs;
            }
        }
    }
}
