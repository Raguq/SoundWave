using SoundWave.Core.Utility;

namespace SoundWave.Core.Data
{
    /// <summary>
    /// Класс для доступа к данным о песнях
    /// </summary>
    public class RemoteDataSource
    {
        /// <summary>
        /// Относительный путь к файлу, где хранятся данные
        /// </summary>
        private readonly string path = ".\\song_data.json";

        /// <summary>
        /// Метод чтения данных в формате JSON 
        /// и их десериализация
        /// </summary>
        /// <returns></returns>
        public List<Song> Get()
        {
            //if (File.Exists(path))
            //{
            //    using(StreamReader reader = new StreamReader(path))
            //    {
            //        string data = reader.ReadToEnd();
            //        var tmp = DataSerializer.Deserialize<List<Song>>(data) ?? [];
            //        Song._id_counter = tmp.Count > 0 ? tmp.Select(x => x.ItemId).Max() + 1 : 0;
            //        return tmp;
            //    }
                
            //}
            return [];
        }

        /// <summary>
        /// Метод записи данных в формате JSON 
        /// и их десериализация
        /// </summary>
        /// <returns></returns>
        public void Write(List<Song> data)
        {
            using (StreamWriter writer = new StreamWriter(path,false)) {

                writer.WriteLine(DataSerializer.Serialize(data));
            }
        }
    }
}
