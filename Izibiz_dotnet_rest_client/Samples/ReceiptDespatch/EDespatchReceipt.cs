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

namespace Samples.ReceiptDespatch
{
    //[Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EDespatchReceipt
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();


        [Test, Order(1)]
        public void EDespatchReceiptList()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptList(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("Gelen E-İrsaliye Yanıt Listesi ");
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }



        [Test, Order(2)]
        public void EDespatchReceiptStatus()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptStatusInquery(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Yanıt Durumu Sorgulama : " + request.data);
        }


        [Test, Order(3)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptUBL()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token,"XML");
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatchReceipt), nameof(EI.DocumentType.XML), request);
        }

        [Test, Order(4)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptHtml()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token, "HTML");
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatchReceipt), nameof(EI.DocumentType.HTML), request);
        }


        [Test, Order(5)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptPdf()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token, "PDF");
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatchReceipt), nameof(EI.DocumentType.PDF), request);
        }

        [Test, Order(6)]
        public void EArchiveInvoiceStatus()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptStatus(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Yanıt Durumları : " + request.data);
        }


        //Outbox

        [Test, Order(1)]
        public void EDespatchReceiptList_Outbox()
        {
            var request = _izibizClient.EDespatchReceipt().EDespatchReceiptList_Outbox(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("Giden E-İrsaliye Yanıt Listesi ");
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }

    }
}
