using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class StationSchedulesConfiguration : IEntityTypeConfiguration<StationSchedule>
{
    public void Configure(EntityTypeBuilder<StationSchedule> builder)
    {
        builder.ToTable("StationSchedules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new StationScheduleId(v));

        builder.HasOne(x => x.Station);

        builder.Property(x => x.ArrivalTime)
            .IsRequired();
        
        builder.Property(x => x.DepartureTime)
            .IsRequired();
        
        builder.Property(x => x.Platform)
            .IsRequired();
    }
}