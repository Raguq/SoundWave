using SoundWave.Core.Data;
using SoundWave.Server.DTOs;

namespace SoundWave.Core.Service
{
    /// <summary>
    /// Класс, осуществляющий работу с песнями 
    /// </summary>
    public class SongService
    {
        private SongRemoteDataSource _dataSource;
        public SongService(SongRemoteDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        /// <summary>
        /// Получить все песни
        /// </summary>
        /// <returns></returns>
        public async Task<List<SongDTO>> GetAll()
        {
            return await _dataSource.GetSongList();
        }
        /// <summary>
        /// Получить песню по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор песни</param>
        /// <returns>null в случае, если песня не найден</returns>
        public async Task<SongDTO?> Get(int id)
        {
            foreach (SongDTO song in await _dataSource.GetSongList())
                if (song.Id == id)
                    return song;
            return null;
        }
        /// <summary>
        /// Добавить новую песню
        /// </summary>
        /// <param name="song"></param>
        public async Task Create(SongDTO song)
        {
            try
            {
                await _dataSource.PostSong(new AddSongDTO(
                    song.Title,
                    song.Lenght,
                    song.AlbumId,
                    song.UserId
                    ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Удалить фильм по идентификатору
        /// </summary>
        /// <param name="id"></param>
        public async Task Delete(int id)
        {
            try
            {
                await _dataSource.DeleteSong(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Обновить песню
        /// </summary>
        /// <param name="song"></param>
        public async Task Update(UpdateSongDTO song)
        {
            try
            {
                await _dataSource.PutSong(new UpdateSongDTO(
                                song.Id,
                                song.Title,
                                song.Length,
                                song.AlbumId,
                                song.UserId
                                ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
