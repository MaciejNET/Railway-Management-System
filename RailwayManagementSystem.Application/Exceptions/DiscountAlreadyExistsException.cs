using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class DiscountAlreadyExistsException : CustomException
{
    public string DiscountName { get; }

    public DiscountAlreadyExistsException(string discountName) : base(message: $"Discount with name: {discountName} already exists.", httpStatusCode: 400)
    {
        DiscountName = discountName;
    }
}