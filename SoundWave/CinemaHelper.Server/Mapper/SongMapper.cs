using SoundWave.Server.DTOs;

namespace SoundWave.Server.Mapper
{
    public static class SongMapper
    {
        public static SoundWave.Server.Entities.Song ToEntity(this AddSongDTO game)
        {
            return new Entities.Song()
            {
                AlbumId = game.AlbumId,
                Title = game.Title,
                Description = game.Description,
            };
        }

        public static Entities.Song ToEntity(this UpdateSongDTO game, int id)
        {
            return new Entities.Song()
            {
                AlbumId = game.AlbumId,
                Title = game.Title,
                Description = game.Description,
            };
        }

        public static SongDTO ToCinemaDTO(this Entities.Song game)
        {
            return new SongDTO(
                game.Id,
                game.Title,
                game.Description,
                game.AlbumId
            );
        }
    }
}
