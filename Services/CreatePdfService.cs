using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PdfSharp.Fonts;

namespace Services
{
    public class CreatePdfService
    {
        public string qrCode { get; set; }
        public string invoiceDetails { get; set; }
        public string invoiceFileName { get; set; }
        private char[] deliminaterChars = { '\r', '\n' };
        public PdfDocument FromInvoiceAndQrCode(ILogger log)
        {           
            try
            {

                PdfDocument invoicePdf = CreateBlankPdfWithTitle();
                PdfPage pdfPage = invoicePdf.AddPage();

                string[] invoicePDFStringArray = ParseInvoiceIntoStringFormat();
                
                string invoiceStringPreQRCode = string.Join(Environment.NewLine, invoicePDFStringArray.Take(invoicePDFStringArray.Count() - 1));

                string invoiceStringPostQRCode = invoicePDFStringArray.Last();

                var qRImage = GetQrImage();                            

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var enc1252 = Encoding.GetEncoding(1252);

                var font = RegisterFont(log);

                DrawPdf(invoiceStringPreQRCode, invoiceStringPostQRCode, font, qRImage, pdfPage);

                return invoicePdf;               
            }
           
            catch (Exception e)
            {
                throw new Exception("Pdf creation failed." + e.Message);
            }
        }
        private PdfDocument CreateBlankPdfWithTitle ()
        {
            PdfDocument invoicePDF = new PdfDocument();
            invoicePDF.Info.Title = invoiceFileName;

            return invoicePDF;
        }
        private PdfPage AddBlankPageToPdf(PdfDocument pdf)
        {
            return pdf.AddPage();
           
        }
        private string[] ParseInvoiceIntoStringFormat()
        {
            string[] invoicePDFStringArray =  invoiceDetails.Split(deliminaterChars);
            List<string> invoicePDFStringList = invoicePDFStringArray.ToList();
            invoicePDFStringList.RemoveAll(x => string.IsNullOrEmpty(x));
            invoicePDFStringArray = invoicePDFStringList.ToArray();

            return invoicePDFStringArray;
        }
        private XImage GetQrImage()
        {
            var imageBytes = Convert.FromBase64String(qrCode);
            return XImage.FromStream(new MemoryStream(imageBytes));
        }
        private XFont RegisterFont(ILogger log)
        {
            string fontName = "Verdana";
            try
            {
                EZFontResolver fontResolver = EZFontResolver.Get;
                GlobalFontSettings.FontResolver = fontResolver;
                fontResolver.AddFont(fontName, XFontStyle.Regular, fontResolver.LoadFontData("TaxServices.Verdana.ttf"), true, true);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Font Exists"))
                {
                    log.LogInformation(e.Message);
                }
                else
                {
                    throw new Exception("Font Exception");
                }
            }

            return  new XFont(fontName, 10, XFontStyle.Regular);
        }
        private void DrawPdf(string invoiceStringPreQRCode,string invoiceStringPostQRCode,XFont font,XImage qRImage, PdfPage pdfPage)
        {
            try
            {
                XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect1 = new XRect(50, 20, pdfPage.Width, 400);

                tf.DrawString(invoiceStringPreQRCode, font, XBrushes.Black, rect1, XStringFormats.TopLeft);
                gfx.DrawImage(qRImage, new XRect(50, 335, 240, 240));
                XRect rect2 = new XRect(50, 580, pdfPage.Width, 400);
                tf.DrawString(invoiceStringPostQRCode, font, XBrushes.Black, rect2, XStringFormats.TopLeft);
            }
            catch (Exception e)
            {
                throw new Exception("Image could not be drawn: " + e);
            }

        }
    }
}
