using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteDiscountTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_DiscountDeletedSuccessfully()
    {
        //Arrange
        var discountId = new DiscountId(Guid.NewGuid());
        var command = new DeleteDiscount(discountId);
        var discountRepositoryMock = new Mock<IDiscountRepository>();

        var discount = Discount.Create(discountId, "Student", 51);

        discountRepositoryMock
            .Setup(repo => repo.GetByIdAsync(discountId))
            .ReturnsAsync(discount);
        
        var deleteDiscountHandler = new DeleteDiscountHandler(discountRepositoryMock.Object);
        
        //Act
        await deleteDiscountHandler.HandleAsync(command);
        
        //Assert
        discountRepositoryMock.Verify(repo => repo.DeleteAsync(discount), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_DiscountNotFound_ThrowsDiscountNotFoundException()
    {
        var discountId = new DiscountId(Guid.NewGuid());
        var command = new DeleteDiscount(discountId);
        var discountRepositoryMock = new Mock<IDiscountRepository>();
        
        discountRepositoryMock
            .Setup(repo => repo.GetByIdAsync(discountId))
            .ReturnsAsync((Discount)null);
        
        var deleteDiscountHandler = new DeleteDiscountHandler(discountRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteDiscountHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<DiscountNotFoundException>();
    }
}