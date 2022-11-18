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
    public DbSet<TripInterval> TripIntervals => Set<TripInterval>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
            .HasMany(x => x.Schedules)
            .WithOne(x => x.Trip);

        modelBuilder.Entity<Train>()
            .HasMany(x => x.Seats)
            .WithOne(x => x.Train);
        
        modelBuilder.Entity<Seat>()
            .HasMany(x => x.Ticket)
            .WithOne(x => x.Seat);

        modelBuilder.Entity<Ticket>()
            .HasMany(x => x.Stations)
            .WithMany(x => x.Tickets)
            .UsingEntity(x => x.ToTable("TicketStation"));

        modelBuilder.Entity<Ticket>().Navigation(x => x.Stations).AutoInclude();
        modelBuilder.Entity<Ticket>().Navigation(x => x.Trip).AutoInclude();
        modelBuilder.Entity<Ticket>().Navigation(x => x.Seat).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.Train).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.TripInterval).AutoInclude();
        modelBuilder.Entity<Trip>().Navigation(x => x.Schedules).AutoInclude();
        modelBuilder.Entity<Schedule>().Navigation(x => x.Station).AutoInclude();
        
        modelBuilder.Entity<Carrier>().OwnsOne(x => x.Name);
        modelBuilder.Entity<Discount>().OwnsOne(x => x.Name);
        modelBuilder.Entity<Train>().OwnsOne(x => x.Name);
        modelBuilder.Entity<Passenger>().OwnsOne(x => x.FirstName);
        modelBuilder.Entity<Passenger>().OwnsOne(x => x.LastName);
        modelBuilder.Entity<Passenger>().OwnsOne(x => x.Email);
        modelBuilder.Entity<Passenger>().OwnsOne(x => x.PhoneNumber);
        modelBuilder.Entity<Station>().OwnsOne(x => x.Name);
        modelBuilder.Entity<Station>().OwnsOne(x => x.City);
        modelBuilder.Entity<Admin>().OwnsOne(x => x.Name);
        modelBuilder.Entity<RailwayEmployee>().OwnsOne(x => x.Name);
        modelBuilder.Entity<RailwayEmployee>().OwnsOne(x => x.FirstName);
        modelBuilder.Entity<RailwayEmployee>().OwnsOne(x => x.LastName);
    }
}