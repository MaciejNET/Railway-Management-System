using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class TripsConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Price)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.HasOne(x => x.Train)
            .WithMany(x => x.Trips)
            .HasForeignKey(x => x.TrainId);

        builder.HasMany(x => x.Schedules)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId);

        builder.OwnsOne(x => x.TripInterval);

        builder.HasMany(x => x.Tickets)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId);
    }
}