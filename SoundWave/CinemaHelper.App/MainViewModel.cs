using Microsoft.AspNetCore.SignalR.Client;
using SoundWave.App.Core;
using SoundWave.Core;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace SoundWave.App
{
    public class MainViewModel : ObservableObject
    {

        private HubConnection _connection;
        private string _userName;
        private string _message;
        //public string UserName
        //{
        //    get => _userName;
        //    set => SetProperty(ref _userName, value);
        //}

        //public string Message
        //{
        //    get => _message;
        //    set => SetProperty(ref _message, value);
        //}

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
        public ObservableCollection<AlbumDTO> AlbumList { get => _albumList; set { _albumList = value; OnPropertyChanged("AlbumList"); } }
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

        private ObservableCollection<SongEntity> _songList = new ObservableCollection<SongEntity>();
        public ObservableCollection<SongEntity> SongList { get => _songList; set { _songList = value; OnPropertyChanged("SongList"); } }

        private SongService songService;

        private SongEntity _selectedSong;
        public SongEntity SelectedSong
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
            //SendFileCommand = new RelayCommand(async obj => await SendFile(obj));
            Task.Run(() => Fetch());
        }

        public async Task Fetch()
        {
            try
            {
                AlbumList = new ObservableCollection<AlbumDTO>(await albumService.GetAll());
                SongList = new ObservableCollection<SongEntity>((await songService.GetAll()).Select(x=>new SongEntity(x,AlbumList.First(y=>x.AlbumId == y.Id))));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private async Task SendFile(object obj)
        //{
        //    if (obj is string filePath && !string.IsNullOrEmpty(filePath))
        //    {
        //        var fileName = Path.GetFileName(filePath);
        //        var fileBytes = await File.ReadAllBytesAsync(filePath);
        //        await _connection.InvokeAsync("SendFile", UserName, fileBytes, fileName);
        //    }
        //}

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
                                  await songService.Create(
                                          new SongDTO(0, Input, Input2 ?? 0, SelectedAlbum.Id, 1)
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
                                        SelectedSong.Song.Id
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

                return editCommand ??
                  (editCommand = new AsyncRelayCommand(() => Task.Run(
                      async () =>
                      {
                          try
                          {
                              await songService.Update(
                                    new UpdateSongDTO(
                                        SelectedSong.Song.Id,
                                        Input,
                                        Input2 ?? 0,
                                        SelectedSong.Song.AlbumId,
                                        SelectedSong.Song.UserId
                                        )
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


    }
}
