using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class UpdateEmailTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_EmailUpdatedSuccessfully()
    {
        //Arrange
        var command = new UpdateEmail(Id: Guid.NewGuid(), Email: "new.email@example.com");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var passenger = Passenger.Create(
            id: new UserId(command.Id),
            firstName: new FirstName("Jon"),
            lastName: new LastName("Doe"),
            email: new Email("old.email@example.com"),
            dateOfBirth: new DateOnly(1990, 10, 10),
            password: "hashedPassword");
        
        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(passenger);

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(false);

        var updateEmailHandler = new UpdateEmailHandler(passengerRepositoryMock.Object);

        //Act
        await updateEmailHandler.HandleAsync(command);

        //Assert
        passengerRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Passenger>()), Times.Once);
        passengerRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
        passengerRepositoryMock.Verify(repo => repo.ExistsByEmailAsync(command.Email), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_PassengerNotFound_ThrowsPassengerNotFoundException()
    {
        //Arrange
        var command = new UpdateEmail(Id: Guid.NewGuid(), Email: "new.email@example.com");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Passenger) null);

        var updateEmailHandler = new UpdateEmailHandler(passengerRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => updateEmailHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<PassengerNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_EmailAlreadyUsed_ThrowsPassengerWithGivenEmailAlreadyExistsException()
    {
        //Arrange
        var command = new UpdateEmail(Id: Guid.NewGuid(), Email: "new.email@example.com");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var passenger = Passenger.Create(
            id: new UserId(command.Id),
            firstName: new FirstName("Jon"),
            lastName: new LastName("Doe"),
            email: new Email("old.email@example.com"),
            dateOfBirth: new DateOnly(1990, 10, 10),
            password: "hashedPassword");

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(passenger);

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(true);

        var updateEmailHandler = new UpdateEmailHandler(passengerRepositoryMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => updateEmailHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<PassengerWithGivenEmailAlreadyExistsException>();
    }
}