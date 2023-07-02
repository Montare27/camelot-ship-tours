namespace Domain.Models.Ticket
{
    using Service;

    public class TicketClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public uint Capacity { get; set; }
        public uint AvailableTickets { get; set; }
        public Trip Trip { get; set; } = null!;
        public int? TripId { get; set; }
        public ICollection<Ticket>? Tickets { get; set; } = null;
    }
}
