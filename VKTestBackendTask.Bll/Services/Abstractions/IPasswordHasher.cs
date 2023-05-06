namespace VKTestBackendTask.Bll.Services.Abstractions;

public interface IPasswordHasher
{
    /// <summary>
    /// Get hash for specified value
    /// </summary>
    string Hash(string password);

    /// <summary>
    /// Checks specified value on equality
    /// </summary>
    /// <returns>
    /// True - <see cref="hash"/> is equivalent to <see cref="password"/> <br/>
    /// False - otherwise
    /// </returns>
    bool IsSamePasswords(string hash, string password);
}