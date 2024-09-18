using SoundWave.Server.DTOs;
using SoundWave.Server.Entities;
using SoundWave.Server.Utilities;

namespace SoundWave.Server.Mapper
{
    public static class SecurityMapper
    {
        public static User ToEntity(this SecurityRequest request)
        {
            return new User()
            {
                Login = request.Login,
                Password = Encoder.ComputeSHA256Hash(request.Password),
            };
        }

        public static SecurityResponse ToResponse(this User user)
        {
            return new SecurityResponse(
                user.UserId
            );
        }
    }
}
