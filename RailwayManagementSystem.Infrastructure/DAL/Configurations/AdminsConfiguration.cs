using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class AdminsConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new UserId(v));

        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, v => new AdminName(v))
            .IsRequired();

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, v => new Password(v))
            .IsRequired();
    }
}