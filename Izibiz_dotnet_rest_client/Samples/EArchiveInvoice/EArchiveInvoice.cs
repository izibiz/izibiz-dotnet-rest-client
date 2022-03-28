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
            var request = _izibizClient.EArchiveInvoice().list(Authentication.Token);
            Assert.NotNull(request);
        }

        [Test, Order(2)]
        public void EArchiveInvoice_Html()
        {
            var request = _izibizClient.EArchiveInvoice().EArchiveInvoiceHtml(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EArchive),nameof(EI.DocumentType.HTML),request);
        }

        
        [Test, Order(3)]
        public void EArchiveInvoiceInquiry_PDF()
        {
            var request = _izibizClient.EArchiveInvoice().EArchiveInvoicePDF(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EArchive), nameof(EI.DocumentType.PDF), request);
        }

        [Test, Order(4)]
        public void EArchiveInvoiceStatus()
        {
            var request = _izibizClient.EArchiveInvoice().EArchiveStatus(Authentication.Token);
            Assert.NotNull(request);
        }

    }
}
