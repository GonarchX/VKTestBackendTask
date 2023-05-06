namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow();
}