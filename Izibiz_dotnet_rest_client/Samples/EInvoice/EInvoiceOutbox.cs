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
  //  [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EInvoiceOutbox
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       Dictionary<string, EInvoiceResponse> dictionary = new Dictionary<string, EInvoiceResponse>();

        [Test,Order(1)]//Belirli bir tarih aralığındaki faturaları getirme
        public void GetEInvoicePendingApprovalList_Outbox()
        {
            var response = _izibizClient.EInvoice().PendingApprovalEInvoice_Outbox(Authentication.Token);
            dictionary.Add("PendingApprovalEInvoice", response);
            Assert.NotNull(response.contents);
            System.Diagnostics.Debug.WriteLine("Onay Bekleyen E-Fatura Listesi");
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : "+ req.uuid + "Fatura ID : "+req.documentNo);
            }

            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

        [Test,Order(2)]//Cevap Teslim Edilemeyen Faturalar
        public void GetEInvoiceUndeliverableList_Outbox()
        {
            var response = _izibizClient.EInvoice().UndeliverableEInvoiceList_Outbox(Authentication.Token);
            dictionary.Add("EInvoiceUndeliverableList_Outbox", response);
            Assert.NotNull(response.contents);
            System.Diagnostics.Debug.WriteLine("Cevap Teslim Edilemeyen Fatura Listesi");
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }

            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

        [Test, Order(3)]//Reddedilen Faturalar 
        public void GetEInvoiceRejected_Outbox()
        {
            var response = _izibizClient.EInvoice().RejectedEInvoiceList(Authentication.Token, EI.Status.Outbox);
            dictionary.Add("EInvoiceRejected_Outbox", response);
            Assert.NotNull(response.contents);
            System.Diagnostics.Debug.WriteLine("Reddedilen Fatura Listesi");
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }

        [Test, Order(4)]//Giden Faturalar Ubl 
        public void GetEInvoiceOutbox_Ubl()
        {
            var response = _izibizClient.EInvoice().GetEInvoiceDocument_Outbox(Authentication.Token,EI.DocumentType.UBL);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EInvoice, EI.DocumentType.XML, response);
        }


        [Test, Order(5)]//Giden Faturalar PDF
        public void GetEInvoiceOutbox_PDF()
        {
            var response = _izibizClient.EInvoice().GetEInvoiceDocument_Outbox(Authentication.Token, EI.DocumentType.PDF);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EInvoice, EI.DocumentType.PDF, response);
        }


        [Test, Order(6)]//Giden Fatura durumları
        public void GetEInvoiceOutboxStatus()
        {
            var response = _izibizClient.EInvoice().EInvoiceStatus(Authentication.Token, EI.Status.Outbox);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine(response);
        }

    }
}
