using FluentAssertions;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Entities;

public class PassengerTests
{
    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@.com")]
    [InlineData("test.com")]
    [InlineData("test@com")]
    [InlineData("test@com.")]
    public void Create_Email_With_InvalidEmail_ShouldThrowInvalidEmailException(string email)
    {
        //Arrange
        
        //Act
        var exception = Record.Exception(() => new Email(email));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidEmailException>();
    }
    
    [Fact]
    public void Create_Email_With_ValidEmail_ShouldSucceed()
    {
        // Arrange
        var validEmail = "test@example.com";

        // Act
        var email = new Email(validEmail);

        // Assert
        email.Value.Should().Be(validEmail);
    }
    
    [Fact]
    public void Create_DateOfBirth_With_ValidDateOfBirth_ShouldSucceed()
    {
        // Arrange
        var validDateOfBirth = new DateOnly(1990, 5, 15);

        // Act
        var dateOfBirth = new DateOfBirth(validDateOfBirth);

        // Assert
        dateOfBirth.Value.Should().Be(validDateOfBirth);
    }

    [Fact]
    public void Create_DateOfBirth__InvalidYearOfBirth_ShouldThrowInvalidYearOfBirthException()
    {
        // Arrange
        var invalidDateOfBirth = new DateOnly(1890, 8, 23);

        // Act
        var exception = Record.Exception(() => new DateOfBirth(invalidDateOfBirth));

        // Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidYearOfBirthException>();
    }

    [Fact]
    public void Create_DateOfBirth_With_InvalidAge_ShouldThrowInvalidAgeException()
    {
        // Arrange
        var invalidDateOfBirth = new DateOnly(DateTime.Now.Year - 12, 7, 10);

        // Act
        var exception = Record.Exception(() => new DateOfBirth(invalidDateOfBirth));

        // Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidAgeException>();
    }
    
    [Theory]
    [InlineData("Abc123!@#")]
    [InlineData("MyPa$$w0rd")]
    [InlineData("SecureP@55w0rd!")]
    public void ValidatePassword_ValidPassword_ShouldReturnTrue(string password)
    {
        // Act
        var isValid = Password.ValidatePassword(password);

        // Assert
        isValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abc123")]
    [InlineData("PASSWORD")]
    [InlineData("password")]
    [InlineData("Password123")]
    [InlineData("123456")]
    [InlineData("!@#$%^&*")]
    [InlineData("TooLongPassword123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
    public void ValidatePassword_InvalidPassword_ShouldReturnFalse(string password)
    {
        // Act
        var isValid = Password.ValidatePassword(password);

        // Assert
        isValid.Should().BeFalse();
    }
}