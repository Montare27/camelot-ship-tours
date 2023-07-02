namespace Persistence
{
    using Application.Interfaces;
    using Domain.Models.Location;
    using Domain.Models.Service;
    using Domain.Models.Ship;
    using Domain.Models.Ticket;
    using EntityConfigurations;
    using Microsoft.EntityFrameworkCore;

    public class CamelotDbContext : DbContext, ICamelotDbContext
    {
        public CamelotDbContext() {}
        public CamelotDbContext(DbContextOptions<CamelotDbContext> options) : base(options) {}
        
        public DbSet<Ship> Ships { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<TicketClass> TicketClasses { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShipConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TripConfiguration());
            modelBuilder.ApplyConfiguration(new TicketClassConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
