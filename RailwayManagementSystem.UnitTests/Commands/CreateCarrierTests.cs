using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateCarrierTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_CarrierAddedSuccessfully()
    {
        //Arrange
        var carrierId = new CarrierId(Guid.NewGuid());
        var command = new CreateCarrier(carrierId, "PKP Intercity");
        var carrierRepositoryMock = new Mock<ICarrierRepository>();

        carrierRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        var createCarrierHandler = new CreateCarrierHandler(carrierRepositoryMock.Object);

        //Act
        await createCarrierHandler.HandleAsync(command);
        
        //Assert
        carrierRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Carrier>()));
    }

    [Fact]
    public async Task HandleAsync_CarrierAlreadyExists_ThrowsCarrierAlreadyExistsException()
    {
        //Arrange
        var carrierId = new CarrierId(Guid.NewGuid());
        var command = new CreateCarrier(carrierId, "PKP Intercity");
        var carrierRepositoryMock = new Mock<ICarrierRepository>();
        
        carrierRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        var createCarrierHandler = new CreateCarrierHandler(carrierRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createCarrierHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<CarrierAlreadyExistsException>();
    }
}