using Microsoft.EntityFrameworkCore.Update.Internal;
using SoundWave.Core.Utility;
using SoundWave.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.Core.Data
{
    public class SongRemoteDataSource
    {
        public static readonly HttpClient client = new HttpClient();

        public SongRemoteDataSource()
        {
            client.BaseAddress = new Uri("http://localhost:5042/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<SongDTO> GetSong(int id)
        {
            SongDTO song = null;

            HttpResponseMessage response = await client.GetAsync($"api/Songs/{id}");
            if (response.IsSuccessStatusCode)
            {
                song = DataSerializer.Deserialize<SongDTO>(
                    await response.Content.ReadAsStringAsync());
            }
            return song;
        }

        public async Task<List<SongDTO>> GetSongList()
        {

            HttpResponseMessage response = await client.GetAsync(
                "api/Songs");
            response.EnsureSuccessStatusCode();

            List<SongDTO> SongResponse = new List<SongDTO>();
            if (response.IsSuccessStatusCode)
            {
                SongResponse = DataSerializer.Deserialize<List<SongDTO>>(await response.Content.ReadAsStringAsync());
            }
            return SongResponse;
        }

        public async Task PostSong(AddSongDTO addSongDTO)
        {
            HttpResponseMessage response = await client.PostAsync(
                ("api/Songs"), JsonContent.Create(addSongDTO));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }
        public async Task PutSong(UpdateSongDTO song)
        {
            var json = JsonContent.Create(song);

            HttpResponseMessage response = await client.PutAsync(
                $"api/Songs", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }

        public async Task DeleteSong(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Songs/{id}");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }

    }
}
