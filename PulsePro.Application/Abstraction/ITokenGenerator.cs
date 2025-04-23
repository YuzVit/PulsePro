namespace PulsePro.Application.Abstraction;

public interface ITokenGenerator
{
    string GenerateAccessToken(Guid userId, string email);
}