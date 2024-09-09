using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class AddSongDTO
    (
        [Required][StringLength(128)] string Title,
        [Required][StringLength(128)] string Length
    );
}
