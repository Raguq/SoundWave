using SoundWave.Core.Data;
using SoundWave.Core.Service;
using System.Windows;

namespace SoundWave.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel cinemaListViewModel = new MainViewModel(new SongService(new SongRemoteDataSource()));
        private BookingViewModel bokingViewModel = new BookingViewModel(new TicketService(new AlbumRemoteDataSource()));


        public MainWindow()
        {
            InitializeComponent();
        }

        private void BokingPage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new BookingPage(bokingViewModel);
        }

        private void CinemaListPage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CinemaListPage(cinemaListViewModel);
        }
    }
}