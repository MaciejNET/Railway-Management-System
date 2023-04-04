using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Trip)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.TripId);

        builder.HasOne(x => x.Passenger)
            .WithMany(x => x.Tickets)
            .HasForeignKey(x => x.PassengerId);

        builder.Property(x => x.Price)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.HasOne(x => x.Seat)
            .WithMany(x => x.Ticket)
            .HasForeignKey(x => x.SeatId);

        builder.Property(x => x.TripDate)
            .IsRequired();
        
        builder.HasMany(x => x.Stations)
            .WithMany(x => x.Tickets)
            .UsingEntity(x => x.ToTable("TicketStation"));
    }
}