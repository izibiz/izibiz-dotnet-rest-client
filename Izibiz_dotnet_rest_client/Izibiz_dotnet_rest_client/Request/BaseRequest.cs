using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Request
{

    public class BaseRequest
    {
          public string documentAction { get; set; }
        public Content content { get; set; }
    }

    public class Content
    {
        public string profile { get; set; }
        public string documentTypeCode { get; set; }
        public string documentNo { get; set; }
        public string uuid { get; set; }
        public string issueDate { get; set; }
        public string issueTime { get; set; }
        public string currencyCode { get; set; }
        public Line[] lines { get; set; }
        public Additionalreference[] additionalReferences2 { get; set; }
        public Party supplierParty { get; set; }
        public Party customerParty { get; set; }
        public Taxtotal taxTotal { get; set; }
        public Legalmonetarytotal legalMonetaryTotal { get; set; }
        public string[] notes { get; set; }
    }

    public class Line
    {
        public int id { get; set; }
        public int quantity { get; set; }
        public string unitCode { get; set; }
        public int lineExtensionAmount { get; set; }
        public Taxtotal1 taxTotal { get; set; }
        public string itemName { get; set; }
        public int itemPrice { get; set; }
    }

    public class Party
    {
        public string schemeId { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public Identification[] identifications { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string centralRegistrationNo { get; set; }
        public string taxOffice { get; set; }
        public string taxRegisterNo { get; set; }
        public string headOffice { get; set; }
        public Address address { get; set; }
        public string customerType { get; set; }

    }

    public class Address
    {
        public string buildingName { get; set; }
        public string buildingNumber { get; set; }
        public string streetName { get; set; }
        public string postalCode { get; set; }
        public string subCity { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string website { get; set; }
        public string nationalityId { get; set; }
        }

 



    public class Identification
    {
        public string scheme { get; set; }
        public string value { get; set; }
    }

    public class Taxtotal
    {
        public float taxAmount { get; set; }
        public Taxsubtotal[] taxSubTotal { get; set; }
    }

    public class Taxsubtotal
    {
        public int calculationSequenceNumeric { get; set; }
        public float taxableAmount { get; set; }
        public int percent { get; set; }
        public float taxAmount { get; set; }
        public Taxscheme taxScheme { get; set; }
    }

    public class Taxscheme
    {
        public string name { get; set; }
        public string typeCode { get; set; }
    }

    public class Legalmonetarytotal
    {
        public float lineExtensionAmount { get; set; }
        public float taxExclusiveAmount { get; set; }
        public float taxInclusiveAmount { get; set; }
        public float payableAmount { get; set; }
        public float allowanceTotalAmount { get; set; }
    }

    public class Additionalreference
    {
        public  string documentType { get; set; }
        public  string id { get; set; }
        public  string issueDate { get; set; }
        public  Attachment attachment { get; set; }
        public  string documentTypeCode { get;set; }

    }

    public class Attachment
    {
        public string characterSetCode { get; set; }
        public string encodingCode { get; set; }
        public string filename { get; set; }
        public string mimeCode { get; set; }
        public string content { get; set; }
    }

    public class Validityperiod
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }

    public class Taxtotal1
    {
        public float taxAmount { get; set; }
        public Taxsubtotal1[] taxSubTotal { get; set; }
    }

    public class Taxsubtotal1
    {
        public int taxableAmount { get; set; }
        public float taxAmount { get; set; }
        public int calculationSequenceNumeric { get; set; }
        public int percent { get; set; }
        public Taxscheme1 taxScheme { get; set; }
    }

    public class Taxscheme1
    {
        public string name { get; set; }
        public string typeCode { get; set; }
    }

}
