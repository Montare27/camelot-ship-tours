namespace Domain.Models.Service
{
    using Location;
    using Interfaces;
    using Ship;
    using Ticket;

    public class Trip : ITrip
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public Ship Ship { get; set; } = new Ship();
        public int? ShipId { get; set; }
        public ICollection<Image>? Images { get; set; } = null;
        public ICollection<Review>? Reviews { get; set; } = null;
        public ICollection<Location> Locations { get; set; } = null!;
        public ICollection<TicketClass> TicketClasses { get; set; } = null!;
    }
}
