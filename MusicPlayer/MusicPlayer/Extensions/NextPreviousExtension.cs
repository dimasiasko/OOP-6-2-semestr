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

        public static int FirstItem = 0;
        public static Song SelectNext<Song>(this ObservableCollection<Song> library, Song selected)
        {

            int lastSelectedItem = library.Count - 1;
            int selectedNow = library.IndexOf(selected);
            int selectNext = selectedNow + 1;
            

            if (selectedNow < lastSelectedItem) // якщо наступного немає - вибір першого
                return library[selectNext];

            return library[FirstItem];
        }
        // метод вибору попереднього елементу в колекції
        public static Song SelectPrevious<Song>(this ObservableCollection<Song> library, Song selected)
        {
            int selectedNow = library.IndexOf(selected);
            int selectPrevious = selectedNow - 1;
            int lastSelectedItem = library.Count - 1;

            if (selectedNow > FirstItem) // якщо попереднього немає - вибір останнього
                return library[selectPrevious];

            return library[lastSelectedItem];
        }
        // метод перемішування колекції (повернення випадково вибраного елемента)
        public static Song RandomSongNext<Song>(this ObservableCollection<Song> library)
        {
            int lastSelectedItem = library.Count - 1;
            Random random = new Random();

            return library[random.Next(FirstItem, lastSelectedItem)];
            
           
        }
    }
}
