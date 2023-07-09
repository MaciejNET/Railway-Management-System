using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Configurations;

internal sealed class PassengersConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.ToTable("Passengers");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, v => new UserId(v));
        
        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, v => new FirstName(v))
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, v => new LastName(v))
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, v => new Email(v))
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, v => new Password(v))
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasConversion(x => x.Value, v => new DateOfBirth(v))
            .IsRequired();

        builder.HasOne(x => x.Discount)
            .WithMany();

        builder.HasMany(x => x.Tickets)
            .WithOne()
            .HasForeignKey(x => x.PassengerId);
        
        builder.Property(x => x.Role)
            .IsRequired();
    }
}