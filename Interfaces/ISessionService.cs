namespace Interfaces
{
    public interface ISessionService
    {
        public void SetItem<T>(string key, T value);

        public T? GetItem<T>( string key);
    }
}
