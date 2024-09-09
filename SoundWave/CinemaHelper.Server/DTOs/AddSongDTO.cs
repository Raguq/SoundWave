using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public class AddSongDTO
    (
        [Required][StringLength(50)] string Name,
        int GenreId,
        [Range(1, 100)] decimal Price,
        DateOnly ReleaseDate
    );
}
