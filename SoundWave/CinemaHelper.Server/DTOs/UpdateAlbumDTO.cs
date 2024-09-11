using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class UpdateAlbumDTO(
        [Required] int Id,
        [Required][StringLength(128)] string Name
    );
}
