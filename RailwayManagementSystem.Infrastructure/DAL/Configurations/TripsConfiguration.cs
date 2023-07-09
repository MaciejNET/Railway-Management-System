using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class TripsConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new TripId(v));
        
        builder.Property(x => x.Price)
            .HasPrecision(5, 2)
            .IsRequired();

        builder.HasOne(x => x.Train)
            .WithMany(x => x.Trips);

        builder.HasOne(x => x.Schedule)
            .WithOne()
            .HasForeignKey<Schedule>(x => x.TripId);

        builder.HasMany(x => x.Tickets)
            .WithOne(x => x.Trip);
    }
}