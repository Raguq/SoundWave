namespace SoundWave.Server.DTOs
{

    public record class SongDTO(
        int Id,
        string FilePath,
        string Title,
        int Lenght,
        int AlbumId,
        int UserId);
}
