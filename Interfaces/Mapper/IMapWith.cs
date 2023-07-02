namespace Interfaces.Mapper
{
    using AutoMapper;
    
    public interface IMapWith<T, TU>
    {
        public void Mapping(Profile profile) =>
            profile.CreateMap<T,TU>();
    }
}
