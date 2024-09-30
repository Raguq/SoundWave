using SoundWave.Server.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class AddSongDTO
    (
        [Required][StringLength(128)] string Title,
        [Required][StringLength(256)] string FilePath,
        int Length,
        int AlbumId,
        int UserId
    );
}
