using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class SchedulesConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new ScheduleId(v));
        
        builder.HasOne(x => x.Station)
            .WithMany();

        builder.Property(x => x.ArrivalTime)
            .IsRequired();

        builder.Property(x => x.DepartureTime)
            .IsRequired();

        builder.Property(x => x.Platform)
            .IsRequired();
    }
}