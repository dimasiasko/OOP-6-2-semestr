using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{
    public class PathesLoader
    {
        public ObservableCollection<Song> LoadPathes(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                ObservableCollection<Song> songs = new ObservableCollection<Song>();

                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    songs.Add(new Song(System.IO.Path.GetFileNameWithoutExtension(line), line));
                }

                return songs;
            }
        }
    }
}
