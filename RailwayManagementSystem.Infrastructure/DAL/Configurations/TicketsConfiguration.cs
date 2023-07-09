using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new TicketId(v));
        
        builder.Property(x => x.Price)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.HasOne(x => x.Seat)
            .WithMany(x => x.Tickets);
        
        builder.Property(x => x.TripDate)
            .IsRequired();

        builder.HasMany(x => x.Stations)
            .WithMany(x => x.Tickets)
            .UsingEntity(x => x.ToTable("TicketStation"));
        
        builder.HasOne(x => x.Trip)
            .WithMany(x => x.Tickets);
    }
}