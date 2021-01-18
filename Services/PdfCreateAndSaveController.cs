using Microsoft.Extensions.Logging;
using System;

namespace Services
{
    public class PdfCreateAndSaveController
    {
        private CreatePdfService createPdf = new CreatePdfService();
        private SavePdfService savePdf = new SavePdfService();

        public string storageConnectionString { get; set; }
        public string storageName { get; set; }
        public string invoiceFileName { get; set; }
        public string qrCode { get; set; }
        public string invoiceDetails { get; set; }
        public void Execute(ILogger log)
        {
            try
            {
                EstablishPdfServices();
                var pdf = createPdf.FromInvoiceAndQrCode(log);
                var pdfSavedLocation = savePdf.SavePdfInFileShare(pdf);
                log.LogInformation("Pdf saved to:" + pdfSavedLocation);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public void EstablishPdfServices()
        {
            createPdf.invoiceDetails = invoiceDetails;
            createPdf.qrCode = qrCode;

            savePdf.storageConnectionString = storageConnectionString;
            savePdf.storageName = storageName;
            savePdf.invoiceFileName = invoiceFileName;
        }
    }
}
