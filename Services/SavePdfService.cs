using System;
using System.Threading.Tasks;
using Azure.Storage.Files.Shares;
using Azure;
using System.IO;
using PdfSharp.Pdf;

namespace SamoaTaxServices
{
    public class SavePdfService
    {       
        private string directoryName = "InvoicePdfs";
        public string storageConnectionString { get; set; }
        public string storageName { get; set; }
        public string invoiceFileName { get; set; }
        public async Task<string> SavePdfInFileShare(PdfDocument invoicePDF)
        {          
            ShareClient shareClient = new ShareClient(storageConnectionString, storageName);

            if (await shareClient.ExistsAsync())
            {

                ShareDirectoryClient directory = shareClient.GetDirectoryClient(directoryName);
                if (await directory.ExistsAsync())
                {
                    ShareFileClient file = directory.GetFileClient(invoiceFileName);
                    
                    MemoryStream stream = new MemoryStream();
                    invoicePDF.Save(stream, false);
                    
                    file.Create(stream.Length);
                    file.UploadRange(
                            new HttpRange(0, stream.Length),
                            stream);
                    
                   return (directory.Uri.AbsoluteUri);

                }
                else
                {
                    throw new Exception("Directory not found.");
                }
            }
            else
            {
                throw new Exception("Share client failed to instantiate.");
            }
        }
    }
}
