namespace Api.ViewModels.Trip
{
    using AutoMapper;
    using Domain.Models.Service;
    using Interfaces.Mapper;

    public class AddImagesViewModel
    {
        public int? TripId { get; set; }
        public List<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();
    }

    public class ImageViewModel : IMapWith<Image, ImageViewModel>
    {
        public string Encoded { get; set; } = string.Empty;
        
        public void Mapping(Profile profile) =>
            profile.CreateMap<Image, ImageViewModel>();
    }
}
