namespace SoundWave.Server.DTOs
{

    public record class SongDTO(
        int Id,
        string Title,
        string Lenght,
        int AlbumId);
}
