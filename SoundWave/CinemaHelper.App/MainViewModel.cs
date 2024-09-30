using Microsoft.AspNetCore.SignalR.Client;
using SoundWave.App.Core;
using SoundWave.Core;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Threading.Tasks;

namespace SoundWave.App
{
    public class MainViewModel : ObservableObject
    {
        private HubConnection _connection;
        private string _userName;
        private string _message;
        public string UserName
        {
            get => _userName;
            set => OnPropertyChanged(_userName ?? "");
        }

        public string Message
        {
            get => _message;
            set => OnPropertyChanged(_message ?? "");
        }

        public RelayCommand SendFileCommand { get; }

        private string _input = string.Empty;
        private int? _input2 = null;
        private List<AlbumDTO> _album;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged("Input");
            }
        }

        public int? Input2
        {
            get => _input2;
            set
            {
                _input2 = value;
                OnPropertyChanged("Input2");
            }
        }

        private ObservableCollection<AlbumDTO> _albumList = new ObservableCollection<AlbumDTO>();
        public ObservableCollection<AlbumDTO> AlbumList
        {
            get => _albumList;
            set { _albumList = value; OnPropertyChanged("AlbumList"); }
        }

        private AlbumService albumService;
        private AlbumDTO _selectedAlbum;
        public AlbumDTO SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                OnPropertyChanged("SelectedAlbum");
            }
        }

        private ObservableCollection<SongDTO> _songList = new ObservableCollection<SongDTO>();
        public ObservableCollection<SongDTO> SongList
        {
            get => _songList;
            set { _songList = value; OnPropertyChanged("SongList"); }
        }

        private SongService songService;
        private SongDTO _selectedSong;
        public SongDTO SelectedSong
        {
            get => _selectedSong;
            set
            {
                _selectedSong = value;
                OnPropertyChanged("SelectedSong");
            }
        }

        public MainViewModel(SongService service, AlbumService albumService)
        {
            songService = service;
            this.albumService = albumService;

            SendFileCommand = new RelayCommand(async obj => await SendFile());
            Task.Run(() => Fetch());
        }

        public async Task Fetch()
        {
            try
            {
                SongList = new ObservableCollection<SongDTO>(await songService.GetAll());
                AlbumList = new ObservableCollection<AlbumDTO>(await albumService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task SendFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    using var memoryStream = new MemoryStream();
                    await fileStream.CopyToAsync(memoryStream);

                    // Передаем файл на сервер
                    await _connection.InvokeAsync("SendFile", memoryStream.ToArray(), Path.GetFileName(filePath));
                    MessageBox.Show("File sent successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending file: {ex.Message}");
                }
            }
        }

        private AsyncRelayCommand addCommand;
        public AsyncRelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (
                    addCommand = new AsyncRelayCommand(() => Task.Run(
                        async () =>
                        {
                            try
                            {
                                // Определяем путь для файла
                                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedSongs", $"{Input}.mp3");

                                await songService.Create(
                                    new SongDTO(0, filePath, Input, Input2 ?? 0, SelectedAlbum.Id, 1)
                                );
                                await Fetch();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }))
                    );
            }
        }


        private AsyncRelayCommand deleteCommand;
        public AsyncRelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (
                    deleteCommand = new AsyncRelayCommand(() => Task.Run(
                        async () =>
                        {
                            try
                            {
                                await songService.Delete(
                                        SelectedSong.Id
                                          );
                                await Fetch();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }))
                    );
            }
        }

        private AsyncRelayCommand editCommand;
        public AsyncRelayCommand EditCommand
        {
            get
            {
                return editCommand ??= new AsyncRelayCommand(async () =>
                {
                    try
                    {
                        // Определяем путь для файла, если требуется
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedSongs", $"{Input}.mp3");

                        await songService.Update(
                            new UpdateSongDTO(
                                SelectedSong.Id,
                                filePath, // Устанавливаем обновленный путь, если это необходимо
                                Input,
                                Input2 ?? 0,
                                SelectedSong.AlbumId,
                                SelectedSong.UserId
                            )
                        );
                        await Fetch();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
            }
        }
    }
}