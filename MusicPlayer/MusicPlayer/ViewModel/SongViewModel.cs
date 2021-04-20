using System;
using System.Collections.Generic;
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
    // ViewModel програми
    //Успадковування від інтерфейсу INotifyPropertyChanged та реалізація його
    public class SongViewModel : INotifyPropertyChanged
    {
        // створення приватних полів
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

        // створення публічних властивостей для їх прив'язки на основі приватних полів
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

       

        // створення властивостей типу ICommand
        public ICommand ShutdownCurrentCommand { get; set; }
        public ICommand ChooseSongCommand { get; set; }
        public ICommand PlayPauseSelectedCommand { get; set; }
        public ICommand PlayAudioCommand { get; set; }
        public ICommand ChangeVolumePositionCommand { get; set; }
        public ICommand ChangeSongPositionMdCommand { get; set; }
        public ICommand ChangeSongPositionMuCommand { get; set; }
        public ICommand StopCurrentCommand { get; set; }
        public ICommand DeleteAllSongsCommand { get; set; }
        public ICommand PlayPreviousCommand { get; set; }
        public ICommand PlayNextCommand { get; set; }
        public ICommand SaveLibraryCommand { get; set; }

        private enum PlayerState // перечислення станів програвання
        {
            Playing, Stopped, Paused
        }

        private PlayerState _playerState;

        private void ConstructCommands() // метод для конструювання команд, прив'язки до методів
        { // UpCast приведення до інтерфейсу ICommand
            ShutdownCurrentCommand = new SongCommand(ShutdownCurrent, CanShutdownCurrent);
            ChooseSongCommand = new SongCommand(ChooseSong, CanChooseSong);
            PlayPauseSelectedCommand = new SongCommand(PlayPauseSelected, CanPlayPauseSelected);
            PlayAudioCommand = new SongCommand(PlayAudio, CanPlayAudio);
            ChangeVolumePositionCommand = new SongCommand(ChangeVolumePosition, CanChangeVolumePosition);
            ChangeSongPositionMdCommand = new SongCommand(ChangeSongPositionMd, CanChangeSongPositionMd);
            ChangeSongPositionMuCommand = new SongCommand(ChangeSongPositionMu, CanChangeSongPositionMu);
            StopCurrentCommand = new SongCommand(StopCurrent, CanStopCurrent);
            DeleteAllSongsCommand = new SongCommand(DeleteAllSongs, CanDeleteAllSongs);
            PlayPreviousCommand = new SongCommand(PlayPrevious, CanPlayPrevious);
            PlayNextCommand = new SongCommand(PlayNext, CanPlayNext);
            SaveLibraryCommand = new SongCommand(SaveLibrary, CanSaveLibrary);
        } // методи Can... визначають умову чи може бути виконанан команда

       
        public SongViewModel() // конструктор, визначеємо стандартні значення
        {
            ConstructCommands();
            _playerState = PlayerState.Stopped;
            IconKindButton = PackIconKind.Play;

            SearchLibrary(); // пошук збереженого плейлисту
            

            timer = new DispatcherTimer(); // таймер для оновлення
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick; // прив'язка методу який спрацьовуватиме кожну секунду

            Volume = 1; // звук за замовчуванням 1
        }


        private void SearchLibrary() // пошук при запуску програми збереженого плейлисту
        {
            string[] libraryPath = Directory.GetFiles(Environment.CurrentDirectory, "*.songlibrary");
            
            if (libraryPath.Length > 0)
                Library = new PathesLoader().LoadPathes(libraryPath[0]);
            else
                Library = new ObservableCollection<Song>(); // якщо немає збереженого плейлисту - створити новий

        }
        // методи команди закриття програми
        private void ShutdownCurrent(object obj) => Application.Current.Shutdown();
        private bool CanShutdownCurrent(object arg) => true;

        public enum FormatFile
        {
            Unknown,
            MP3,
            WAV,
            WMA,
            OGG
        }

        private readonly Dictionary<string, FormatFile> _formatFiles = new Dictionary<string, FormatFile>
        {
            {"mp3", FormatFile.MP3},
            {"wav", FormatFile.WAV},
            {"wma", FormatFile.WMA},
            {"ogg", FormatFile.OGG}
        };
        public FormatFile GetFormatFile(string extension) => 
            _formatFiles.TryGetValue(extension.Trim('.').ToLower(), out FormatFile category) ? category : FormatFile.Unknown;



        // метод вибору файлів при натисканні на кнопку вибору файлу
        private void ChooseSong(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // фільтр та початкова директорія вибору
            openFileDialog.InitialDirectory = "c:\\"; 
            openFileDialog.Filter = "All Supported Audio (*.mp3..) | *.mp3; *.wav; *.wma; *.ogg;  | All files (*.*) | *.*";                  

            openFileDialog.Title = "Choose songs for play"; 
            
            openFileDialog.Multiselect = true;

            if ((bool)openFileDialog.ShowDialog()) // перевірка результату вибору у вікні
            {
                string[] songName = openFileDialog.FileNames;
                for (int i = 0; i < songName.Length; i++)
                {
                    switch (GetFormatFile(Path.GetExtension(songName[i]))) // перевірка формату вибраних файлів
                    {
                        case FormatFile.MP3:
                        case FormatFile.WAV:
                        case FormatFile.WMA:
                        case FormatFile.OGG:
                            Song song = new Song(Path.GetFileNameWithoutExtension(songName[i]), songName[i]);
                            Library.Add(song); // додавання об'єктів Song у колекцію (плейлист)
                            break;
                        case FormatFile.Unknown:
                            MessageBox.Show($"File {songName[i]} is not in the correct format!");
                            break; // при невірному форматі - повідомлення
                    }
                }
            }
            else
                MessageBox.Show("Choose songs for play!");
        }
        
        private bool CanChooseSong(object arg) => true;

        // 3 методи паузи та відновлення та стоп програвання (оновлення прив'язок) для зменшення повтору коду
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

        // метод визначення команди кнопки Play\Pause
        private void PlayPauseSelected(object obj)
        {
            if (_playerState == PlayerState.Playing)
                PausePlayer();
            else if(_playerState == PlayerState.Paused)
                ResumePlayer();
            else
                PlayAudio(obj);
        }

        // основний метод програвання аудіо через об'єкт класу MediaPlayer
        private void PlayAudio(object obj)
        {
            player.Close(); // при відкритому потоці програвання - закрити (зменшення витрат пам'яті)
            if (SelectedSong != null) // перевірка чи вибраний файл у ListBox
            {
                if (!File.Exists(SelectedSong.PathToSong))
                { // перевірка чи існує файл за заданим шляхом
                    MessageBox.Show($"File {SelectedSong.SongName} doesn't exist at this path");
                    Library.Remove(SelectedSong); // видалення файлу зі списку
                }
                else
                {
                    player.Open(new Uri(SelectedSong.PathToSong, UriKind.RelativeOrAbsolute));
                    player.Play(); // відкрити файл за заданим шляхом та відтворити його
                    _playerState = PlayerState.Playing;
                    IconKindButton = PackIconKind.Pause;
                    timer.Start(); // запустити таймер, кожну секунду якого оновлюється позиція відтворення та його дані
                }
            }
            else
                return;
        }

        // метод кожного тіку таймера який оновлює всю інформацію кожну секунду яка  до цього прив'язана 
        private void timer_Tick(object sender, EventArgs e)
        {
            SelectedSongLength = player.GetSongPosition();
            SelectedSongLengthString = player.GetSongPositionString();
            // зміна даних через прив'язки
            if (_playerState == PlayerState.Playing)
            {
                NowPlayingPosition = player.GetNowPosition();
                NowPlayingPositionString = player.GetNowPositionString();
            }

            // відтворення наступного файлу при завершенні першого
            if (NowPlayingPosition >= (SelectedSongLength - 1))
            {
                if (NowPlayingPosition != SelectedSongLength)
                {
                    if (IsRepeat) // якщо увімкнений повтор - заново
                        player.Repeat();
                    else
                        PlayNext(sender);
                }
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

        // метод зміни гучності звуку
        private void ChangeVolumePosition(object obj)
        {
            if (player.HasAudio)
                player.Volume = Volume;
        }
        private bool CanChangeVolumePosition(object arg) => true;

        // методи змінення Slider - перемотки аудіо файлу
        // прив'язка до Slider
        private void ChangeSongPositionMd(object obj) => PausePlayer(); // при натисканні - стоп

        private bool CanChangeSongPositionMd(object arg)
        {
            if (SelectedSong != null)
                return true;
            else
                return false;
        }

        private void ChangeSongPositionMu(object obj) // при відпусканні - зміна і відновлення відтворення
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

        // стоп - прив'язка до кнопки Stop
        private void StopCurrent(object obj) => StopPlayer();

        private bool CanStopCurrent(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }
        // відтворення наступного (прив'язка до кнопки Play Next)
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
        // відтворення попереднього файлу (прив'язка до кнопки Play Previous)
        private void PlayPrevious(object obj)
        {
            if (NowPlayingPosition >= 2)
                player.Repeat(); // якщо файл відтворюється більше 2 секунд - на початок
            else
                SelectedSong = Library.SelectPrevious(SelectedSong);
        }

        private bool CanPlayPrevious(object arg)
        {
            if (SelectedSong != null)
                return true;

            return false;
        }

        // метод команди збереження плейлисту - прив'язка до кнопки Save Library
        private void SaveLibrary(object obj)
        {
            PathesSaver pathesSaver = new PathesSaver();
            pathesSaver.SavePathes(Library, Path.Combine(Environment.CurrentDirectory, "songs.songlibrary")); // запис у файл

            MessageBox.Show("Your songs was saved successfully");
        }

        private bool CanSaveLibrary(object arg) => true;

        // метод команди очищення плейлисту
        private void DeleteAllSongs(object obj)
        {
            SystemSounds.Exclamation.Play();
            var result = MessageBox.Show("If you turn 'Yes', you will delete ALL tracks",
                "Are you really want to delete ALL tracks?", MessageBoxButton.YesNo,
                MessageBoxImage.Exclamation);

            // запит підтвердження
            if (result == MessageBoxResult.Yes)
            {
                player.Stop();
                player.Close(); // видалення та закриття плеєру при підтвердженні
                Library.Clear();
                IconKindButton = PackIconKind.Play;
                MessageBox.Show("Songs was successfully deleted");
            }
            else
                return;
        }

        private bool CanDeleteAllSongs(object arg)
        {
            if (Library.Count != 0 && _playerState != PlayerState.Playing)
                return true;

            return false;
        }

        // реалізація INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
