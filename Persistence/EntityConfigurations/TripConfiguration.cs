namespace Persistence.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Trip=Domain.Models.Service.Trip;

    public class TripConfiguration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.HasMany(p => p.Reviews)
                .WithOne(x => x.Trip);
            builder.HasMany(p => p.TicketClasses)
                .WithOne(x => x.Trip);
            builder.HasMany(p => p.Images)
                .WithOne(x => x.Trip);
        }
    }
}
