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
 //   [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EDespatchReceipt
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();


        [Test, Order(1)]
        public void EDespatchReceiptList()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptList(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("Gelen E-İrsaliye Yanıt Listesi ");
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }



        [Test, Order(2)]
        public void EDespatchReceiptStatus()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptStatusInquery(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Yanıt Durumu Sorgulama : " + response.data);
        }


        [Test, Order(3)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptUBL()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token,EI.DocumentType.XML);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatchReceipt, EI.DocumentType.XML, response);
        }

        [Test, Order(4)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptHtml()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token, EI.DocumentType.HTML);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatchReceipt, EI.DocumentType.HTML, response);
        }


        [Test, Order(5)]//Giden İrsaliyelerin xml 
        public void EDespatchReceiptPdf()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptDocument(Authentication.Token, EI.DocumentType.PDF);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatchReceipt, EI.DocumentType.PDF, response);
        }

        [Test, Order(6)]
        public void EArchiveInvoiceStatus()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptStatus(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Yanıt Durumları : " + response.data);
        }


        //Outbox

        [Test, Order(1)]
        public void EDespatchReceiptList_Outbox()
        {
            var response = _izibizClient.EDespatchReceipt().EDespatchReceiptList_Outbox(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("Giden E-İrsaliye Yanıt Listesi ");
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

    }
}
