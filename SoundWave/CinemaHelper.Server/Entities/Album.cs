using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SoundWave.Server.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

    }
}
