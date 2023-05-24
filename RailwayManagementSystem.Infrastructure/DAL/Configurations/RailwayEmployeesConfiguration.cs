using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class RailwayEmployeesConfiguration : IEntityTypeConfiguration<RailwayEmployee>
{
    public void Configure(EntityTypeBuilder<RailwayEmployee> builder)
    {
        builder.ToTable("RailwayEmployees");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new UserId(v));
        
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new RailwayEmployeeName(v))
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, v => new FirstName(v))
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, v => new LastName(v))
            .IsRequired();
        
        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, v => new Password(v))
            .IsRequired();
    }
}