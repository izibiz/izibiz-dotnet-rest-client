using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Request
{
    public class ETicketRequest
    {
        public string documentAction { get; set; }
        public TicketContent content { get; set; }
    }

    public class TicketContent:ContentT
    {
        public string[] notes { get; set; }
        public Additionalreference1[] additionalreference1 { get; set; }
        public Sellerparty sellerParty { get; set; }
        public Line[] lines { get; set; }
    }

    public class Sellerparty
    {
        public Identification[] identifications { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Address2 address { get; set; }
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

    public class Additionalreference1: Additionalreference
    {
        public string documentDescription { get; set; }
        public string scheme { get; set; }
        public Validityperiod validityPeriod { get; set; }
     
    }


}
