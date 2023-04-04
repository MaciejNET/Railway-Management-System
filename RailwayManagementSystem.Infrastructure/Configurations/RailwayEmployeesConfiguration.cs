using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class RailwayEmployeesConfiguration : IEntityTypeConfiguration<RailwayEmployee>
{
    public void Configure(EntityTypeBuilder<RailwayEmployee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                x => x.Value,
                v => RailwayEmployeeName.Create(v).Value)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasConversion(
                x => x.Value,
                v => FirstName.Create(v).Value)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(
                x => x.Value,
                v => LastName.Create(v).Value)
            .IsRequired();
    }
}