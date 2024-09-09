using System.ComponentModel.DataAnnotations;

namespace CinemaHelper.Server.DTOs
{
    public record class UpdateCinemaDto(
        [Required][StringLength(256)] string Title,
        [Required] int AlbumId,
        string Description
    );
}
