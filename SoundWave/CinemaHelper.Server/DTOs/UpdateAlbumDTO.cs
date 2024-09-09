using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class UpdateAlbumDTO(
        [Required][StringLength(128)] string Name
    );
}
