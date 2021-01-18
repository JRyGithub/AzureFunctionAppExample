using Newtonsoft.Json.Linq;
using Models;
using System;

namespace Services
{
    public class ResponseParserService
    {
        public ParsedRequestModel responseModel = new ParsedRequestModel();
        public JObject apiResponseObject;

        public ParsedRequestModel parseResponse(string response)
        {
            try
            {
                apiResponseObject = JObject.Parse(response);
                SetResponseModelFunctionResponse();
                SetResponseModelPdfComponent();
            }
            catch(Exception e)
            {
                throw e;
            }
            return responseModel;
        }
        
        private void SetResponseModelFunctionResponse()
        {
            responseModel.functionResponseModel.PartitionKey = "";
            responseModel.functionResponseModel.RowKey = apiResponseObject["IN"].ToString();
        }
        
        private void SetResponseModelPdfComponent()
        {
            responseModel.invoiceDetails = apiResponseObject["Journal"].ToString();
            responseModel.qrCode = apiResponseObject["VerificationQRCode"].ToString();
        }
                    
                  
    }
}
