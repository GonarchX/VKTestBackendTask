namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IPasswordHasher
{
    string Hash(string password);
    bool IsSamePasswords(string hash, string password);
}