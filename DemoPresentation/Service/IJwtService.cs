using DemoPresentation.DTO;

namespace DemoPresentation.Service
{
    public interface IJwtService
    {
        string GenerateToken(UserDto? user);
    }
}
