using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class LoginAdminTests
{
    [Fact]
    public async Task HandleAsync_ValidCredentials_AdminLoggedSuccessfully()
    {
        //Arrange
        var command = new LoginAdmin("Admin", "P@ssw0rd!");
        var adminRepositoryMock = new Mock<IAdminRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        var authenticatorMock = new Mock<IAuthenticator>();
        var tokenStorageMock = new Mock<ITokenStorage>();
        
        var admin = Admin.Create(new UserId(Guid.NewGuid()), "Admin", "hashedPassword");

        adminRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.Name))
            .ReturnsAsync(admin);
        
        passwordManagerMock
            .Setup(pm => pm.Validate(command.Password, admin.Password))
            .Returns(true);
        
        authenticatorMock
            .Setup(auth => auth.CreateToken(admin.Id, admin.Role.ToString().ToLowerInvariant()))
            .Returns(new JwtDto {AccessToken = "jwtToken"});
        
        var loginAdminHandler = new LoginAdminHandler(adminRepositoryMock.Object, passwordManagerMock.Object, authenticatorMock.Object, tokenStorageMock.Object);
        
        //Act
        await loginAdminHandler.HandleAsync(command);
        
        //Assert
        adminRepositoryMock.Verify(repo => repo.GetByNameAsync(command.Name), Times.Once);
        passwordManagerMock.Verify(pm => pm.Validate(command.Password, admin.Password), Times.Once);
        authenticatorMock.Verify(auth => auth.CreateToken(admin.Id, admin.Role.ToString().ToLowerInvariant()), Times.Once);
        tokenStorageMock.Verify(ts => ts.Set(It.IsAny<JwtDto>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidCredentials_ThrowsInvalidCredentialsException()
    {
        //Arrange
        var command = new LoginAdmin("Admin", "P@ssw0rd!");
        var adminRepositoryMock = new Mock<IAdminRepository>();
        var passwordManagerMock = new Mock<IPasswordManager>();
        var authenticatorMock = new Mock<IAuthenticator>();
        var tokenStorageMock = new Mock<ITokenStorage>();
        
        adminRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.Name))
            .ReturnsAsync((Admin)null);

        passwordManagerMock
            .Setup(pm => pm.Validate(command.Password, It.IsAny<string>()))
            .Returns(false);
        
        var loginAdminHandler = new LoginAdminHandler(adminRepositoryMock.Object, passwordManagerMock.Object, authenticatorMock.Object, tokenStorageMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => loginAdminHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidCredentialsException>();
        
        tokenStorageMock.Verify(ts => ts.Set(It.IsAny<JwtDto>()), Times.Never);
    }
}