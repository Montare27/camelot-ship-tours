namespace Identity.Data.Wrapper
{

    public interface IAuthRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> FindAsync(Guid id);
        public Task<T?> FindByNameAsync(string username);

        public Task Add(T item);
        public void Update(T item);
        public void Remove(T item);
    }
}
