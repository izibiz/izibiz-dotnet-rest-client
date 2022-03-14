using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Response
{
   public class LoggedInUserCompanyResponse
    {
        public string id { get; set; }
        public string commercialName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string vknTckno { get; set; }
        public string website { get; set; }
        public string taxOffice { get; set; }
        public string status { get; set; }
        public string statusDesc { get; set; }
        public string customerType { get; set; }
        public string configType { get; set; }
        public string configTypeDesc { get; set; }
        public string companyType { get; set; }
        public string companyTypeDesc { get; set; }
        public string channelRefId { get; set; }
        public string dealerRefId { get; set; }
        public string accountRefId { get; set; }
        public string accountRefName { get; set; }
        public string channelRefName { get; set; }
        public string dealerRefName { get; set; }
        public string sicilNo { get; set; }
        public string isletmeMerkezi { get; set; }
        public string mersisNo { get; set; }
        public string contractSigned { get; set; }
        public string activationReason { get; set; }
        public string deactivationReason { get; set; }
        public string billingCustomerId { get; set; }
        public string tariffType { get; set; }
        public string[] addressList { get; set; }

    }
}
