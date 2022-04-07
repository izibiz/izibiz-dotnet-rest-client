using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Response
{
    public class EExchangeResponse
    {
        public EExchange[] contents { get; set; }
        public object pageable { get; set; }
    }

    public class EExchange
    {
        public int id { get; set; }
        public string documentType { get; set; }
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
        public bool erpReadFlag { get; set; }
        public object accountingSupplier { get; set; }
        public object accountingCustomer { get; set; }
        public object supplierAlias { get; set; }
        public object customerAlias { get; set; }
        public string supplierSSN { get; set; }
        public string customerSSN { get; set; }
        public object type { get; set; }
        public object rootXmlType { get; set; }
        public string xmlType { get; set; }
        public object documentRefId { get; set; }
        public object sendingType { get; set; }
        public string supplierName { get; set; }
        public string customerName { get; set; }

    }
}
