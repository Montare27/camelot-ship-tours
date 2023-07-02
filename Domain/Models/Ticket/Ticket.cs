namespace Domain.Models.Ticket
{
    public class Ticket
    {
        public Ticket() => 
            PurchaseDate = DateTime.Now;
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
        public int? TicketClassId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public TicketClass TicketClass { get; set; } = null!;
    }
}
