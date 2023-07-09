using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteAdminTests
{
    [Fact]
    public async Task HandleAsync_AdminExists_AdminDeletedSuccessfully()
    {
        //Arrange
        var adminId = new UserId(Guid.NewGuid());
        var command = new DeleteAdmin(adminId);
        var adminRepositoryMock = new Mock<IAdminRepository>();
        
        var existingAdmin = Admin.Create(adminId, "Admin", "P@ssw0rd!");

        adminRepositoryMock
            .Setup(repo => repo.GetByIdAsync(adminId))
            .ReturnsAsync(existingAdmin);

        var deleteAdminHandler = new DeleteAdminHandler(adminRepositoryMock.Object);
        
        //Act
        await deleteAdminHandler.HandleAsync(command);
        
        //Assert
        adminRepositoryMock.Verify(repo => repo.DeleteAsync(existingAdmin), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_AdminDoesNotExist_ThrowsAdminNotFoundException()
    {
        //Arrange
        var adminId = new UserId(Guid.NewGuid());
        var command = new DeleteAdmin(adminId);
        var adminRepositoryMock = new Mock<IAdminRepository>();
        
        adminRepositoryMock
            .Setup(repo => repo.GetByIdAsync(adminId))
            .ReturnsAsync((Admin)null);
        
        var deleteAdminHandler = new DeleteAdminHandler(adminRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteAdminHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<AdminNotFoundException>();
    }
}