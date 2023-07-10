namespace RailwayManagementSystem.Application.Responses;

public class Connection
{
    public List<ConnectionTrip> Trips { get; set; } = new();
    public int NoChanges { get; set; }
    public TimeOnly TotalTravelTime { get; set; }
    public decimal TotalPrice { get; set; }

    public Connection()
    {
        
    }
    
    public Connection(Connection existingConnection)
    {
        Trips.AddRange(existingConnection.Trips);
        NoChanges = existingConnection.NoChanges;
        TotalTravelTime = existingConnection.TotalTravelTime;
        TotalPrice = existingConnection.TotalPrice;
    }
}