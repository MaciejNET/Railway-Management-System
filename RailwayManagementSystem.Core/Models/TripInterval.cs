namespace RailwayManagementSystem.Core.Models;

public class TripInterval
{
    public int Id { get; set; }
    public virtual Trip Trip { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
}