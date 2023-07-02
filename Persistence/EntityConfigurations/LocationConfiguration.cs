namespace Persistence.EntityConfigurations
{
    using Domain.Models.Location;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {

        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(p => p.CityName).HasMaxLength(50);
            builder.Property(p => p.CountryName).HasMaxLength(50);
            builder.HasMany(p => p.Trips)
                .WithMany(x => x.Locations);
        }
    }
}
