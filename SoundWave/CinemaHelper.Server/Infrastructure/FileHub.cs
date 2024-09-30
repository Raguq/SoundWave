using Microsoft.AspNetCore.SignalR;
using SoundWave.Server.Data; // Пространство имен для вашего DataContext
using SoundWave.Server.DTOs;
using Microsoft.EntityFrameworkCore; // Для работы с EF Core
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundWave.Server.Hubs
{
    public class FileHub : Hub
    {
        private readonly DataContext _context; // Поле для хранения контекста данных

        // Конструктор для внедрения DataContext
        public FileHub(DataContext context)
        {
            _context = context;
        }

        // Метод для отправки информации о песне всем подключенным клиентам
        public async Task SendSong(SongDTO song)
        {
            await Clients.All.SendAsync("ReceiveSong", song);
        }

        // Метод для получения списка песен
        public async Task GetSongs()
        {
            var songs = await GetSongsFromDatabase();
            await Clients.Caller.SendAsync("ReceiveSongs", songs);
        }

        // Метод для получения песен из базы данных
        private async Task<List<SongDTO>> GetSongsFromDatabase()
        {
            var songs = await _context.Songs
                .Include(s => s.Album) // Включение альбома, если нужно
                .Include(s => s.User) // Включение пользователя, если нужно
                .Select(song => new SongDTO(song.Id, song.FilePath, song.Title, song.Lenght, song.AlbumId, song.UserId))
                .ToListAsync();

            return songs; // Возвращаем список песен как DTO
        }
    }
}
