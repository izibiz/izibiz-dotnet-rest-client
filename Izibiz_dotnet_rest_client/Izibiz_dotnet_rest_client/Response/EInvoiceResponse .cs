using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Response
{
    public class EInvoiceResponse
    {
        public EInvoiceContents[] contents { get; set; }
        public object pageable { get; set; }
    }

        public class EInvoiceContents
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
            public string invoiceType { get; set; }
            public string statusDesc { get; set; }
            public string note { get; set; }
            public string responseDescription { get; set; }
            public string orderReference { get; set; }
            public string orderReferenceDate { get; set; }
            public object envelope { get; set; }
            public string gtbRegistrationNo { get; set; }
            public string gtbExportDate { get; set; }
            public string gtbRefNo { get; set; }
            public int statusCode { get; set; }
            public string statusCodeDesc { get; set; }
            public string supplierName { get; set; }
            public string customerName { get; set; }

       }
}
