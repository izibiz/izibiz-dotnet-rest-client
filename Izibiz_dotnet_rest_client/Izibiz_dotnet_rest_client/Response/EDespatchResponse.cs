using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Response
{
    public class EDespatchResponse
    {
        public EDespatch[] contents { get; set; }
        public object pageable { get; set; }
    }

    public class EDespatch
    {
        public int id { get; set; }
        public object documentType { get; set; }
        public string issueDate { get; set; }
        public string issueTime { get; set; }
        public string createDate { get; set; }
        public string uuid { get; set; }
        public string documentNo { get; set; }
        public object currency { get; set; }
        public string direction { get; set; }
        public object readStatus { get; set; }
        public object documentStatus { get; set; }
        public string amount { get; set; }
        public string taxAmount { get; set; }
        public int? lineCount { get; set; }
        public string profile { get; set; }
        public bool erpReadFlag { get; set; }
        public object accountingSupplier { get; set; }
        public object accountingCustomer { get; set; }
        public string supplierAlias { get; set; }
        public string customerAlias { get; set; }
        public string supplierSSN { get; set; }
        public string customerSSN { get; set; }
        public string type { get; set; }
        public object orderReferenceId { get; set; }
        public string orderReferenceDate { get; set; }
        public string statusDesc { get; set; }
        public int statusCode { get; set; }
        public string statusCodeDesc { get; set; }
        public string customerName { get; set; }
        public string supplierName { get; set; }
    }
}
