using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayer.Model;

namespace MusicPlayer
{ // клас який розширює стандартні можливості ObservableCollection<>
    public static class NextPreviousExtension
    { // метод вибору наступного елементу в колекції
        public static Song SelectNext<Song>(this ObservableCollection<Song> library, Song selected)
        {
            int selectedNow = library.IndexOf(selected);

            if (selectedNow < library.Count - 1) // якщо наступного немає - вибір першого
                return library[selectedNow + 1];

            return library[0];
        }
        // метод вибору попереднього елементу в колекції
        public static Song SelectPrevious<Song>(this ObservableCollection<Song> library, Song selected)
        {
            int selectedNow = library.IndexOf(selected);

            if (selectedNow > 0) // якщо попереднього немає - вибір останнього
                return library[selectedNow - 1];

            return library[library.Count - 1];
        }
        // метод перемішування колекції (повернення випадково вибраного елемента)
        public static Song RandomSongNext<Song>(this ObservableCollection<Song> library)
        {
            Random random = new Random();
            return library[random.Next(0, library.Count - 1)];
        }
    }
}
