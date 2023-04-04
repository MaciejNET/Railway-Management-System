using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class SeatsConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SeatNumber)
            .IsRequired();

        builder.Property(x => x.Place)
            .IsRequired();

        builder.HasOne(x => x.Train)
            .WithMany(x => x.Seats)
            .HasForeignKey(x => x.TrainId);

        builder.HasMany(x => x.Ticket)
            .WithOne(x => x.Seat)
            .HasForeignKey(x => x.SeatId);
    }
}