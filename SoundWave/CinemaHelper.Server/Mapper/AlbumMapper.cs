using SoundWave.Server.DTOs;

namespace SoundWave.Server.Mapper
{
    public static class AlbumMapper
    {
        public static SoundWave.Server.Entities.Album ToEntity(this AddAlbumDTO album)
        {
            return new Entities.Album()
            {
                Name = album.Name,
            };
        }

        public static Entities.Album ToEntity(this UpdateAlbumDTO game, int id)
        {
            return new Entities.Album()
            {
                Name = game.Name,
            };
        }

        public static AlbumDTO ToAuthorDTO(this Entities.Album game)
        {
            return new AlbumDTO(
                game.Id,
                game.Name
            );
        }
    }
}
