namespace Identity.ViewModels
{
    using global::Interfaces.Mapper;
    using AutoMapper;
    using Models;

    public class UserViewModel : IMapWith<User, UserViewModel>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Role { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public void Mapping(Profile profile) => 
            profile.CreateMap<User, UserViewModel>();
    }
}
