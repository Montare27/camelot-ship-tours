namespace Domain.Models.Ship
{
    using Service;

    public class Ship 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; } = null!;
        public ICollection<Trip>? Trips { get; set; } = null;
    }
}
