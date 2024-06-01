using DemoPresentation.DTO;

namespace DemoPresentation.Service
{
    public interface IUserService
    {
        UserDto? GetUser(string? username);
        bool IsAuthenticated(string? password, string? passwordHash);
    }

}
