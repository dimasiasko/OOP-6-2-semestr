using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{
    public static class NextPreviousExtension
    {
        public static Song SelectNext<Song>(this ObservableCollection<Song> library, Song selected)
        {
            int selectedNow = library.IndexOf(selected);

            if (selectedNow < library.Count - 1)
                return library[selectedNow + 1];

            return library[0];
        }

        public static Song SelectPrevious<Song>(this ObservableCollection<Song> library, Song selected)
        {
            int selectedNow = library.IndexOf(selected);

            if (selectedNow > 0)
                return library[selectedNow - 1];

            return library[library.Count - 1];
        }

        public static Song RandomSongNext<Song>(this ObservableCollection<Song> library)
        {
            Random random = new Random();
            return library[random.Next(0, library.Count - 1)];
        }
    }
}
