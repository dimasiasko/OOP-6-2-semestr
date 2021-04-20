using System.ComponentModel;
using System.Runtime.CompilerServices;
using MusicPlayer.Annotations;

namespace MusicPlayer.Model
{
    // модель програми
    public class Song : INotifyPropertyChanged //Успадковування від інтерфейсу INotifyPropertyChanged та реалізація його
    { // поля та властивості імені та шляху
        private string _songName;
        private string _pathToSong;

        public string SongName
        {
            get { return _songName; }
            set
            {
                if (Equals(value,_songName))
                {
                    return;
                }

                _songName = value;
                // означає що система буде оновлювати всі прив'язки як тільки зміняться дані які повертаються
                OnPropertyChanged(nameof(SongName)); 
            }
        }

        public string PathToSong
        {
            get { return _pathToSong; }
            set
            {
                if (Equals(value, _pathToSong)) 
                {
                    return;
                }

                _pathToSong = value;
                OnPropertyChanged(nameof(PathToSong));
            }
        }

        public Song(string name, string path)
        {
            PathToSong = path;
            SongName = name;
        }

        // реалізація інтерфейсу
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
