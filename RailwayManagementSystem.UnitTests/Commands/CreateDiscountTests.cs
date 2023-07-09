using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateDiscountTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_DiscountAddedSuccessfully()
    {
        //Assert
        var discountId = new DiscountId(Guid.NewGuid());
        var command = new CreateDiscount(discountId, "Students", 51);
        var discountRepositoryMock = new Mock<IDiscountRepository>();

        discountRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        var createDiscountHandler = new CreateDiscountHandler(discountRepositoryMock.Object);
        
        //Act
        await createDiscountHandler.HandleAsync(command);
        
        //Assert
        discountRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Discount>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_DiscountAlreadyExists_ThrowsDiscountAlreadyExistsException()
    {
        //Arrange
        var discountId = new DiscountId(Guid.NewGuid());
        var command = new CreateDiscount(discountId, "Students", 51);
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        
        discountRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        var createDiscountHandler = new CreateDiscountHandler(discountRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createDiscountHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountAlreadyExistsException>();
    }
}