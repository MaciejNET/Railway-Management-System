using Microsoft.EntityFrameworkCore;
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
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Train> Trains { get; set; }
    public DbSet<Trip> Trips { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}