namespace Domain.Models.Location
{
    using Service;

    public class Location 
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
        public ICollection<Trip> Trips { get; set; } = null!;
    }
}
