using Newtonsoft.Json;
using SoundWave.Server.DTOs;

namespace SoundWave.Core
{
    /// <summary>
    /// Класс фильма. 
    /// >>Здесь представлен так называемый Primary конструктор(Доступен с .NET8) 
    /// с необязательным параметром title<<
    /// </summary>
    /// <param name="id">Уникальный идентификатор фильма</param>
    /// <param name="title">Название фильма</param>
    public class SongEntity
    {
        /// <summary>
        /// Обычный конструктор, до .NET8. В новых версиях доступны оба варианта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        public SongEntity(SongDTO song,AlbumDTO album)
        {
            Song = song;
            Album = album;
        }

        [JsonProperty("ItemId")]
        public SongDTO Song { get; set; }

        public AlbumDTO Album { get; set; }

    }
}
