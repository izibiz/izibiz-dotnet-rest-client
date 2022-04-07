using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Request
{

    public class EExchangeRequest
    {
        public string documentAction = "SEND";
        public EExchangeContent content { get; set; }
    }

    public class EExchangeContent : Content
    {
        public Additionalreference2[] additionalReferences { get; set; }
        public Paymentmean[] paymentMeans = new Paymentmean[] {
            new Paymentmean {
                paymentCodeListId = "UN/4461" ,
                paymentCode="ZZZ",
                instructionNote="AÇIKLAMA",
                payeeFinancialAccount=new Payeefinancialaccount
                {
                    financialInstitutionBranch =new Financialinstitutionbranch
                {
                    financialInstitution=new Financialinstitution
                    {
                        id="1304-15"
                    }
                },
                },payerFinancialAccount=new Payerfinancialaccount
                {
                    financialInstitutionBranch=new Financialinstitutionbranch1
                    {
                        financialInstitution=new Financialinstitution1
                        {
                            id="B2560",
                        }
                    }
                }
        } };
        public Pricingexchangerate pricingExchangeRate = new Pricingexchangerate();
        public Paymentexchangerate paymentExchangeRate = new Paymentexchangerate();
    }

    public class Additionalreference2 : Additionalreference
    {
        public Issuerparty issuerParty { get; set; }

    }

    public class Issuerparty
    {
        public Postaladdress postalAddress = new Postaladdress();
    }

    public class Postaladdress
    {
        public Country country = new Country();
    }

    public class Country
    {
        public string identificationCode = "BG";
        public string name = "BULGARISTAN";
    }

    public class Pricingexchangerate
    {
        public float calculationRate = 1.1283F;
        public string sourceCurrencyCode = "EUR";
        public string targetCurrencyCode = "USD";
    }

    public class Paymentexchangerate
    {
        public float calculationRate = 11.34f;
        public string sourceCurrencyCode = "USD";
        public string targetCurrencyCode = "TRY";
    }

    public class Paymentmean
    {
        public string paymentCodeListId { get; set; }
        public string paymentCode { get; set; }
        public string instructionNote { get; set; }
        public Payeefinancialaccount payeeFinancialAccount { get; set; }
        public Payerfinancialaccount payerFinancialAccount { get; set; }

    }

    public class Payeefinancialaccount
    {
        public Financialinstitutionbranch financialInstitutionBranch = new Financialinstitutionbranch();
    }

    public class Financialinstitutionbranch
    {
        public Financialinstitution financialInstitution = new Financialinstitution();
    }

    public class Financialinstitution
    {
        public string id { get; set; }
    }

    public class Payerfinancialaccount
    {
        public Financialinstitutionbranch1 financialInstitutionBranch = new Financialinstitutionbranch1();
    }

    public class Financialinstitutionbranch1
    {
        public Financialinstitution1 financialInstitution = new Financialinstitution1();
    }

    public class Financialinstitution1
    {
        public string id { get; set; }
    }

}
