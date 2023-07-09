using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateAdminTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_AdminAddedSuccessfully()
    {
        //Arrange
        var adminId = new UserId(Guid.NewGuid());
        var command = new CreateAdmin(adminId, "Admin", "P@ssw0rd!");
        var adminRepositoryMock = new Mock<IAdminRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();

        adminRepositoryMock
            .Setup(repo => repo.ExistByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        passwordManagerMock
            .Setup(pm => pm.Secure(It.IsAny<string>()))
            .Returns("securePassword");
        
        var createAdminHandler = new CreateAdminHandler(adminRepositoryMock.Object, passwordManagerMock.Object);
        
        //Act
        await createAdminHandler.HandleAsync(command);
        
        //Assert
        adminRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Admin>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidPassword_ThrowsInvalidPasswordException()
    {
        //Arrange
        var adminId = new UserId(Guid.NewGuid());
        var command = new CreateAdmin(adminId, "Admin", "invalidPassword");
        var adminRepositoryMock = new Mock<IAdminRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        
        var createAdminHandler = new CreateAdminHandler(adminRepositoryMock.Object, passwordManagerMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createAdminHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidPasswordException>();
    }

    [Fact]
    public async Task HandleAsync_AdminAlreadyExists_ThrowsAdminAlreadyExistsException()
    {
        //Arrange
        var adminId = new UserId(Guid.NewGuid());
        var command = new CreateAdmin(adminId, "Admin", "P@ssw0rd!");
        var adminRepositoryMock = new Mock<IAdminRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        
        adminRepositoryMock
            .Setup(repo => repo.ExistByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        var createAdminHandler = new CreateAdminHandler(adminRepositoryMock.Object, passwordManagerMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createAdminHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<AdminAlreadyExistsException>();
    }
}