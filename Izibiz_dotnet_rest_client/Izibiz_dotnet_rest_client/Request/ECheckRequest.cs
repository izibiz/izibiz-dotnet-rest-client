using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Request
{
    public class ECheckRequest: BaseRequest
    {
     //   public string documentAction { get; set; }
        public ECheckContent content { get; set; }
    }

    public class ECheckContent:Content
    {
        
        public AdditionalreferenceEcheck[] additionalreference1 { get; set; }
        public Party sellerParty { get; set; }
    }

    

    public class AdditionalreferenceEcheck : Additionalreference
    {
        public string documentDescription { get; set; }
        public string scheme { get; set; }
        public Validityperiod validityPeriod { get; set; }

    }


}
