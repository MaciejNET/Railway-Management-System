using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Infrastructure.DAL;

internal sealed class RailwayManagementSystemDbContext : DbContext
{
    public RailwayManagementSystemDbContext(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Carrier> Carriers { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Passenger> Passengers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<StationSchedule> StationSchedules { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    public bool AreAllTablesEmpty()
    {
        var dbSetProperties = GetType().GetProperties()
            .Where(p => p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

        foreach (var property in dbSetProperties)
        {
            var dbSet = (IEnumerable)property.GetValue(this);
            if (dbSet.Cast<object>().Any())
            {
                return false;
            }
        }

        return true;
    }
}