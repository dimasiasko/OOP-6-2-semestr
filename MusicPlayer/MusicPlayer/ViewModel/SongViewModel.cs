using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using MusicPlayer.Annotations;
using MusicPlayer.Model;

namespace MusicPlayer.ViewModel
{
    public class SongViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Song> _library;
        private Song _selectedSong;

        private double _nowPlayingPosition;
        private double _selectedSongLength;
        private float _volume;
        private string _nowPlayingPositionString;
        private string _selectedSongLengthString;
        private bool _isnextrandom;
        private bool _isRepeat;

        private PackIconKind _iconKindButton;
        private MediaPlayer player = new MediaPlayer();
        private DispatcherTimer timer;

        public ObservableCollection<Song> Library
        {
            get { return _library; }
            set
            {
                if (Equals(value, _library))
                    return;

                _library = value;
                OnPropertyChanged(nameof(Library));
            }
        }

        public Song SelectedSong
        {
            get { return _selectedSong; }
            set
            {
                if (Equals(value, _selectedSong))
                    return;

                _selectedSong = value;
                OnPropertyChanged(nameof(SelectedSong));
            }
        }

        public double NowPlayingPosition
        {
            get { return _nowPlayingPosition; }
            set
            {
                if (Equals(value, _nowPlayingPosition))
                    return;

                _nowPlayingPosition = value;
                OnPropertyChanged(nameof(NowPlayingPosition));
            }
        }

        public double SelectedSongLength
        {
            get { return _selectedSongLength; }
            set
            {
                if (Equals(value, _selectedSongLength))
                    return;

                _selectedSongLength = value;
                OnPropertyChanged(nameof(SelectedSongLength));
            }
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                if (Equals(value, _volume))
                    return;

                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        public PackIconKind IconKindButton
        {
            get { return _iconKindButton; }
            set
            {
                if (Equals(value, _iconKindButton))
                    return;

                _iconKindButton = value;
                OnPropertyChanged(nameof(IconKindButton));
            }
        }

        public string NowPlayingPositionString
        {
            get { return _nowPlayingPositionString; }
            set
            {
                if (Equals(value, _nowPlayingPositionString))
                    return;

                _nowPlayingPositionString = value;
                OnPropertyChanged(nameof(NowPlayingPositionString));
            }
        }
        public string SelectedSongLengthString
        {
            get { return _selectedSongLengthString; }
            set
            {
                if (Equals(value, _selectedSongLengthString))
                    return;

                _selectedSongLengthString = value;
                OnPropertyChanged(nameof(SelectedSongLengthString));
            }
        }

        public bool IsNextRandom
        {
            get { return _isnextrandom; }
            set
            {
                if (Equals(value, _isnextrandom))
                    return;

                _isnextrandom = value;
                OnPropertyChanged(nameof(IsNextRandom));
            }
        }

        public bool IsRepeat
        {
            get { return _isRepeat; }
            set
            {
                if (Equals(value, _isRepeat))
                    return;

                _isRepeat = value;
                OnPropertyChanged(nameof(IsRepeat));
            }
        }

        public ICommand ShutdownCurrentCommand { get; set; }
        public ICommand ChooseSongCommand { get; set; }
        public ICommand PlayPauseSelectedCommand { get; set; }
        public ICommand PlayAudioCommand { get; set; }
        public ICommand ChangeVolumePositionCommand { get; set; }
        public ICommand ChangeSongPositionMdCommand { get; set; }
        public ICommand ChangeSongPositionMuCommand { get; set; }
        public ICommand StopCurrentCommand { get; set; }
        public ICommand DeleteAllTracksCommand { get; set; }
        public ICommand PlayPreviousCommand { get; set; }
        public ICommand PlayNextCommand { get; set; }
        public ICommand SaveLibraryCommand { get; set; }

        private enum PlayerState
        {
            Playing, Stopped, Paused
        }

        private PlayerState _playerState;

        private void ConstructCommands()
        {
            ShutdownCurrentCommand = new SongCommand(ShutdownCurrent, CanShutdownCurrent);
            ChooseSongCommand = new SongCommand(ChooseSong, CanChooseSong);
            PlayPauseSelectedCommand = new SongCommand(PlayPauseSelected, CanPlayPauseSelected);
            PlayAudioCommand = new SongCommand(PlayAudio, CanPlayAudio);
            ChangeVolumePositionCommand = new SongCommand(ChangeVolumePosition, CanChangeVolumePosition);
            ChangeSongPositionMdCommand = new SongCommand(ChangeSongPositionMd, CanChangeSongPositionMd);
            ChangeSongPositionMuCommand = new SongCommand(ChangeSongPositionMu, CanChangeSongPositionMu);
            StopCurrentCommand = new SongCommand(StopCurrent, CanStopCurrent);
            DeleteAllTracksCommand = new SongCommand(DeleteAllTracks, CanDeleteAllTracks);
            PlayPreviousCommand = new SongCommand(PlayPrevious, CanPlayPrevious);
            PlayNextCommand = new SongCommand(PlayNext, CanPlayNext);
            SaveLibraryCommand = new SongCommand(SaveLibrary, CanSaveLibrary);
        }

       
        public SongViewModel()
        {
            ConstructCommands();
            _playerState = PlayerState.Stopped;
            IconKindButton = PackIconKind.Play;

            SearchLibrary();
            

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            Volume = 1;
        }


        private void SearchLibrary()
        {
            string[] libraryPath = Directory.GetFiles(Environment.CurrentDirectory, "*.songlibrary");
            
            if (libraryPath.Length > 0)
                Library = new PathesLoader().LoadPathes(libraryPath[0]);
            else
                Library = new ObservableCollection<Song>();

        }
        private void ShutdownCurrent(object obj)
        {
            Application.Current.Shutdown();
        }
        private bool CanShutdownCurrent(object arg)
        {
            return true;
        }

        private void ChooseSong(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\"; 
            openFileDialog.Filter = "All Supported Audio (*.mp3..) | *.mp3; *.wav; *.wma; *.ogg;  | All files (*.*) | *.*";                  

            openFileDialog.Title = "Choose songs for play"; 
            
            openFileDialog.Multiselect = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                string[] songName = openFileDialog.FileNames;
                for (int i = 0; i < songName.Length; i++)
                {
                    switch (System.IO.Path.GetExtension(songName[i])) 
                    {
                        case ".mp3":
                        case ".wav":
                        case ".wma":
                        case ".ogg":
                            Song song = new Song(Path.GetFileNameWithoutExtension(songName[i]), songName[i]);
                            Library.Add(song);
                            break;
                        default:
                            MessageBox.Show($"File {songName[i]} is not in the correct format!");
                            break;
                    }
                }
            }
            else
                MessageBox.Show("Choose songs for play!");
        }
        
