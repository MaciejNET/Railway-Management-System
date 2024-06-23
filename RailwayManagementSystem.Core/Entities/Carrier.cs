using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Carrier
{
    private readonly List<Train> _trains = [];
    
    public CarrierId Id { get; private set; }
    public CarrierName Name { get; private set; }
    public IReadOnlyList<Train> Trains => _trains.AsReadOnly();

    private Carrier(CarrierId id, CarrierName name, List<Train>? trains)
    {
        Id = id;
        Name = name;
        _trains = trains ?? [];
    }

    public static Carrier Create(CarrierId id, CarrierName name)
    {
        return new Carrier(id, name, null);
    }

    public void AddTrain(Train train)
    {
        _trains.Add(train);
    }
    
    private Carrier() {}
}