// Сценарий: Добавление нового фильма, изменение его параметров, удаление фильма
using SoundWave.Core;
using SoundWave.Core.Data;
using SoundWave.Core.Service;
using SoundWave.Server.DTOs;

//CinemaService dataService = new CinemaService(new CinemaDataSource());

//Console.WriteLine("Start");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));
//dataService.Create(new Song("Интерстеллар"));
//Console.WriteLine("Added Интерстеллар");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));

//Song tmp = new Song("Вверх");
//dataService.Create(tmp);
//Console.WriteLine("Added Вверх");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));

//Song tmp2 = new Song("Пираты");
//dataService.Update(tmp2);
//Console.WriteLine("Changed Интерстеллар to Пираты");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));

//dataService.Delete(tmp.ItemId);
//Console.WriteLine("Deleted Вверх");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));

//dataService.Delete(tmp2.ItemId);
//Console.WriteLine("Deleted Интерстеллар");
//Console.WriteLine(string.Join("\n", dataService.GetAll()));



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
