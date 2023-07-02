namespace Identity.Data.Wrapper
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class UserRepository : IAuthRepository<User>
    {   
        
        private readonly AuthDbContext _db;

        public UserRepository(AuthDbContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult(_db.Users.AsEnumerable());
        }

        public async Task<User?> FindAsync(Guid id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<User?> FindByNameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));
        }

        public async Task Add(User item)
        {
            await _db.AddAsync(item);
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Remove(User item)
        {
            if (_db.Users.Contains(item))
            {
                _db.Users.Remove(item);
            }
        }
    }
}
