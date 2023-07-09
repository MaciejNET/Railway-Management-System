using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class LoginPassengerTests
{
    [Fact]
    public async Task HandleAsync_ValidCredentials_PassengerLoggedSuccessfully()
    {
        // Arrange
        var command = new LoginPassenger("test@example.com", "P@ssw0rd!");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var authenticatorMock = new Mock<IAuthenticator>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        var tokenStorageMock = new Mock<ITokenStorage>();

        var passenger = Passenger.Create(
            id: new UserId(Guid.NewGuid()),
            firstName: new FirstName("John"),
            lastName: new LastName("Doe"),
            email: new Email("test@example.com"),
            dateOfBirth: new DateOfBirth(new DateOnly(1990, 10, 10)),
            password: "hashedPassword");

        passengerRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(command.Email))
            .ReturnsAsync(passenger);

        passwordManagerMock
            .Setup(pm => pm.Validate(command.Password, passenger.Password))
            .Returns(true);

        authenticatorMock
            .Setup(auth => auth.CreateToken(passenger.Id, passenger.Role.ToString().ToLowerInvariant()))
            .Returns(new JwtDto {AccessToken = "jwtToken"});

        var loginPassengerHandler = new LoginPassengerHandler(passengerRepositoryMock.Object, authenticatorMock.Object,
            passwordManagerMock.Object, tokenStorageMock.Object);

        //Act
        await loginPassengerHandler.HandleAsync(command);

        //Assert
        passengerRepositoryMock.Verify(repo => repo.GetByEmailAsync(command.Email), Times.Once);
        passwordManagerMock.Verify(pm => pm.Validate(command.Password, passenger.Password), Times.Once);
        authenticatorMock.Verify(auth => auth.CreateToken(passenger.Id, passenger.Role.ToString().ToLowerInvariant()),
            Times.Once);
        tokenStorageMock.Verify(ts => ts.Set(It.IsAny<JwtDto>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidCredentials_ThrowsInvalidCredentialsException()
    {
        // Arrange
        var command = new LoginPassenger("test@example.com", "P@ssw0rd!");
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var authenticatorMock = new Mock<IAuthenticator>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        var tokenStorageMock = new Mock<ITokenStorage>();

        passengerRepositoryMock
            .Setup(repo => repo.GetByEmailAsync(command.Email))
            .ReturnsAsync((Passenger) null);

        passwordManagerMock
            .Setup(pm => pm.Validate(command.Password, It.IsAny<string>()))
            .Returns(false);

        var loginPassengerHandler = new LoginPassengerHandler(passengerRepositoryMock.Object, authenticatorMock.Object,
            passwordManagerMock.Object, tokenStorageMock.Object);

        //Act
        var exception = await Record.ExceptionAsync(() => loginPassengerHandler.HandleAsync(command));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidCredentialsException>();
    }
}