namespace RailwayManagementSystem.Application.Responses;

public class Connection()
{
    public List<ConnectionTrip> Trips { get; set; } = [];
    public int NoChanges { get; set; }
    public TimeOnly TotalTravelTime { get; set; }
    public decimal TotalPrice { get; set; }
    
    public Connection(Connection existingConnection) : this()
    {
        Trips.AddRange(existingConnection.Trips);
        NoChanges = existingConnection.NoChanges;
        TotalTravelTime = existingConnection.TotalTravelTime;
        TotalPrice = existingConnection.TotalPrice;
    }
}