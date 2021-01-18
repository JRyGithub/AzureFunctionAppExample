using System.IO;
using Xunit;

namespace FunctionsTests
{

    public class CreatePdfServiceShould
    {
        #region Test data set ups
        public string qrCode = "";
        public string invoiceDetails = "";

        
        #endregion
        [Theory]
        [InlineData("Verdana.ttf")]
        public void CheckFontFile_IfFontFileExists_ReturnTrue(string name)
        {
            //Arrange
            string fontFileName = $"";
            bool exists;
            //Act
            exists = File.Exists(fontFileName);
            //Assert
            Assert.True(exists);
        }
    }
}
