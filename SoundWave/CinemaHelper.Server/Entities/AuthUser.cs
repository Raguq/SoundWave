using Microsoft.AspNetCore.Identity;

namespace SoundWave.Server.Entities
{
    public class AuthUser : IdentityUser
    {
        public string? Initials { get; set; }
    }
}
