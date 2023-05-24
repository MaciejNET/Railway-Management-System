using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class TrainsConfiguration : IEntityTypeConfiguration<Train>
{
    public void Configure(EntityTypeBuilder<Train> builder)
    {
        builder.ToTable("Trains");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new TrainId(v));
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new TrainName(v))
            .IsRequired();

        builder.Property(x => x.SeatsAmount)
            .IsRequired();

        builder.HasOne(x => x.Carrier)
            .WithMany(x => x.Trains);

        builder.HasMany(x => x.Seats)
            .WithOne()
            .HasForeignKey(x => x.TrainId);

        builder.HasMany(x => x.Seats)
            .WithOne()
            .HasForeignKey(x => x.TrainId);
    }
}