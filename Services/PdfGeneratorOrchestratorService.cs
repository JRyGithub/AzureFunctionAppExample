using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class PdfGeneratorOrchestratorService
    {
        private APICallService apiCallService;
        private ResponseParserService responseParserService;
        private PdfCreateAndSaveController createAndSavePdf;

        private AppSettingsOptions options;
        private JsonModel jsonModel;
        private string pdfName;
        private ParsedRequestModel parsedResponse;

        public PdfGeneratorOrchestratorService(AppSettingsOptions options, dynamic data)
        {
            this.options = options;
            jsonModel = data.JsonModel;
            pdfName = data.PdfName;
        }
        public async Task<IActionResult> ExecutePdfGeneration(ILogger log)
        {
            EstablishServices();

            try
            {
                var response = await apiCallService.ExecuteRequest(jsonModel, log);
                parsedResponse = responseParserService.parseResponse(response);
                parsedResponse.functionResponseModel.InvoiceNumber = jsonModel.InvoiceNumber;

                EstablishPdfCreationService();
                await Task.Run(() => createAndSavePdf.Execute(log));

            }
            catch (Exception e)
            {
                CreateErrorResponseService createErrorResponseService = new CreateErrorResponseService();
                return new OkObjectResult(createErrorResponseService.Create(e, jsonModel));
            }

            return new OkObjectResult(parsedResponse.functionResponseModel);
        }
        private void EstablishServices()
        {
            apiCallService = new APICallService();
            apiCallService.Cn = options.thumbKey;
            apiCallService.VSDCAddress = options.VSDCAddress;

            responseParserService = new ResponseParserService();
            createAndSavePdf = new PdfCreateAndSaveController();
        }
        private void EstablishPdfCreationService()
        {
            createAndSavePdf.storageConnectionString = options.connectionString;
            createAndSavePdf.storageName = options.storageName;
            createAndSavePdf.invoiceFileName = pdfName;
            createAndSavePdf.invoiceDetails = parsedResponse.invoiceDetails;
            createAndSavePdf.qrCode = parsedResponse.qrCode;
        }
    }
}

