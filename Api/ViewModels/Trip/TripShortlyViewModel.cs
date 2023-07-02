namespace Api.ViewModels.Trip
{
    using AutoMapper;
    using Domain.Models.Location;
    using Domain.Models.Service;
    using Domain.Models.Ship;
    using Domain.Models.Ticket;
    using Interfaces.Mapper;

    public class GetTripShortlyViewModel : IMapWith<Trip, GetTripShortlyViewModel>
    {
        public int Id { get; set; }
        public Ship Ship { get; set; } = new Ship();
        public int? ShipId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<Location> Locations { get; set; } = null!;
        public ICollection<Image>? Images { get; set; } = null;
        public ICollection<TicketClass> TicketClasses { get; set; } = null!;

        public void Mapping(Profile profile) =>
            profile.CreateMap<Trip, GetTripShortlyViewModel>()
                .MaxDepth(1);
    }
    
    public class CreateTripViewModel : IMapWith<CreateTripViewModel, Trip>
    {
        public int Id { get; set; }
        public int? ShipId { get; set; }
        public string Description { get; set; } = string.Empty;

        public void Mapping(Profile profile) =>
            profile.CreateMap<CreateTripViewModel, Trip>()
                .MaxDepth(1);
    }
}
