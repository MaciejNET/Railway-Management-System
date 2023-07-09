using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Trip
{
    private readonly List<Ticket> _tickets = new();
    
    public TripId Id { get; private set; }
    public decimal Price { get; private set; }
    public Train Train { get; private set; }
    public Schedule Schedule { get; private set; }
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

    private Trip(TripId id, decimal price, Train train, Schedule schedule)
    {
        Id = id;
        Price = price;
        Train = train;
        Schedule = schedule;
    }

    public static Trip Create(TripId id, decimal price, Train train, Schedule schedule)
    {
        return new Trip(id, price, train, schedule);
    }

    public void ReserveTicket(Passenger passenger, Station startStation, Station endStation, DateTime tripDate, Seat? seat = null)
    {
        if (Schedule.IsTripRunningOnGivenDate(tripDate) is false)
        {
            throw new TrainDoesNotRunOnGivenDateException(Train.Name, tripDate);
        }

        if (Schedule.Stations.Any(x => x.Station == startStation) is false || Schedule.Stations.Any(x => x.Station == endStation) is false)
        {
            throw new StationDoesNotExistOnScheduleException();
        }
        
        var availableSeats = GetAvailableSeats(startStation, endStation, tripDate);

        if (!availableSeats.Any())
        {
            throw new NoSeatsAvailableForReservationException();
        }

        if (seat is not null && !availableSeats.Contains(seat))
        {
            throw new SeatNoAvailableException();
        }

        seat ??= availableSeats.First();

        var discount = passenger.Discount is null ? 1 : passenger.Discount.Percentage / 100;
            
        var tripStations = Schedule.Stations.Select(x => x.Station).ToList();
        var stationsToBook = GetStationsToBook(tripStations, startStation, endStation).ToList();
            
        var ticket = new Ticket(
            trip: this,
            passengerId: passenger.Id,
            price: Price * discount,
            seat: seat,
            tripDate: tripDate,
            stations: stationsToBook);

        _tickets.Add(ticket);
        seat.AddTicket(ticket);
        passenger.AddTicket(ticket);
    }

    public List<Seat> GetAvailableSeats(Station startStation, Station endStation, DateTime tripDate)
    {
        var bookedStations = GetBookedStationsForSeat(tripDate);
        var tripStations = Schedule.Stations.Select(x => x.Station).ToList();
        var stationsToBook = GetStationsToBook(tripStations, startStation, endStation);

        List<Seat> availableSeats = new();
        foreach (var (seat, stations) in bookedStations)
        {
            if (!stations.Any(x => stationsToBook.Contains(x)))
            {
                availableSeats.Add(seat);
            }
        }

        return availableSeats;
    }

    private Dictionary<Seat, List<Station>> GetBookedStationsForSeat(DateTime tripDate)
    {
        Dictionary<Seat, List<Station>> bookedStationsForSeat = new();
        foreach (var seat in Train.Seats)
        {
            var ticketsForSeat = seat.Tickets.Where(x => x.TripDate == tripDate).ToList();

            if (!ticketsForSeat.Any())
            {
                bookedStationsForSeat.Add(seat, new());
            }
            else
            {
                var bookedStations = ticketsForSeat.SelectMany(x => x.Stations).ToList();
                bookedStations.Reverse();
                if (bookedStations.Count > 0)
                    bookedStations.RemoveAt(bookedStations.Count - 1);
                bookedStationsForSeat.Add(seat, bookedStations);
            }
        }

        return bookedStationsForSeat;
    }
    
    private static IEnumerable<Station> GetStationsToBook(List<Station> stations, Station startStation, Station endStation)
    {
        var startStationIndex = stations.IndexOf(startStation);
        var endStationIndex = stations.IndexOf(endStation);
        var stationsToBook = stations.GetRange(startStationIndex, endStationIndex - startStationIndex);

        return stationsToBook;
    }

    private Trip() {}
}