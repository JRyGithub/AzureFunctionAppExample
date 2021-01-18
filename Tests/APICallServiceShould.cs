using FluentAssertions;
using Services;
using System;
using System.Net.Http;
using Xunit;

namespace FunctionsTests
{
    public class TaxCoreAPICallServiceShould
    {
        private string _Cn = "";

        [Fact]
        public void LoadCertificateIntoHandlerSuccess()
        {
            //Arrange
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;

            //Action
            handler.ClientCertificates.Add(APICallService.LoadMyCertificate(_Cn));
            //Assert
            handler.ClientCertificates.Should().HaveCountGreaterThan(0);
        }
        [Fact]
        public void LoadCertificateIntoHandlerFailure()
        {
            //Arrange
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            string incorrectCn = "Rubbish";
            //Action & Assert
            try
            {
                handler.ClientCertificates.Add(APICallService.LoadMyCertificate(incorrectCn));
            }catch(Exception e)
            {
                e.Should().NotBeNull();
            }
            
            
        }

    }
}
