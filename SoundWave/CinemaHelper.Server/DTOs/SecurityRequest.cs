using System.ComponentModel.DataAnnotations;

namespace SoundWave.Server.DTOs
{
    public record class SecurityRequest
         ([Required] string Login,
         [Required] string Password);

}
