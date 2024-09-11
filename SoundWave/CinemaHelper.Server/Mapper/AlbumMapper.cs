using SoundWave.Server.DTOs;

namespace SoundWave.Server.Mapper
{
    public static class AlbumMapper
    {
        public static SoundWave.Server.Entities.Album ToEntity(this AddAlbumDTO album)
        {
            return new Entities.Album()
            {
                Name = album.Name
            };
        }

        public static Entities.Album ToEntity(this UpdateAlbumDTO album)
        {
            return new Entities.Album()
            {
                Id = album.Id,
                Name = album.Name            
            };
        }

        public static AlbumDTO ToAlbumDTO(this Entities.Album album)
        {
            return new AlbumDTO(
                album.Id,
                album.Name
            );
        }
    }
}
