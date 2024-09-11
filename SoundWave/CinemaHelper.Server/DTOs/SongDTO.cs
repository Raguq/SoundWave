namespace SoundWave.Server.DTOs
{

    public record class SongDTO(
        int Id,
        string Title,
        int Lenght,
        int AlbumId);
}
