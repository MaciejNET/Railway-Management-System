using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL;

internal sealed class DatabaseInitializer(
    IServiceProvider serviceProvider,
    TimeProvider timeProvider,
    IPasswordManager passwordManager)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RailwayManagementSystemDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (dbContext.AreAllTablesEmpty())
            await InitData(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task InitData(RailwayManagementSystemDbContext dbContext)
    {
        var adminPassword = passwordManager.Secure("Password123!");
        var admin = Admin.Create(
            id: Guid.NewGuid(),
            name: "Admin",
            password: adminPassword);

        await dbContext.Admins.AddAsync(admin);

        var firstDiscount = Discount.Create(
            id: Guid.NewGuid(),
            name: "Students",
            percentage: 51);

        var secondDiscount = Discount.Create(
            id: Guid.NewGuid(),
            name: "Seniors",
            percentage: 37);

        await dbContext.Discounts.AddRangeAsync(firstDiscount, secondDiscount);

        var carrier = Carrier.Create(
            id: Guid.NewGuid(),
            name: "Good trains");

        var firstTrain = Train.Create(
            id: Guid.NewGuid(),
            name: $"{carrier.Name.Value.ToLower().Underscore()}_{new Random().Next(1000, 10000)}",
            seatsAmount: 100,
            carrier: carrier);

        var secondTrain = Train.Create(
            id: Guid.NewGuid(),
            name: $"{carrier.Name.Value.ToLower().Underscore()}_{new Random().Next(1000, 10000)}",
            seatsAmount: 100,
            carrier: carrier);

        carrier.AddTrain(firstTrain);
        carrier.AddTrain(secondTrain);

        await dbContext.Carriers.AddAsync(carrier);
        await dbContext.Trains.AddRangeAsync(firstTrain, secondTrain);
        await dbContext.Seats.AddRangeAsync(firstTrain.Seats);
        await dbContext.Seats.AddRangeAsync(secondTrain.Seats);

        var firstPassengerPassword = passwordManager.Secure("Password123!");
        var firstPassenger = Passenger.Create(
            id: Guid.NewGuid(),
            firstName: "John",
            lastName: "Doe",
            email: "johndoe@mail.com",
            dateOfBirth: new DateOnly(1990, 5, 24),
            password: firstPassengerPassword);

        var secondPassengerPassword = passwordManager.Secure("Password123!");
        var secondPassenger = Passenger.CreateWithDiscount(
            id: Guid.NewGuid(),
            firstName: "Jack",
            lastName: "Pepper",
            email: "jack@mail.com",
            dateOfBirth: new DateOnly(2003, 4, 23),
            discount: firstDiscount,
            password: secondPassengerPassword);

        await dbContext.Passengers.AddRangeAsync(firstPassenger, secondPassenger);

        var starachowice = Station.Create(
            id: Guid.NewGuid(),
            name: "Starachowice",
            city: "Starachowice",
            numberOfPlatforms: 2);

        var kielce = Station.Create(
            id: Guid.NewGuid(),
            name: "Kielce",
            city: "Kielce",
            numberOfPlatforms: 6);

        var krakow = Station.Create(
            id: Guid.NewGuid(),
            name: "Krakow Glowny",
            city: "Krakow",
            numberOfPlatforms: 12);

        var zakopane = Station.Create(
            id: Guid.NewGuid(),
            name: "Zakopane",
            city: "Zakopane",
            numberOfPlatforms: 4);

        await dbContext.Stations.AddRangeAsync(starachowice, kielce, krakow, zakopane);

        var firstTripId = TripId.Create();
        var secondTripId = TripId.Create();
        var thirdTripId = TripId.Create();

        List<StationSchedule> firstTripStations = new()
        {
            StationSchedule.Create(starachowice, new TimeOnly(10, 00), new TimeOnly(10, 00), 1),
            StationSchedule.Create(kielce, new TimeOnly(11, 00), new TimeOnly(11, 15), 1),
            StationSchedule.Create(krakow, new TimeOnly(12, 30), new TimeOnly(13, 00), 1),
            StationSchedule.Create(zakopane, new TimeOnly(14, 30), new TimeOnly(14, 30), 1)
        };

        List<StationSchedule> secondTripStations = new()
        {
            StationSchedule.Create(starachowice, new TimeOnly(9, 00), new TimeOnly(9, 00), 2),
            StationSchedule.Create(kielce, new TimeOnly(10, 00), new TimeOnly(10, 05), 1),
            StationSchedule.Create(krakow, new TimeOnly(12, 15), new TimeOnly(12, 15), 1),
        };

        List<StationSchedule> thirdTripStations = new()
        {
            StationSchedule.Create(krakow, new TimeOnly(13, 30), new TimeOnly(13, 30), 1),
            StationSchedule.Create(zakopane, new TimeOnly(14, 45), new TimeOnly(14, 45), 1)
        };

        var firstTripStationSchedules = firstTripStations.OrderBy(x => x.DepartureTime).ToList();
        var secondTripStationSchedules = secondTripStations.OrderBy(x => x.DepartureTime).ToList();
        var thirdTripStationSchedules = thirdTripStations.OrderBy(x => x.DepartureTime).ToList();

        var firstTripSchedule = Schedule.Create(
            tripId: firstTripId,
            validDate: new ValidDate(DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime), DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime.AddMonths(3))),
            tripAvailability: new TripAvailability(
                Monday: true,
                Tuesday: true,
                Wednesday: true,
                Thursday: true,
                Friday: true,
                Saturday: false,
                Sunday: false));

        foreach (var station in firstTripStationSchedules)
        {
            firstTripSchedule.AddStationSchedule(station);
        }

        var secondTripSchedule = Schedule.Create(
            tripId: secondTripId,
            validDate: new ValidDate(DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime), DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime.AddMonths(3))),
            tripAvailability: new TripAvailability(
                Monday: true,
                Tuesday: true,
                Wednesday: true,
                Thursday: true,
                Friday: true,
                Saturday: true,
                Sunday: true));

        foreach (var station in secondTripStationSchedules)
        {
            secondTripSchedule.AddStationSchedule(station);
        }

        var thirdTripSchedule = Schedule.Create(
            tripId: thirdTripId,
            validDate: new ValidDate(DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime), DateOnly.FromDateTime(timeProvider.GetUtcNow().DateTime.AddMonths(3))),
            tripAvailability: new TripAvailability(
                Monday: false,
                Tuesday: false,
                Wednesday: true,
                Thursday: true,
                Friday: true,
                Saturday: true,
                Sunday: false));
        
        foreach (var station in thirdTripStationSchedules)
        {
            thirdTripSchedule.AddStationSchedule(station);
        }

        var firstTrip = Trip.Create(
            id: firstTripId,
            price: 25.00M,
            train: firstTrain,
            schedule: firstTripSchedule);
        
        var secondTrip = Trip.Create(
            id: secondTripId,
            price: 20.00M,
            train: secondTrain,
            schedule: secondTripSchedule);
        
        var thirdTrip = Trip.Create(
            id: thirdTripId,
            price: 15.00M,
            train: secondTrain,
            schedule: thirdTripSchedule);

        await dbContext.Trips.AddRangeAsync(firstTrip, secondTrip, thirdTrip);
        await dbContext.Schedules.AddRangeAsync(firstTripSchedule, secondTripSchedule, thirdTripSchedule);
        await dbContext.StationSchedules.AddRangeAsync(firstTripStations);
        await dbContext.StationSchedules.AddRangeAsync(secondTripStations);
        await dbContext.StationSchedules.AddRangeAsync(thirdTripStations);
        
        secondTrip.ReserveTicket(firstPassenger, starachowice, kielce, timeProvider.GetUtcNow().DateTime.AddDays(1));

        await dbContext.Tickets.AddRangeAsync(secondTrip.Tickets);

        await dbContext.SaveChangesAsync();
    }
}