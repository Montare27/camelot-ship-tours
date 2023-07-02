namespace Application.Services.SessionService
{
    using global::Interfaces;
    using Microsoft.AspNetCore.Http;
    using System.Text.Json;

    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _context;
        
        public SessionService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void SetItem<T> (string key, T value)
        {
            var json = JsonSerializer.Serialize<T>(value);
            (_context.HttpContext?.Session as ISessionWrapper)?.SetString(key, json);
        }

        public T? GetItem<T>(string key)
        {
            var value = _context.HttpContext?.Session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
