using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FunctionResponseModel
    {
        [JsonProperty(PropertyName = "PartitionKey")]
        public string PartitionKey { get; set; }
        [JsonProperty(PropertyName = "RowKey")]
        public string RowKey { get; set; } //ReferenceDocument Number
        [JsonProperty(PropertyName = "InvoiceNumber")]
        public string InvoiceNumber { get; set; }
        public int Status = 200;
    }
}
