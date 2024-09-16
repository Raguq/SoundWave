using SoundWave.App.Core;
using SoundWave.Core;
using SoundWave.Core.Service;
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

        private ObservableCollection<Song> _cinemaList = new ObservableCollection<Song>();
        public ObservableCollection<Song> CinemaList { get => _cinemaList; set { _cinemaList = value; OnPropertyChanged("CinemaList"); } }

        private SongService cinemaService;

        private Song _selectedCinema;
        public Song SelectedCinema
        {
            get => _selectedCinema;
            set
            {
                _selectedCinema = value;
                OnPropertyChanged("SelectedCinema");
            }
        }

        public MainViewModel(SongService service)
        {
            cinemaService = service;
            CinemaList = new ObservableCollection<Song>(cinemaService.GetAll());
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
                              await cinemaService.Create(
                                  new Song(0, Input)
                                  );
                              CinemaList = new ObservableCollection<Song>(cinemaService.GetAll());
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
                            await cinemaService.Delete(
                                SelectedCinema.ItemId
                                  );
                            CinemaList = new ObservableCollection<Song>(cinemaService.GetAll());
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
                        SelectedCinema.Title = Input;
                        await cinemaService.Update(
                          SelectedCinema
                          );
                      CinemaList = new ObservableCollection<Song>(cinemaService.GetAll());
                  }))
                  );
            }
        }


    }
}
