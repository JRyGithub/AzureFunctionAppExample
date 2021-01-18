using SamoaTaxServices;
using Xunit;
using FluentAssertions;

namespace SamoaTaxFunctionsTests
{
    public class ValidationServiceShould
    {
        [Fact]
        public void ValidateStringIfStartOrEndWithBracketReturnTrue()
        {
            //Arrange
            ValidationService validate = new ValidationService();
            string positiveTestString= "{Test Positive}";
            //Act
            var result = validate.ValidateResponse(positiveTestString);
            //Assert
            result.Should().BeTrue();
        }
        [Fact]
        public void ValidateStringIfDoesNotEndWithBracketReturnFalse()
        {
            //Arrange
            ValidationService validate = new ValidationService();
            string positiveTestString = "{Test Positive";
            //Act
            var result = validate.ValidateResponse(positiveTestString);
            //Assert
            result.Should().BeFalse();
        }
        [Fact]
        public void ValidateStringIfDoesNotStartWithBracketReturnFalse()
        {
            //Arrange
            ValidationService validate = new ValidationService();
            string positiveTestString = "Test Positive}";
            //Act
            var result = validate.ValidateResponse(positiveTestString);
            //Assert
            result.Should().BeFalse();
        }
    }
}
