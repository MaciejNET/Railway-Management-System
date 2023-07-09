using RailwayManagementSystem.Core.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.Now;
}