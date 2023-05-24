using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class CarriersConfiguration : IEntityTypeConfiguration<Carrier>
{
    public void Configure(EntityTypeBuilder<Carrier> builder)
    {
        builder.ToTable("Carriers");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new CarrierId(v));
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new CarrierName(v))
            .IsRequired();

        builder.HasMany(x => x.Trains)
            .WithOne(x => x.Carrier);
    }
}