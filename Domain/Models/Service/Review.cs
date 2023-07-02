namespace Domain.Models.Service
{
    public class Review
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime AdditionTime { get; set; }
        public string Text { get; set; } = string.Empty;
        public Trip Trip { get; set; } = null!;
        public int? TripId { get; set; }
        public RateEnum Rate { get; set; }
        public enum RateEnum
        {
            Terrible = 1,
            Poor,
            Average,
            Good,
            Excellent,
        }
    }
}
