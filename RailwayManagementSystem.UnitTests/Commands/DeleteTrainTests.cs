using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteTrainTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_TrainDeletedSuccessfully()
    {
        //Arrange
        var command = new DeleteTrain(Guid.NewGuid());
        
        var trainRepositoryMock = new Mock<ITrainRepository>();

        trainRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(Train.Create(
                id: new TrainId(command.Id),
                name: new TrainName("Test"),
                seatsAmount: 123,
                carrier: Carrier.Create(new CarrierId(Guid.NewGuid()), new CarrierName("Test"))));

        trainRepositoryMock
            .Setup(repo => repo.IsTrainInUse(It.IsAny<Train>()))
            .ReturnsAsync(false);
        
        var deleteTrainHandler = new DeleteTrainHandler(trainRepositoryMock.Object);
        
        //Act
        await deleteTrainHandler.HandleAsync(command);
        
        //Assert
        trainRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Train>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_TrainNotFound_ThrowsTrainNotFoundException()
    {
        //Arrange
        var command = new DeleteTrain(Guid.NewGuid());
        
        var trainRepositoryMock = new Mock<ITrainRepository>();

        trainRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Train) null);
        
        var deleteTrainHandler = new DeleteTrainHandler(trainRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteTrainHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TrainNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_TrainInUse_ThrowsTrainInUseException()
    {
        //Arrange
        var command = new DeleteTrain(Guid.NewGuid());
        
        var trainRepositoryMock = new Mock<ITrainRepository>();

        trainRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(Train.Create(
                id: new TrainId(command.Id),
                name: new TrainName("Test"),
                seatsAmount: 123,
                carrier: Carrier.Create(new CarrierId(Guid.NewGuid()), new CarrierName("Test"))));

        trainRepositoryMock
            .Setup(repo => repo.IsTrainInUse(It.IsAny<Train>()))
            .ReturnsAsync(true);
        
        var deleteTrainHandler = new DeleteTrainHandler(trainRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteTrainHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TrainInUseException>();
    }
}