using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ParsedRequestModel
    {
        public FunctionResponseModel functionResponseModel = new FunctionResponseModel();

        public string invoiceDetails { get; set; }
        public string qrCode { get; set; }

    }
}
