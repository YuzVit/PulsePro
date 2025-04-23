using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _db;
    private readonly IPasswordHasher       _hasher;
    private readonly ITokenGenerator       _tokens;
    private readonly ApplicationMapper     _map;

    public UserService(IApplicationDbContext db, IPasswordHasher hasher,
                       ITokenGenerator tokens, ApplicationMapper map)
    {
        _db     = db;
        _hasher = hasher;
        _tokens = tokens;
        _map    = map;
    }

    public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
    {
        if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email already in use.");

        var entity = _map.RegisterUserDtoToUser(dto);
        entity.Id           = Guid.NewGuid();
        entity.PasswordHash = _hasher.Hash(dto.Password);

        _db.Users.Add(entity);
        await _db.SaveChangesAsync();

        return _map.UserToDto(entity);
    }

    public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email)
            ?? throw new InvalidOperationException("User not found.");

        if (!_hasher.Verify(dto.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid credentials.");

        return _tokens.GenerateAccessToken(user.Id, user.Email);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        return user is null ? null : _map.UserToDto(user);
    }

    public async Task UpdateUserAsync(UserDto dto)
    {
        var user = await _db.Users.FindAsync(dto.Id)
            ?? throw new InvalidOperationException("User not found.");

        user.UserName   = dto.UserName;
        user.Email      = dto.Email;
        user.Weight     = dto.Weight;
        user.Height     = dto.Height;
        user.BirthDate  = dto.BirthDate;

        await _db.SaveChangesAsync();
    }
}
