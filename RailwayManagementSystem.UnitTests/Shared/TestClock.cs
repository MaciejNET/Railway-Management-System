using RailwayManagementSystem.Core.Abstractions;

namespace RailwayManagementSystem.UnitTests.Shared;

public class TestClock : IClock
{
    public DateTime Current() => new DateTime(2023, 7, 10, 12, 0, 0);
}