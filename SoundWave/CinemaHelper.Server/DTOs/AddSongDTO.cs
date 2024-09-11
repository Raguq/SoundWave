using SoundWave.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class AddSongDTO
    (
        [Required][StringLength(128)] string Title,
        [StringLength(128)] int Length,
        [StringLength(128)] int AlbumId
    );
}
