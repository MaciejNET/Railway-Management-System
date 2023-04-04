using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class SchedulesConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Trip)
            .WithMany(x => x.Schedules)
            .HasForeignKey(x => x.TripId);

        builder.HasOne(x => x.Station)
            .WithMany(x => x.Schedule)
            .HasForeignKey(x => x.StationId);

        builder.Property(x => x.ArrivalTime)
            .IsRequired();

        builder.Property(x => x.DepartureTime)
            .IsRequired();

        builder.Property(x => x.Platform)
            .IsRequired();
    }
}