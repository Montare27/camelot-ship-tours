namespace Application.Services.WishList
{
    using global::Interfaces.Mapper;
    using global::Interfaces;
    using AutoMapper;
    using Domain.Models.Service;

    public class WishListTripViewModel : IMapWith<Trip, WishListTripViewModel>, ITrip
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        
        public void Mapping(Profile profile) =>
            profile.CreateMap<Trip, WishListTripViewModel>();
    }
}
