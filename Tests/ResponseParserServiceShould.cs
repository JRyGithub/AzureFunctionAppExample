using Xunit;
using FluentAssertions;
using Services;
using Models;

namespace FunctionsTests
{
    public class ResponseParserServiceShould
    {
        #region Test data set up
        private string response = "";
        #endregion
        [Fact]
        public void ReturnAParsedResponseModel()
        {
            //Arrange
            ResponseParserService responseParser = new ResponseParserService();
            //Act
            var result = responseParser.parseResponse(response);
            //Assert
            result.Should().BeOfType<ParsedRequestModel>();
        }
        [Fact]
        public void HaveAParsedRequestModelWithSetPartitionKey()
        {
            // Arrange
            ResponseParserService responseParser = new ResponseParserService();
            ParsedRequestModel parsedRequestModel = responseParser.parseResponse(response);
            string partitionKey = "";
            //Act
            var result = parsedRequestModel.functionResponseModel.PartitionKey;
            //Assert
            result.Should().Be(partitionKey);

        }
        [Fact]
        public void HaveAParsedRequestModelWithSetRowKey()
        {
            // Arrange
            ResponseParserService responseParser = new ResponseParserService();
            ParsedRequestModel parsedRequestModel = responseParser.parseResponse(response);
            string rowKey = "";
            //Act
            var result = parsedRequestModel.functionResponseModel.RowKey;
            //Assert
            result.Should().Be(rowKey);

        }
        [Fact]
        public void HaveAParsedRequestModelWithSetQR()
        {
            // Arrange
            ResponseParserService responseParser = new ResponseParserService();
            ParsedRequestModel parsedRequestModel = responseParser.parseResponse(response);
            string qrCode = "";
            //Act
            var result = parsedRequestModel.qrCode;
            //Assert
            result.Should().Be(qrCode);

        }
        [Fact]
        public void HaveAParsedRequestModelWithSetInvoiceDetails()
        {
            // Arrange
            ResponseParserService responseParser = new ResponseParserService();
            ParsedRequestModel parsedRequestModel = responseParser.parseResponse(response);
            string invoiceDetails = "";
            //Act
            var result = parsedRequestModel.invoiceDetails;
            //Assert
            result.Should().Be(invoiceDetails);

        }
    }
}
