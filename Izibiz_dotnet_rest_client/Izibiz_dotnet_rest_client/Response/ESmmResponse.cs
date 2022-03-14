using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Response
{
    public class ESmmResponse
    {
        public SmmContents[] contents { get; set; }
        public object pageable { get; set; }
    }

    public class SmmContents
    {
        public int id { get; set; }
        public object documentType { get; set; }
        public string issueDate { get; set; }
        public string issueTime { get; set; }
        public string createDate { get; set; }
        public string uuid { get; set; }
        public string documentNo { get; set; }
        public string currency { get; set; }
        public string direction { get; set; }
        public string readStatus { get; set; }
        public object documentStatus { get; set; }
        public string amount { get; set; }
        public string taxAmount { get; set; }
        public int? lineCount { get; set; }
        public string profile { get; set; }
        public string erpReadFlag { get; set; }
        public object accountingSupplier { get; set; }
        public object accountingCustomer { get; set; }
        public string supplierAlias { get; set; }
        public string customerAlias { get; set; }
        public string supplierSSN { get; set; }
        public string customerSSN { get; set; }
        public string type { get; set; }
        public string smmId { get; set; }
        public object reportId { get; set; }
        public string email { get; set; }
        public string emailStatusDesc { get; set; }
        public string smsStatusDesc { get; set; }
        public object smsNumber { get; set; }
        public string statusDesc { get; set; }
        public int smsStatusCode { get; set; }
        public int emailStatusCode { get; set; }
        public int statusCode { get; set; }
        public string emailStatusCodeDesc { get; set; }
        public string statusCodeDesc { get; set; }
        public string smsStatusCodeDesc { get; set; }
        public string supplierName { get; set; }
        public string customerName { get; set; }

    }
}
