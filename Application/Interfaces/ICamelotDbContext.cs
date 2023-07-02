namespace Application.Interfaces
{
    using Domain.Models.Location;
    using Domain.Models.Service;
    using Domain.Models.Ship;
    using Domain.Models.Ticket;
    using Microsoft.EntityFrameworkCore;

    public interface ICamelotDbContext
    {
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trip> Trips { get; set; } 
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; } 
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<Ticket> Tickets { get; set; } 
        public DbSet<TicketClass> TicketClasses { get; set; } 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
