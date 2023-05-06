using System.Text;

namespace VKTestBackendTask.Bll.Common;

public static class BasicAuthHelper
{
    public const string CredentialsDelimiter = ":";

    public static string EncodeCredentials(string login, string password)
    {
        var valueBytes = Encoding.UTF8.GetBytes(login + CredentialsDelimiter + password);
        return Convert.ToBase64String(valueBytes);
    }

    public static string DecodeCredentials(string encodedCredentials)
    {
        var valueBytes = Encoding.UTF8.GetBytes(encodedCredentials);
        return Convert.ToBase64String(valueBytes);
    }
}