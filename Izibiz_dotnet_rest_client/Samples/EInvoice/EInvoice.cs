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

namespace Samples.EInvoice
{
   // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EInvoice
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       Dictionary<string, EInvoiceResponse> dictionary = new Dictionary<string, EInvoiceResponse>();

        [Test,Order(1)]//Belirli bir tarih aralığındaki faturaları getirme
        public void EInvoiceList()
        {
            var response = _izibizClient.EInvoice().EInvoiceList(Authentication.Token);
            dictionary.Add("EInvoiceList", response);
            foreach (var res in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : "+ res.uuid + "Fatura ID : "+res.documentNo);
            }

            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

        [Test, Order(2)]//Onay Süresi Geçmiş Faturalar
        public void EInvoiceApprovalExpired()
        {
            var response = _izibizClient.EInvoice().ApprovalExpiredEInvoice(Authentication.Token);
            dictionary.Add("EInvoiceApprovalExpired", response);

        }

        [Test,Order(2)]//Cevap Teslim Edilemeyen Faturalar
        public void EInvoiceUndeliverableAnswer()
        {
            var response = _izibizClient.EInvoice().UndeliverableAnswerEInvoiceList(Authentication.Token);
            dictionary.Add("EInvoiceUndeliverableAnswer", response);

        }

        [Test, Order(2)]//Reddedilen Faturalar
        public void EInvoiceRejected()
        {
            var response = _izibizClient.EInvoice().RejectedEInvoiceList(Authentication.Token);
            dictionary.Add("EInvoiceRejected", response);
        }

        [Test, Order(2)]//Gelen Faturaların html 
        public void EInvoiceInboxHtml()
        {
            var response = _izibizClient.EInvoice().InboxViewEInvoice(Authentication.Token,nameof(EI.DocumentType.HTML));
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(nameof(EI.Type.EInvoice), nameof(EI.DocumentType.HTML), response);
        }
        [Test, Order(3)]//Gelen Faturaların Ubl 
        public void EInvoiceInboxUBL()
        {
            var response = _izibizClient.EInvoice().InboxViewEInvoice(Authentication.Token, nameof(EI.DocumentType.XML));
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(nameof(EI.Type.EInvoice), nameof(EI.DocumentType.XML), response);
        }
        [Test, Order(4)]//Gelen Faturaların PDF 
        public void EInvoiceInboxPDF()
        {
            var response = _izibizClient.EInvoice().InboxViewEInvoice(Authentication.Token, nameof(EI.DocumentType.PDF));
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(nameof(EI.Type.EInvoice), nameof(EI.DocumentType.PDF), response);
        }

        [Test, Order(5)]//Gelen Fatura durumları
        public void EInvoiceInboxStatus()
        {
            var response = _izibizClient.EInvoice().EInvoiceStatus(Authentication.Token,nameof(EI.Status.Inbox));
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("Gelen Fatura Durumları : " + response.data);
        }
        
    }
}
