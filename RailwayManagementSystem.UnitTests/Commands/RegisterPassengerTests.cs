using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.UnitTests.Commands;

public class RegisterPassengerTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_PassengerRegisteredSuccessfully()
    {
        //Arrange
        var command = new RegisterPassenger(
            Id: Guid.NewGuid(),
            FirstName: "John",
            LastName: "Doe",
            Email: "test@example.com",
            Password: "P@ssw0rd121",
            DateOfBirth: new DateOnly(1990, 10, 10),
            null);

        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(false);

        passwordManagerMock
            .Setup(pm => pm.Secure(command.Password))
            .Returns("hashedPassword");

        var registerPassengerHandler = new RegisterPassengerHandler(
            passengerRepositoryMock.Object,
            discountRepositoryMock.Object,
            passwordManagerMock.Object);

        //Act
        await registerPassengerHandler.HandleAsync(command);

        //Assert
        passengerRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Core.Entities.Passenger>()), Times.Once);
    }

    [Fact]
    public async Task
        HandleAsync_PassengerWithGivenEmailAlreadyExists_ThrowsPassengerWithGivenEmailAlreadyExistsException()
    {
        //Arrange
        var command = new RegisterPassenger(
            Id: Guid.NewGuid(),
            FirstName: "John",
            LastName: "Doe",
            Email: "test@example.com",
            Password: "P@ssw0rd121",
            DateOfBirth: new DateOnly(1990, 10, 10),
            null);
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(true);

        var registerPassengerHandler = new RegisterPassengerHandler(passengerRepositoryMock.Object,
            discountRepositoryMock.Object, passwordManagerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => registerPassengerHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<PassengerWithGivenEmailAlreadyExistsException>();
    }

    [Fact]
    public async Task HandleAsync_InvalidPassword_ThrowsInvalidPasswordException()
    {
        //Arrange
        var command = new RegisterPassenger(
            Id: Guid.NewGuid(),
            FirstName: "John",
            LastName: "Doe",
            Email: "test@example.com",
            Password: "P@ssw0rd121",
            DateOfBirth: new DateOnly(1990, 10, 10),
            null);
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(false);

        var registerPassengerHandler = new RegisterPassengerHandler(passengerRepositoryMock.Object,
            discountRepositoryMock.Object, passwordManagerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => registerPassengerHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidPasswordException>();
    }

    [Fact]
    public async Task HandleAsync_DiscountNotFound_ThrowsDiscountNotFoundException()
    {
        //Arrange
        var command = new RegisterPassenger(
            Id: Guid.NewGuid(),
            FirstName: "John",
            LastName: "Doe",
            Email: "test@example.com",
            Password: "P@ssw0rd121",
            DateOfBirth: new DateOnly(1990, 10, 10),
            DiscountName: "test");

        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();

        passengerRepositoryMock
            .Setup(repo => repo.ExistsByEmailAsync(command.Email))
            .ReturnsAsync(false);
        discountRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.DiscountName))
            .ReturnsAsync((Core.Entities.Discount) null);

        var registerPassengerHandler = new RegisterPassengerHandler(passengerRepositoryMock.Object,
            discountRepositoryMock.Object, passwordManagerMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => registerPassengerHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountNotFoundException>();
    }
}