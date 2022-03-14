using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Operations
{
    public partial class EI
    {
        public enum DocumentType
        {
            PDF,
            XML,
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
           ECurrency
        }

        public enum Status
        {
            Inbox,
            Outbox,
            Draft
        }


    }
}
