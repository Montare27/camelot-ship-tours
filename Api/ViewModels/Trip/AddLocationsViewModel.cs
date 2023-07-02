namespace Api.ViewModels.Trip
{
    using AutoMapper;
    using Domain.Models.Location;
    using Interfaces.Mapper;

    public class AddLocationsViewModel
    {
        public int? TripId { get; set; }
        public List<LocationViewModel> Locations { get; set; } = new List<LocationViewModel>();
    }

    public class LocationViewModel : IMapWith<Location, LocationViewModel>
    {
        public string CountryName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;

        public void Mapping(Profile profile) =>
            profile.CreateMap<Location, LocationViewModel>();
    }
}
