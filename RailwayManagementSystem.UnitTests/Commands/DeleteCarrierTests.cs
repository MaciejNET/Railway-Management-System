using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteCarrierTests
{
    [Fact]
    public async Task HandleAsync_CarrierExists_CarrierDeletedSuccessfully()
    {
        //Arrange
        var carrierId = new CarrierId(Guid.NewGuid());
        var command = new DeleteCarrier(carrierId);
        var carrierRepositoryMock = new Mock<ICarrierRepository>();
        
        var existingCarrier = Carrier.Create(carrierId, "PKP Intercity");

        carrierRepositoryMock
            .Setup(repo => repo.GetByIdAsync(carrierId))
            .ReturnsAsync(existingCarrier);
        
        var deleteCarrierHandler = new DeleteCarrierHandler(carrierRepositoryMock.Object);
        
        //Act
        await deleteCarrierHandler.HandleAsync(command);
        
        //Assert
        carrierRepositoryMock.Verify(repo => repo.DeleteAsync(existingCarrier), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_CarrierDoesNotExist_ThrowsCarrierNotFoundException()
    {
        //Arrange
        var carrierId = new CarrierId(Guid.NewGuid());
        var command = new DeleteCarrier(carrierId);
        var carrierRepositoryMock = new Mock<ICarrierRepository>();
        
        carrierRepositoryMock
            .Setup(repo => repo.GetByIdAsync(carrierId))
            .ReturnsAsync((Carrier)null);
        
        var deleteCarrierHandler = new DeleteCarrierHandler(carrierRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteCarrierHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<CarrierNotFoundException>();
    }
}