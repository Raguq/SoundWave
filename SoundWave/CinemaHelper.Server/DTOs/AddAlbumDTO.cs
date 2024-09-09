using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class AddAlbumDTO(
        [Required][StringLength(50)] string Name

    );
}
