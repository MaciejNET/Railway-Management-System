using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class DiscountAlreadyExistsException(string discountName)
    : CustomException(message: $"Discount with name: {discountName} already exists.", httpStatusCode: 400)
{
    public string DiscountName { get; } = discountName;
}