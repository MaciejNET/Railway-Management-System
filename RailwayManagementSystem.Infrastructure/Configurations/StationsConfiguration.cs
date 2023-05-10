using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class StationsConfiguration : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new StationName(v))
            .IsRequired();

        builder.Property(x => x.City)
            .HasConversion(x => x.Value, v => new City(v))
            .IsRequired();

        builder.Property(x => x.NumberOfPlatforms)
            .IsRequired();

        builder.HasMany(x => x.Schedule)
            .WithOne(x => x.Station)
            .HasForeignKey(x => x.StationId);

        builder.HasMany(x => x.Tickets)
            .WithMany(x => x.Stations)
            .UsingEntity(x => x.ToTable("TicketStation"));
    }
}