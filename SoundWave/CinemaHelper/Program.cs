
using SoundWave.Core;
using SoundWave.Core.Data;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;



var remote = new SongRemoteDataSource();
List<SongDTO> songs = await remote.GetSongList();

Console.WriteLine("Get All");
Console.WriteLine(string.Join(" ",songs));

if(songs.Count > 0){
    Console.WriteLine("First of them - GET by id");
    Console.WriteLine(await remote.GetSong(songs.First().Id));
}
await remote.PostSong(new AddSongDTO(
    "Integration_test",
    "",
    111,
    1,
    1
    ));

songs = await remote.GetSongList();

Console.WriteLine("POST new song");
Console.WriteLine(string.Join(" ", songs));

await remote.PutSong(new UpdateSongDTO(
    songs.Last().Id,
    "Integration_test2",
    "",
    111,
    1,
    1
    ));

songs = await remote.GetSongList();

Console.WriteLine("PUT new value of song name");
Console.WriteLine(string.Join(" ", songs));

await remote.DeleteSong(songs.Last().Id);

songs = await remote.GetSongList();

Console.WriteLine("DELETEd song");
Console.WriteLine(string.Join(" ", songs));
Console.ReadLine();
