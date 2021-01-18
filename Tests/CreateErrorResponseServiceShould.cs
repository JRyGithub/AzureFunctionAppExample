using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Models;
using Services;

namespace FunctionsTests
{
    public class CreateErrorResponseServiceShould
    {
        [Fact]
        public void ReturnsAErrorMessageModel()
        {
            //Arrange
            CreateErrorResponseService createErrorResponse = new CreateErrorResponseService();
            Exception e = new Exception();
            JsonModel jsonModel = new JsonModel();

            //Act
            var result = createErrorResponse.Create(e, jsonModel);
            //Assert
            result.Should().BeOfType<ErrorResponseModel>();
        }
    }
}
