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
        private MainViewModel cinemaListViewModel = new MainViewModel(new SongService(new SongRemoteDataSource()), new AlbumService(new AlbumRemoteDataSource()));


        public MainWindow()
        {
            InitializeComponent();
        }


        private void CinemaListPage_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new CinemaListPage(cinemaListViewModel);
        }
    }
}