namespace RailwayManagementSystem.Core.ValueObjects;

public record TripInterval(
    bool Monday,
    bool Tuesday,
    bool Wednesday,
    bool Thursday,
    bool Friday,
    bool Saturday,
    bool Sunday);