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
            //SongList = new ObservableCollection<SongDTO>(await songService.GetAll());
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
                                      new SongDTO(0, Input)
                                      );
                                  SongList = new ObservableCollection<SongDTO>(await songService.GetAll());
                              }
                              catch (Exception ex) 
                              {
                                  MessageBox.Show(
                                      ex.Message,
                                      "ОШИБКА",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error
                                      );
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
                            await songService.Delete(
                                SelectedSong.ItemId
                                  );
                            SongList = new ObservableCollection<SongDTO>(songService.GetAll());
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
                        SelectedSong.Title = Input;
                        await songService.Update(
                          SelectedSong
                          );
                      SongList = new ObservableCollection<SongDTO>(songService.GetAll());
                  }))
                  );
            }
        }


    }
}
