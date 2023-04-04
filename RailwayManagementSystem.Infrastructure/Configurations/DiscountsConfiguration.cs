using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.Configurations;

public class DiscountsConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                x => x.Value,
                v => DiscountName.Create(v).Value);

        builder.Property(x => x.Percentage)
            .IsRequired();

        builder.HasMany(x => x.Passengers)
            .WithOne(x => x.Discount)
            .HasForeignKey(x => x.DiscountId);
    }
}