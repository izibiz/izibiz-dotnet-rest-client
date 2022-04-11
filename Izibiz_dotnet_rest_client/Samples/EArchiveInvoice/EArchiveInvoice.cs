using Izibiz;
using Izibiz.Response;
using Izibiz.Adapter;
using Izibiz.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Izibiz.Operations;

namespace Samples.EArchiveInvoice
{
  //[Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EArchiveInvoice
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       

        [Test,Order(1)]
        public void EArchiveInvoiceList()
        {
            var response = _izibizClient.EArchiveInvoice().list(Authentication.Token);
            Assert.NotNull(response);
        }

        [Test, Order(2)]
        public void EArchiveInvoice_Html()
        {
            var response = _izibizClient.EArchiveInvoice().EArchiveInvoiceHtml(Authentication.Token);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EArchive,EI.DocumentType.HTML, response);
        }

        
        [Test, Order(3)]
        public void EArchiveInvoiceInquiry_PDF()
        {
            var response = _izibizClient.EArchiveInvoice().EArchiveInvoicePDF(Authentication.Token);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EArchive, EI.DocumentType.PDF, response);
        }

        [Test, Order(4)]
        public void EArchiveInvoiceStatus()
        {
            var response = _izibizClient.EArchiveInvoice().EArchiveStatus(Authentication.Token);
            Assert.NotNull(response);
        }

    }
}
