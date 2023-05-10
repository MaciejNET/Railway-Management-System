using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class CarriersConfiguration : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new CarrierName(v))
            .IsRequired();

        builder.HasMany(x => x.Trains)
            .WithOne(x => x.Carrier)
            .HasForeignKey(x => x.CarrierId);
    }
}