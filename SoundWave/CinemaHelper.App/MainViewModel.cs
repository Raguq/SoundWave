using SoundWave.App.Core;
using SoundWave.Core;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;
using System.Collections.ObjectModel;

namespace SoundWave.App
{
    public class MainViewModel : ObservableObject
    {

        private string _input = string.Empty;
        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged("Input");
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

        public MainViewModel(SongService service)
        {
            songService = service;
            Task.Run(() => Fetch());
        }

        public async Task Fetch()
        {
            SongList = new ObservableCollection<SongDTO>(await songService.GetAll());
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
                              await songService.Create(
                                  new SongDTO(0, Input, 0, 0, 0)
                                  );
                              await Fetch();
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
                            await songService.Delete(
                                SelectedSong.Id
                                  );
                            await Fetch();
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
                          await songService.Update(
                            new UpdateSongDTO(
                                SelectedSong.Id,
                                Input,
                                0,
                                0,
                                0
                                )
                            );
                          await Fetch();
                      }))
                  );
            }
        }


    }
}
