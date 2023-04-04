using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class TrainsConfiguration : IEntityTypeConfiguration<Train>
{
    public void Configure(EntityTypeBuilder<Train> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                x => x.Value,
                v => TrainName.Create(v).Value)
            .IsRequired();

        builder.Property(x => x.SeatsAmount)
            .IsRequired();

        builder.HasOne(x => x.Carrier)
            .WithMany(x => x.Trains)
            .HasForeignKey(x => x.CarrierId);

        builder.HasMany(x => x.Seats)
            .WithOne(x => x.Train)
            .HasForeignKey(x => x.TrainId);

        builder.HasMany(x => x.Seats)
            .WithOne(x => x.Train)
            .HasForeignKey(x => x.TrainId);
    }
}