// PulsePro.Persistence/Security/BCryptPasswordHasher.cs
using BCrypt.Net;
using PulsePro.Application.Abstraction;

namespace PulsePro.Persistence.Security;
public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)           => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string pass, string hash)  => BCrypt.Net.BCrypt.Verify(pass, hash);
}