using SoundWave.Core.Utility;
using SoundWave.Server.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SoundWave.App
{
    public class AlbumRemoteDataSource
    {
        public static readonly HttpClient client = new HttpClient();

        public AlbumRemoteDataSource()
        {
            client.BaseAddress = new Uri("http://localhost:5042/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<AlbumDTO> GetAlbum(int id)
        {
            AlbumDTO album = null;

            HttpResponseMessage response = await client.GetAsync($"api/Albums/{id}");
            if (response.IsSuccessStatusCode)
            {
                album = DataSerializer.Deserialize<AlbumDTO>(
                    await response.Content.ReadAsStringAsync());
            }
            return album;
        }

        public async Task<List<AlbumDTO>> GetAlbumList()
        {

            HttpResponseMessage response = await client.GetAsync(
                "api/Albums");
            response.EnsureSuccessStatusCode();

            List<AlbumDTO> AlbumResponse = new List<AlbumDTO>();
            if (response.IsSuccessStatusCode)
            {
                AlbumResponse = DataSerializer.Deserialize<List<AlbumDTO>>(await response.Content.ReadAsStringAsync());
            }
            return AlbumResponse;
        }

        public async Task PostAlbum(AddAlbumDTO addAlbumDTO)
        {
            HttpResponseMessage response = await client.PostAsync(
                ("api/Albums"), JsonContent.Create(addAlbumDTO));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }
        public async Task PutAlbum(UpdateAlbumDTO album)
        {
            var json = JsonContent.Create(album);

            HttpResponseMessage response = await client.PutAsync(
                $"api/Albums", json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }

        public async Task DeleteAlbum(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Albums/{id}");
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            return;
        }
    }
}