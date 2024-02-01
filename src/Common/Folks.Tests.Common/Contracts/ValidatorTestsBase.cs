// Copyright (c) v-demyanov. All rights reserved.

using FluentValidation;
using FluentValidation.Results;

using Xunit;

namespace Folks.Tests.Common.Contracts;

public abstract class ValidatorTestsBase<TValidated, TValidator>
{
    public static void AssertInvalid(ValidationResult actualResult, ValidationFailure[] expectedErrors)
    {
        Assert.False(actualResult.IsValid);
        Assert.Equal(expectedErrors.Length, actualResult.Errors.Count);

        for (var i = 0; i < expectedErrors.Length; i++)
        {
            var expectedError = expectedErrors[i];
            var actualError = actualResult.Errors[i];

            Assert.Equal(expectedError.PropertyName, actualError.PropertyName);
            Assert.Equal(expectedError.ErrorMessage, actualError.ErrorMessage);
        }
    }

    public static void AssertValid(ValidationResult actualResult)
    {
        Assert.True(actualResult.IsValid);
        Assert.Empty(actualResult.Errors);
    }

    [Theory]
    [MemberData("InvalidMemberData")]
    public virtual void Validate_Invalid_ShouldReturnExpectedErrors(TValidated invalid, ValidationFailure[] expectedErrors)
    {
        // Arrange
        var validator = this.ArrangeValidator();

        // Act
        var actualResult = validator.Validate(invalid);

        // Assert
        AssertInvalid(actualResult, expectedErrors);
    }

    [Theory]
    [MemberData("ValidMemberData")]
    public void Validate_Valid_ShouldReturnNoErrors(TValidated valid)
    {
        // Arrange
        var validator = this.ArrangeValidator();

        // Act
        var actualResult = validator.Validate(valid);

        // Assert
        AssertValid(actualResult);
    }

    protected abstract AbstractValidator<TValidated> ArrangeValidator();
}
