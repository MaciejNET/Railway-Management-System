using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure;

public class RailwayManagementSystemDbContext : DbContext
{
    public RailwayManagementSystemDbContext(DbContextOptions opt) : base(opt)
    {
    }

    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<RailwayEmployee> RailwayEmployees => Set<RailwayEmployee>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Train> Trains => Set<Train>();
    public DbSet<Trip> Trips => Set<Trip>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RailwayManagementSystemDbContext).Assembly);

        modelBuilder.Entity<Ticket>().Navigation(x => x.Stations).AutoInclude();
        modelBuilder.Entity<Ticket>().Navigation(x => x.Trip).AutoInclude();
        modelBuilder.Entity<Ticket>().Navigation(x => x.Seat).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.Train).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.TripInterval).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.Schedules).AutoInclude();
        modelBuilder.Entity<Schedule>().Navigation(x => x.Station).AutoInclude();
    }
}