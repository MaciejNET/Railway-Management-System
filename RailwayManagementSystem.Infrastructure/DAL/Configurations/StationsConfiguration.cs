using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class StationsConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.ToTable("Stations");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new StationId(v));
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new StationName(v))
            .IsRequired();

        builder.Property(x => x.City)
            .HasConversion(x => x.Value, v => new City(v))
            .IsRequired();

        builder.Property(x => x.NumberOfPlatforms)
            .IsRequired();
    }
}