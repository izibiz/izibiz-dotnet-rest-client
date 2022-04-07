using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Operations
{
    public partial class EI
    {
        public enum DocumentType
        {
            PDF,
            XML,
            UBL,
            HTML,
            XSLT,
            NULL
        }

        public enum Type
        {
           EInvoice,
           EArchive,
           EDespatch,
           EDespatchReceipt,
           CreditNote,
           ESmm,
           EExchange,
           ECheck
        }

        public enum Status
        {
            Inbox,
            Outbox,
            Draft
        }


    }
}
