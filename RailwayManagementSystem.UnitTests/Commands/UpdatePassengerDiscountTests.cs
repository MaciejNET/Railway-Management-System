using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class UpdatePassengerDiscountTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_DiscountUpdatedSuccessfully()
    {
        //Arrange
        var passengerId = Guid.NewGuid();
        var command = new UpdatePassengerDiscount(passengerId, "Student");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        
        var passenger = Passenger.Create(
            id: new UserId(passengerId),
            firstName: new FirstName("Jon"),
            lastName: new LastName("Doe"),
            email: new Email("old.email@example.com"),
            dateOfBirth: new DateOnly(1990, 10, 10),
            password: "hashedPassword");

        var discount = Discount.Create(new DiscountId(Guid.NewGuid()), "Student", 51);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.PassengerId))
            .ReturnsAsync(passenger);

        discountRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.DiscountName))
            .ReturnsAsync(discount);

        var updatePassengerDiscountHandler = new UpdatePassengerDiscountHandler(passengerRepositoryMock.Object, discountRepositoryMock.Object);
        
        //Act
        await updatePassengerDiscountHandler.HandleAsync(command);
        
        //Assert
        passengerRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Passenger>()), Times.Once);
        discountRepositoryMock.Verify(repo => repo.GetByNameAsync(It.IsAny<string>()), Times.Once);
        passenger.Discount.Should().Be(discount);
    }

    [Fact]
    public async Task HandleAsync_PassengerNotFound_ThrowsPassengerNotFoundException()
    {
        //Arrange
        var command = new UpdatePassengerDiscount(Guid.NewGuid(), "Student");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.PassengerId))
            .ReturnsAsync((Passenger) null);

        var updatePassengerDiscountHandler = new UpdatePassengerDiscountHandler(passengerRepositoryMock.Object, discountRepositoryMock.Object);
       
        //Act
        var exception = await Record.ExceptionAsync(() => updatePassengerDiscountHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<PassengerNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_DiscountNotFound_ThrowsDiscountNotFoundException()
    {
        //Arrange
        var passengerId = Guid.NewGuid();
        var command = new UpdatePassengerDiscount(passengerId, "Student");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        
        var passenger = Passenger.Create(
            id: new UserId(passengerId),
            firstName: new FirstName("Jon"),
            lastName: new LastName("Doe"),
            email: new Email("old.email@example.com"),
            dateOfBirth: new DateOnly(1990, 10, 10),
            password: "hashedPassword");
        
        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.PassengerId))
            .ReturnsAsync(passenger);

        discountRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.DiscountName))
            .ReturnsAsync((Discount) null);
        
        var updatePassengerDiscountHandler = new UpdatePassengerDiscountHandler(passengerRepositoryMock.Object, discountRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => updatePassengerDiscountHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountNotFoundException>();
    }
}