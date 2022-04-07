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
        public void GetCreditNoteList()
        {
            var response = _izibizClient.CreditNote().List(Authentication.Token);
            Assert.NotNull(response);
            foreach (var creditNote in response.contents)
            {
                    System.Diagnostics.Debug.WriteLine("Mustahsil uuid : " + creditNote.uuid + "Mustahsil ID : " + creditNote.documentNo); 
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

        [Test, Order(2)]
        public void CreditNoteInquiry_PDF()
        {
            var response = _izibizClient.CreditNote().GetCreditNoteDocument(Authentication.Token,EI.DocumentType.PDF);
            Assert.NotNull(response);
            Assert.IsTrue(response.Count() > 0);
            FolderOperations.SaveToDisk(EI.Type.CreditNote,EI.DocumentType.PDF, response);
        }

        [Test, Order(3)]
        public void CreditNoteInquiry_HTML()
        {
            var response = _izibizClient.CreditNote().GetCreditNoteDocument(Authentication.Token,EI.DocumentType.HTML);
            Assert.NotNull(response);
            Assert.IsTrue(response.Count() > 0);
            FolderOperations.SaveToDisk(EI.Type.CreditNote, EI.DocumentType.HTML, response);
        }

        [Test, Order(4)]
        public void CreditNoteInquiry_XML()
        {
            var response = _izibizClient.CreditNote().GetCreditNoteDocument(Authentication.Token,EI.DocumentType.XML);
            Assert.NotNull(response);
            Assert.IsTrue(response.Count() > 0);
            FolderOperations.SaveToDisk(EI.Type.CreditNote, EI.DocumentType.XML, response);
        }

        [Test, Order(5)]
        public void EArchiveInvoiceStatus()
        {
            var response = _izibizClient.CreditNote().GetCreditNoteStatus(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("Müstahsil Durumları : " + response.data);

        }


    }
}
