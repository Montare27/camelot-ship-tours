namespace Persistence.EntityConfigurations
{
    using Domain.Models.Ship;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.HasMany(p => p.Ships)
                .WithOne(x => x.Category);
        }
    }
}
