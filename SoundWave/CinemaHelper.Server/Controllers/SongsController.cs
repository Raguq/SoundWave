using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundWave.Server.Data;
using SoundWave.Server.DTOs;
using SoundWave.Server.Entities;
using SoundWave.Server.Mapper;

namespace SoundWave.Server.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly string _filePath; // Путь, где будут храниться файлы

        public SongsController(DataContext context)
        {
            _context = context;
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles"); // Путь для хранения файлов
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath); // Создаем каталог, если его нет
            }
        }

        // GET: api/songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongDTO>>> GetSongs()
        {
            var songs = await _context.Songs.ToListAsync();
            var songDtos = songs.Select(song => song.ToSongDTO());

            return Ok(songDtos);
        }

        // GET: api/songs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDTO>> GetSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song.ToSongDTO());
        }

        // PUT: api/songs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, [FromBody] UpdateSongDTO updateSongDto)
        {
            if (id != updateSongDto.Id)
            {
                return BadRequest("Song ID mismatch.");
            }

            var existingSong = await _context.Songs.FindAsync(id);
            if (existingSong == null)
            {
                return NotFound();
            }

            _context.Entry(existingSong).CurrentValues.SetValues(updateSongDto.ToEntity());
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/songs
        [HttpPost]
        public async Task<ActionResult<SongDTO>> CreateSong([FromBody] AddSongDTO addSongDto)
        {
            var songEntity = addSongDto.ToEntity();
            _context.Songs.Add(songEntity);
            await _context.SaveChangesAsync();

            var songDto = songEntity.ToSongDTO();
            return CreatedAtAction(nameof(GetSong), new { id = songDto.Id }, songDto);
        }

        // POST: api/songs/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadSong(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            var fileName = Path.GetFileName(file.FileName);
            var fullPath = Path.Combine(_filePath, fileName);

            // Сохранение файла на сервере
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Обновление пути файла в сущности песни
            song.FilePath = fullPath; // Предполагаем, что у вас есть свойство FilePath в модели Song
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();

            return Ok(new { SongId = song.Id, FilePath = song.FilePath });
        }

        // DELETE: api/songs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id) => _context.Songs.Any(e => e.Id == id);
    }
}
