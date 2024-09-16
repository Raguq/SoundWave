using SoundWave.Core.Data;
using SoundWave.Server.DTOs;

namespace SoundWave.Core.Service
{
    /// <summary>
    /// Класс, осуществляющий работу с фильмами 
    /// </summary>
    public class SongService
    {
        private SongRemoteDataSource _dataSource;
        private List<Song> _songs = new List<Song>();
        public SongService(SongRemoteDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private async Task Init()
        {
            _songs = (await _dataSource.GetSongList()).Select(x => new Song(x.Id, x.Title)).ToList();
        }

        /// <summary>
        /// Получить все фильмы
        /// </summary>
        /// <returns></returns>
        public List<Song> GetAll()
        {
            return _songs;
        }
        /// <summary>
        /// Получить фильм по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор фильма</param>
        /// <returns>null в случае, если фильм не найден</returns>
        public Song Get(int id)
        {
            foreach (Song cinema in _songs)
                if (cinema.ItemId == id)
                    return cinema;
            return null;
        }
        /// <summary>
        /// Добавить новый фильм
        /// </summary>
        /// <param name="cinema"></param>
        public async Task Create(Song cinema)
        {
            _songs.Add(cinema);
            await _dataSource.PostSong(new AddSongDTO(
                cinema.Title,
                111,
                1,
                1
                ));
        }
        /// <summary>
        /// Удалить фильм по идентификатору
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            await _dataSource.DeleteSong(id);
        }
        /// <summary>
        /// Обновить фильм
        /// </summary>
        /// <param name="cinema"></param>
        public async Task Update(Song cinema)
        {
            await _dataSource.PutSong(new UpdateSongDTO(
                            cinema.ItemId,
                            cinema.Title,
                            111,
                            1,
                            1
                            ));
        }

    }
}
