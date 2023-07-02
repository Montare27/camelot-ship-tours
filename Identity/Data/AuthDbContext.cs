namespace Identity.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}
        public DbSet<User> Users { get; set; } = null!;
    }
}
