using SoundWave.Server.DTOs;
using SoundWave.Server.Entities;

namespace SoundWave.Server.Mapper
{
    public static class SongMapper
    {
        public static SoundWave.Server.Entities.Song ToEntity(this AddSongDTO song)
        {
            return new Entities.Song()
            {
                Title = song.Title,
                Lenght = song.Length,
                AlbumId = song.AlbumId,
                UserId = song.UserId
            };
        }

        public static Entities.Song ToEntity(this UpdateSongDTO song)
        {
            return new Entities.Song()
            {
                Id = song.Id,
                Title = song.Title,
                Lenght = song.Length,
                AlbumId = song.AlbumId,
                UserId = song.UserId
            };
        }

        public static SongDTO ToSongDTO(this Entities.Song song)
        {
            return new SongDTO(
                song.Id,
                song.FilePath,
                song.Title,
                song.Lenght,
                song.AlbumId,
                song.UserId
            );
        }
    }
}
