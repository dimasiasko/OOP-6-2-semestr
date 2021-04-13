using System.ComponentModel;
using System.Runtime.CompilerServices;
using MusicPlayer.Annotations;

namespace MusicPlayer.Model
{
    public class Song : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
