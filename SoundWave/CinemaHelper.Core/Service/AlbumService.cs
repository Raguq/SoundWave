using SoundWave.App;
using SoundWave.Core.Data;
using SoundWave.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.Core.Service
{
    public class AlbumService
    {
        private AlbumRemoteDataSource _dataSource;
        public AlbumService(AlbumRemoteDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        /// <summary>
        /// Получить все песни
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlbumDTO>> GetAll()
        {
            return await _dataSource.GetAlbumList();
        }
        /// <summary>
        /// Получить песню по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор песни</param>
        /// <returns>null в случае, если песня не найден</returns>
        public async Task<AlbumDTO?> Get(int id)
        {
            foreach (AlbumDTO album in await _dataSource.GetAlbumList())
                if (album.Id == id)
                    return album;
            return null;
        }
        /// <summary>
        /// Добавить новую песню
        /// </summary>
        /// <param name="song"></param>
        public async Task Create(SongDTO album)
        {
            try
            {
                await _dataSource.PostAlbum(new AddAlbumDTO(
                    album.Title
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
                await _dataSource.DeleteAlbum(id);
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
        public async Task Update(UpdateAlbumDTO album)
        {
            try
            {
                await _dataSource.PutAlbum(new UpdateAlbumDTO(
                                album.Id,
                                album.Name
                                ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