        private bool CanChooseSong(object arg)
        {
            return true;
        }

        private void PausePlayer()
        {
            player.Pause();
            _playerState = PlayerState.Paused;
            IconKindButton = PackIconKind.Play;
        }

        private void ResumePlayer()
        {
            player.Play();
            _playerState = PlayerState.Playing;
            IconKindButton = PackIconKind.Pause;
        }

        private void StopPlayer()
        {
            player.Stop();
            _playerState = PlayerState.Stopped;
            IconKindButton = PackIconKind.Play;
            NowPlayingPosition = 0;
            NowPlayingPositionString = player.GetNowPositionString();
        }

        private void PlayPauseSelected(object obj)
        {
            if (_playerState == PlayerState.Playing)
                PausePlayer();
            else if(_playerState == PlayerState.Paused)
                ResumePlayer();
            else
                PlayAudio(obj);
        }

        private void PlayAudio(object obj)
        {
            player.Close();
            if (SelectedSong != null)
            {
                player.Open(new Uri(SelectedSong.PathToSong, UriKind.RelativeOrAbsolute));
                player.Play();
                _playerState = PlayerState.Playing;
                IconKindButton = PackIconKind.Pause;
                timer.Start();
            }
            else
                return;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SelectedSongLength = player.GetSongPosition();
            SelectedSongLengthString = player.GetSongPositionString();

            if (_playerState == PlayerState.Playing)
            {
                NowPlayingPosition = player.GetNowPosition();
                NowPlayingPositionString = player.GetNowPositionString();
            }

            if (NowPlayingPosition == SelectedSongLength)
            {
                if (IsRepeat)
                    player.Repeat();
                else
                    PlayNext(sender);
            }

        }
        private bool CanPlayAudio(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }


        private bool CanPlayPauseSelected(object arg)
        {
            if (Library.Count !=0)
                return true;

            return false;
        }

        private void ChangeVolumePosition(object obj)
        {
            if (player.HasAudio)
                player.Volume = Volume;
        }
        private bool CanChangeVolumePosition(object arg)
        {
            return true;
        }

        private void ChangeSongPositionMd(object obj)
        {
            PausePlayer();
        }

        private bool CanChangeSongPositionMd(object arg)
        {
            if (SelectedSong != null)
                return true;
            else
                return false;
        }

        private void ChangeSongPositionMu(object obj)
        {
            player.Position = TimeSpan.FromSeconds(NowPlayingPosition);
            ResumePlayer();
        }

        private bool CanChangeSongPositionMu(object arg)
        {
            if (_playerState == PlayerState.Paused)
                return true;

            return false;
        }

        private void StopCurrent(object obj)
        {
            StopPlayer();
        }

        private bool CanStopCurrent(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }
        private void PlayNext(object obj)
        {
            if (IsNextRandom)
                SelectedSong = Library.RandomSongNext();
            else
                SelectedSong = Library.SelectNext(SelectedSong);
        }

        private bool CanPlayNext(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }

        private void PlayPrevious(object obj)
        {
            if (NowPlayingPosition >= 2)
                player.Repeat();
            else
                SelectedSong = Library.SelectPrevious(SelectedSong);
        }

        private bool CanPlayPrevious(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }

        private void SaveLibrary(object obj)
        {
            PathesSaver pathesSaver = new PathesSaver();
            pathesSaver.SavePathes(Library, Path.Combine(Environment.CurrentDirectory, "songs.songlibrary"));

            MessageBox.Show("Your songs was saved successfully");
        }

        private bool CanSaveLibrary(object arg)
        {
            return true;
        }


        private void DeleteAllTracks(object obj)
        {
            SystemSounds.Exclamation.Play();
            var result = MessageBox.Show("If you turn 'Yes', you will delete ALL tracks",
                "Are you really want to delete ALL tracks?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.Yes)
            {
                Library.Clear();
                player.Close();
                IconKindButton = PackIconKind.Play;
                MessageBox.Show("Songs was successfully deleted");
            }
            else
                return;
        }

        private bool CanDeleteAllTracks(object arg)
        {
            if (Library.Count != 0 && _playerState != PlayerState.Playing)
                return true;

            return false;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
