using TaxModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class CreateErrorResponseService
    {
        public ErrorResponseModel Create(Exception e, JsonModel jsonModel)
        {
            ErrorResponseModel errorResponseModel = new ErrorResponseModel();
            errorResponseModel.ErrorMessage = e.Message;
            errorResponseModel.invoiceNumber = jsonModel.InvoiceNumber;
            errorResponseModel.jsonModel = jsonModel;
            return errorResponseModel;
        }
    }
}
