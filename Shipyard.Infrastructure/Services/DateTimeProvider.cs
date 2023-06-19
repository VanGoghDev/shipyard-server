using Shipyard.Application.Common.Services;

namespace Shipyard.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}