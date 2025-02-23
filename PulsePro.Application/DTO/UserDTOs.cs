namespace PulsePro.Application.DTO
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
    }
}