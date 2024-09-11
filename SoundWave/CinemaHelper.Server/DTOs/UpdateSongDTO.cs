using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class UpdateSongDTO(
        [Required] int Id,
        [Required][StringLength(256)] string Title,
        int AlbumId
    );
}
