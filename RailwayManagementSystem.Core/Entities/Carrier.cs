using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Carrier
{
    private readonly List<Train> _trains = new();
    
    public CarrierId Id { get; private set; }
    public CarrierName Name { get; private set; }
    public IReadOnlyList<Train> Trains => _trains.AsReadOnly();

    private Carrier(CarrierId id, CarrierName name, List<Train>? trains)
    {
        Id = id;
        Name = name;
        _trains = trains ?? new();
    }

    public static Carrier Create(CarrierId id, CarrierName name)
    {
        return new Carrier(id, name, null);
    }

    public static Carrier CreateWithTrains(CarrierId id, CarrierName name, List<Train> trains)
    {
        return new Carrier(id, name, trains);
    }

    public void AddTrain(Train train)
    {
        _trains.Add(train);
    }
    
    private Carrier() {}
}