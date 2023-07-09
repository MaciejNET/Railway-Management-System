using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateTrainTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_TrainCreatedSuccessfully()
    {
        //Arrange
        var command = new CreateTrain(Guid.NewGuid(), "Test", 10, Guid.NewGuid());

        var trainRepositoryMock = new Mock<ITrainRepository>();
        var carrierRepositoryMock = new Mock<ICarrierRepository>();

        trainRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(command.Name))
            .ReturnsAsync(false);

        carrierRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.CarrierId))
            .ReturnsAsync(Carrier.Create(new CarrierId(command.CarrierId), new CarrierName("Test")));
        
        var createTrainHandler = new CreateTrainHandler(trainRepositoryMock.Object, carrierRepositoryMock.Object);
        
        //Act
        await createTrainHandler.HandleAsync(command);
        
        //Assert
        trainRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Train>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_TrainWithGivenNameAlreadyExists_ThrowsTrainWithGivenNameAlreadyExistsException()
    {
        //Arrange
        var command = new CreateTrain(Guid.NewGuid(), "Test", 10, Guid.NewGuid());

        var trainRepositoryMock = new Mock<ITrainRepository>();
        var carrierRepositoryMock = new Mock<ICarrierRepository>();

        trainRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(command.Name))
            .ReturnsAsync(true);
        
        var createTrainHandler = new CreateTrainHandler(trainRepositoryMock.Object, carrierRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createTrainHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TrainWithGivenNameAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_CarrierNotFound_ThrowsCarrierNotFoundException()
    {
        //Arrange
        var command = new CreateTrain(Guid.NewGuid(), "Test", 10, Guid.NewGuid());

        var trainRepositoryMock = new Mock<ITrainRepository>();
        var carrierRepositoryMock = new Mock<ICarrierRepository>();

        carrierRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.CarrierId))
            .ReturnsAsync((Carrier)null);
        
        var createTrainHandler = new CreateTrainHandler(trainRepositoryMock.Object, carrierRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createTrainHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<CarrierNotFoundException>();
    }
}