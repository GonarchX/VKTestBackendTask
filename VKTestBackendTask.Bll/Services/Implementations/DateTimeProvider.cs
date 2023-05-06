using VKTestBackendTask.Bll.Services.Abstractions;

namespace VKTestBackendTask.Bll.Services.Implementations;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow() => DateTime.UtcNow;
}