// PulsePro.Persistence/Security/SystemClock.cs
using PulsePro.Application.Abstraction;

namespace PulsePro.Persistence.Security;
public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}