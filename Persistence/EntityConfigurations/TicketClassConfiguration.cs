namespace Persistence.EntityConfigurations
{
    using Domain.Models.Ticket;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketClassConfiguration : IEntityTypeConfiguration<TicketClass>
    {

        public void Configure(EntityTypeBuilder<TicketClass> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.HasMany(p => p.Tickets)
                .WithOne(x => x.TicketClass);
        }
    }
}
