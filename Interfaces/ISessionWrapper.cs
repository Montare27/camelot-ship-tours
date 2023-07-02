namespace Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface ISessionWrapper : ISession
    {
        string? GetString(string key);
        void SetString(string key, string? value);
    }
}
