using PulsePro.Application.DTO;

namespace PulsePro.Application.Abstraction
{
    public interface IUserService
    {
        Task<UserDto> RegisterAsync(RegisterUserDto dto);
        Task<string> LoginAsync(LoginUserDto dto);
        Task<UserDto?> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(UserDto dto);
    }
}