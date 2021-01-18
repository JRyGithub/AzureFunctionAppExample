using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ErrorResponseModel
    {
        public int Status = 500;
        public string invoiceNumber { get; set; }
        public string ErrorMessage { get; set; }
        public JsonModel jsonModel { get; set; }
    }
}
