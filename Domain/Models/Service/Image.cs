namespace Domain.Models.Service
{
    public class Image
    {
        public int Id { get; set; }
        public string Encoded { get; set; } = string.Empty;
        public Trip Trip { get; set; } = null!;
        public int? TripId { get; set; }
    }
}
