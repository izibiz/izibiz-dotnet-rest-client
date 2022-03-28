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

namespace Samples.CreditNotes
{
  // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class CreditNote
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();

        [Test, Order(1)]
        public void CreditNoteList()
        {
            var request = _izibizClient.CreditNote().List(Authentication.Token);
            Assert.NotNull(request);
            foreach (var creditNote in request.contents)
            {
                    System.Diagnostics.Debug.WriteLine("Fatura uuid : " + creditNote.uuid + "Fatura ID : " + creditNote.documentNo); 
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }

        [Test, Order(2)]
        public void CreditNoteInquiry_PDF()
        {
            var request = _izibizClient.CreditNote().CreditNoteDocument(Authentication.Token,nameof(EI.DocumentType.PDF));
            Assert.NotNull(request);
            Assert.IsTrue(request.Count() > 0);
            FolderOperations.SaveToDisk(nameof(EI.Type.CreditNote), nameof(EI.DocumentType.PDF), request);
        }

        [Test, Order(3)]
        public void CreditNoteInquiry_HTML()
        {
            var request = _izibizClient.CreditNote().CreditNoteDocument(Authentication.Token,nameof(EI.DocumentType.HTML));
            Assert.NotNull(request);
            Assert.IsTrue(request.Count() > 0);
            FolderOperations.SaveToDisk(nameof(EI.Type.CreditNote), nameof(EI.DocumentType.HTML), request);
        }

        [Test, Order(4)]
        public void CreditNoteInquiry_XML()
        {
            var request = _izibizClient.CreditNote().CreditNoteDocument(Authentication.Token,nameof(EI.DocumentType.XML));
            Assert.NotNull(request);
            Assert.IsTrue(request.Count() > 0);
            FolderOperations.SaveToDisk(nameof(EI.Type.CreditNote), nameof(EI.DocumentType.XML), request);
        }

        [Test, Order(5)]
        public void EArchiveInvoiceStatus()
        {
            var request = _izibizClient.CreditNote().CreditNoteStatus(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("Müstahsil Durumları : " + request.data);

        }


    }
}
