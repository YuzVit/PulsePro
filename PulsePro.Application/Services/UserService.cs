using Microsoft.EntityFrameworkCore;
using PulsePro.Application.Abstraction;
using PulsePro.Application.DTO;
using PulsePro.Application.Mappers;
using PulsePro.Domain.Entities;

namespace PulsePro.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _db;
        private readonly ApplicationMapper _mapper;

        public UserService(IApplicationDbContext db, ApplicationMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDto> RegisterAsync(RegisterUserDto dto)
        {
            var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existing != null) throw new Exception("Email already in use.");

            var userEntity = _mapper.RegisterUserDtoToUser(dto);
            userEntity.Id = Guid.NewGuid();
            userEntity.PasswordHash = "HASH(" + dto.Password + ")"; // У реалі – безпечне хешування

            _db.Users.Add(userEntity);
            await _db.SaveChangesAsync();

            return _mapper.UserToDto(userEntity);
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new Exception("User not found.");
            if (user.PasswordHash != "HASH(" + dto.Password + ")") throw new Exception("Invalid password.");

            return "FAKE_JWT_TOKEN";
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _db.Users.FindAsync(userId);
            return user == null ? null : _mapper.UserToDto(user);
        }

        public async Task UpdateUserAsync(UserDto dto)
        {
            var user = await _db.Users.FindAsync(dto.Id);
            if (user == null) throw new Exception("User not found.");

            user.UserName = dto.UserName;
            user.Email = dto.Email;
            user.Weight = dto.Weight;
            user.Height = dto.Height;
            user.BirthDate = dto.BirthDate;

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
