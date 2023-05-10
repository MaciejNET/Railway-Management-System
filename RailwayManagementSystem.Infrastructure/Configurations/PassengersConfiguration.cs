using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class PassengersConfiguration : IEntityTypeConfiguration<Passenger>
{
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FirstName)
            .HasConversion(x => x.Value, v => new FirstName(v))
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasConversion(x => x.Value, v => new LastName(v))
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, v => new Email(v))
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasConversion(x => x.Value, v => new PhoneNumber(v))
            .IsRequired();

        builder.Property(x => x.Age)
            .IsRequired();

        builder.HasOne(x => x.Discount)
            .WithMany(x => x.Passengers)
            .HasForeignKey(x => x.DiscountId);

        builder.HasMany(x => x.Tickets)
            .WithOne(x => x.Passenger)
            .HasForeignKey(x => x.PassengerId);
    }
}