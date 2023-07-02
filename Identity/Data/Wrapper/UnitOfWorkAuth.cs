namespace Identity.Data.Wrapper
{
    public class UnitOfWorkAuth : IDisposable
    {
        private readonly AuthDbContext _db;
        private UserRepository? _userRepository;
        private bool _dispose = false;

        public UnitOfWorkAuth(AuthDbContext db)
        {
            _db = db;
        }

        public virtual UserRepository? UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_db);
                return _userRepository;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(cancellationToken);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                    _db.Dispose();
                _dispose = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
