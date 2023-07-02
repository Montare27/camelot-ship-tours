namespace Api.ViewModels.Category
{
    using AutoMapper;
    using Domain.Models.Ship;
    using Interfaces.Mapper;

    public class CategoryViewModel : IMapWith<Category, CategoryViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile) => 
            profile.CreateMap<Category, CategoryViewModel>();
    }
}
