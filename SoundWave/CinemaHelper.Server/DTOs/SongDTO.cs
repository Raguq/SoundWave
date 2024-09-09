namespace SoundWave.Server.DTOs
{

    public record class SongDTO(
        int Id,
        string Title,
        string Description,
        int AlbumId);
}
