using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class JsonModel
    {
        public string DateAndTimeOfIssue { get; set; }
        public string IT { get; set; }
        public string TT { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string Cashier { get; set; }
        public string BD { get; set; }
        public string BuyerCostCenterId { get; set; }
        public string InvoiceNumber { get; set; }
        public string ReferentDocumentNumber { get; set; }
        public string PAC { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public Options Options { get; set; }
        public string Hash { get; set; }
    }

    public class RecordObject
    {
        public string PdfName { get; set; }
        public JsonModel Model { get; set; }
    }

    [Serializable]
    public class Item
    {
        public string GTIN { get; set; }
        public string Name { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<string> Labels { get; set; }
    }

    [Serializable]
    public class Options
    {
        public string OmitQRCodeGen { get; set; }
        public string OmitTextualRepresentation { get; set; }
    }

    public enum InvoiceType
    {
        Normal,
        ProForma,
        Copy,
        Training
    }

    public enum TransactionType
    {
        Sale = 1,
        Refund = 2
    }

    public enum PaymentType
    {
        Other,
        Cash,
        Card,
        Check,
        WireTransfer,
        Voucher,
        MobileMoney
    }
}

