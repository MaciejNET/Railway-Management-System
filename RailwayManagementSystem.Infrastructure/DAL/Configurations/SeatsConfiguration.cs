using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class SeatsConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seats");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new SeatId(v));
        
        builder.Property(x => x.SeatNumber)
            .IsRequired();

        builder.Property(x => x.Place)
            .IsRequired();

        builder.HasMany(x => x.Tickets)
            .WithOne(x => x.Seat);
    }
}