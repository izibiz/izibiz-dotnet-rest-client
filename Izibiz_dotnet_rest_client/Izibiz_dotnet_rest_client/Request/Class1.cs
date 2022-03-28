//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Izibiz.Request
//{

//    public class Rootobject
//    {
//        public string documentAction { get; set; }
//        public Content content { get; set; }
//    }

//    public class Content
//    {
//        public string profile { get; set; }
//        public string documentTypeCode { get; set; }
//        public string documentNo { get; set; }
//        public string uuid { get; set; }
//        public string issueDate { get; set; }
//        public string issueTime { get; set; }
//        public object[] notes { get; set; }
//        public string currencyCode { get; set; }
//        public Additionalreference[] additionalReferences { get; set; }
//        public Supplierparty supplierParty { get; set; }
//        public Customerparty customerParty { get; set; }
//        //public Paymentmean[] paymentMeans { get; set; }
//       // public Pricingexchangerate pricingExchangeRate { get; set; }
//      //  public Paymentexchangerate paymentExchangeRate { get; set; }
//        public Taxtotal taxTotal { get; set; }
//        public Legalmonetarytotal legalMonetaryTotal { get; set; }
//        public object[] lines { get; set; }
//    }

//    public class Supplierparty
//    {
//        public string schemeId { get; set; }
//        public string identifier { get; set; }
//        public string name { get; set; }
//        public string centralRegistrationNo { get; set; }
//        public string taxOffice { get; set; }
//        public string taxRegisterNo { get; set; }
//        public string headOffice { get; set; }
//        public Address address { get; set; }
//    }

//    public class Address
//    {
//        public string buildingName { get; set; }
//        public string buildingNumber { get; set; }
//        public string streetName { get; set; }
//        public string postalCode { get; set; }
//        public string subCity { get; set; }
//        public string city { get; set; }
//        public string country { get; set; }
//        public string email { get; set; }
//        public string telephone { get; set; }
//        public string website { get; set; }
//    }

//    public class Customerparty
//    {
//        public string schemeId { get; set; }
//        public string identifier { get; set; }
//        public string customerType { get; set; }
//        public string firstName { get; set; }
//        public string lastName { get; set; }
//        public string taxOffice { get; set; }
//        public Address1 address { get; set; }
//    }

//    public class Address1
//    {
//        public string buildingName { get; set; }
//        public string buildingNumber { get; set; }
//        public string streetName { get; set; }
//        public string postalCode { get; set; }
//        public string subCity { get; set; }
//        public string city { get; set; }
//        public string country { get; set; }
//        public string email { get; set; }
//        public string telephone { get; set; }
//        public string website { get; set; }
//        public string nationalityId { get; set; }
//    }

//    public class Pricingexchangerate
//    {
//        public float calculationRate { get; set; }
//        public string sourceCurrencyCode { get; set; }
//        public string targetCurrencyCode { get; set; }
//    }

//    public class Paymentexchangerate
//    {
//        public float calculationRate { get; set; }
//        public string sourceCurrencyCode { get; set; }
//        public string targetCurrencyCode { get; set; }
//    }

//    public class Taxtotal
//    {
//        public float taxAmount { get; set; }
//        public Taxsubtotal[] taxSubTotal { get; set; }
//    }

//    public class Taxsubtotal
//    {
//        public int calculationSequenceNumeric { get; set; }
//        public float taxableAmount { get; set; }
//        public float percent { get; set; }
//        public float taxAmount { get; set; }
//        public Taxscheme taxScheme { get; set; }
//    }

//    public class Taxscheme
//    {
//        public string name { get; set; }
//        public string typeCode { get; set; }
//    }

//    public class Legalmonetarytotal
//    {
//        public float lineExtensionAmount { get; set; }
//        public float taxExclusiveAmount { get; set; }
//        public float taxInclusiveAmount { get; set; }
//        public float payableAmount { get; set; }
//    }

//    public class Additionalreference
//    {
//        public string documentType { get; set; }
//        public string id { get; set; }
//        public string issueDate { get; set; }
//        public Attachment attachment { get; set; }
//        public string documentTypeCode { get; set; }
//        public Issuerparty issuerParty { get; set; }
//    }

//    public class Attachment
//    {
//        public string characterSetCode { get; set; }
//        public string encodingCode { get; set; }
//        public string filename { get; set; }
//        public string mimeCode { get; set; }
//        public string content { get; set; }
//    }

//    public class Issuerparty
//    {
//        public Postaladdress postalAddress { get; set; }
//    }

//    public class Postaladdress
//    {
//        public Country country { get; set; }
//    }

//    public class Country
//    {
//        public string identificationCode { get; set; }
//        public string name { get; set; }
//    }

//    public class Paymentmean
//    {
//        public string paymentCodeListId { get; set; }
//        public string paymentCode { get; set; }
//        public string instructionNote { get; set; }
//        public Payeefinancialaccount payeeFinancialAccount { get; set; }
//        public Payerfinancialaccount payerFinancialAccount { get; set; }
//    }

//    public class Payeefinancialaccount
//    {
//        public Financialinstitutionbranch financialInstitutionBranch { get; set; }
//    }

//    public class Financialinstitutionbranch
//    {
//        public Financialinstitution financialInstitution { get; set; }
//    }

//    public class Financialinstitution
//    {
//        public string id { get; set; }
//    }

//    public class Payerfinancialaccount
//    {
//        public Financialinstitutionbranch1 financialInstitutionBranch { get; set; }
//    }

//    public class Financialinstitutionbranch1
//    {
//        public Financialinstitution1 financialInstitution { get; set; }
//    }

//    public class Financialinstitution1
//    {
//        public string id { get; set; }
//    }

//}
