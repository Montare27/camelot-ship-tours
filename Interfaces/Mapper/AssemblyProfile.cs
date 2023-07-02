namespace Interfaces.Mapper
{
    using AutoMapper;
    using System.Reflection;

    public class AssemblyProfile : Profile
    {
        public AssemblyProfile(Assembly assembly) =>
            AssemblyProfileMapping(assembly);

        private void AssemblyProfileMapping(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(y=>y.GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IMapWith<,>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var method = type.GetMethod("Mapping");
                method?.Invoke(instance, new object[]{this});
            }
        }

    }
}
