namespace Api.ViewModels.Ship
{
    using AutoMapper;
    using Domain.Models.Service;
    using Domain.Models.Ship;
    using Interfaces.Mapper;

    public class CreateShipViewModel : IMapWith<CreateShipViewModel, Ship>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<CreateShipViewModel, Ship>();
    }

    public class EditShipViewModel : IMapWith<EditShipViewModel, Ship>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<EditShipViewModel, Ship>();
    }

    public class GetShipDetailsViewModel : IMapWith<Ship, GetShipDetailsViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; } = new Category();
        public ICollection<Trip>? Trips { get; set; } = null;

        public void Mapping(Profile profile) =>
            profile.CreateMap<Ship, GetShipDetailsViewModel>().MaxDepth(1);
    }

    public class GetShipShortlyViewModel : IMapWith<Ship, GetShipShortlyViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        
        public void Mapping(Profile profile) =>
            profile.CreateMap<Ship, GetShipShortlyViewModel>();
    }
    
}
