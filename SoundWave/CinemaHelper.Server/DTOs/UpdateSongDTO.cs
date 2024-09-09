using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class UpdateSongDTO(
        [Required][StringLength(256)] string Title,
        [Required] int AlbumId
    );
}
