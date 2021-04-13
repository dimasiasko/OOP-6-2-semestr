using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{
    public class PathesSaver
    {
        public void SavePathes(ICollection<Song> library, string pathSave)
        {
            using (StreamWriter writer = new StreamWriter(pathSave))
            {
                foreach (var song in library)
                {
                    writer.WriteLine(song.PathToSong);
                }
            }
        }
    }
}
