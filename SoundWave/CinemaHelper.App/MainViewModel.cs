using SoundWave.App.Core;
using SoundWave.Core;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;
using System.Collections.ObjectModel;
using System.Windows;

namespace SoundWave.App
{
    public class MainViewModel : ObservableObject
    {

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


        private ObservableCollection<SongDTO> _songList = new ObservableCollection<SongDTO>();
        public ObservableCollection<SongDTO> SongList { get => _songList; set { _songList = value; OnPropertyChanged("SongList"); } }

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

                return editCommand ??
                  (editCommand = new AsyncRelayCommand(() => Task.Run(
                      async () =>
                      {
                          try
                          {
                              await songService.Update(
                                    new UpdateSongDTO(
                                        SelectedSong.Id,
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
                      }))
                  );

            }
        }


    }
}
