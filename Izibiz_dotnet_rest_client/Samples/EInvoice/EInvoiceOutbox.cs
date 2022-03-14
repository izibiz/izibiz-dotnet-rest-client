using Izibiz_dotnet_rest_client;
using Izibiz_dotnet_rest_client.Response;
using Izibiz_dotnet_rest_client.Adapter;
using Izibiz_dotnet_rest_client.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Izibiz_dotnet_rest_client.Operations;

namespace Samples.EInvoice
{
   // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EInvoiceOutbox
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       Dictionary<string, EInvoiceResponse> dictionary = new Dictionary<string, EInvoiceResponse>();

        [Test,Order(1)]//Belirli bir tarih aralığındaki faturaları getirme
        public void EInvoicePendingApprovalList_Outbox()
        {
            var request = _izibizClient.EInvoice().PendingApprovalEInvoice_Outbox(Authentication.Token);
            dictionary.Add("PendingApprovalEInvoice", request);
            Assert.NotNull(request.contents);
            System.Diagnostics.Debug.WriteLine("Onay Bekleyen E-Fatura Listesi");
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : "+ req.uuid + "Fatura ID : "+req.documentNo);
            }

            System.Diagnostics.Debug.WriteLine(request.pageable);
        }

        [Test,Order(2)]//Cevap Teslim Edilemeyen Faturalar------ DENE
        public void EInvoiceUndeliverableList_Outbox()
        {
            var request = _izibizClient.EInvoice().UndeliverableEInvoiceList_Outbox(Authentication.Token);
            dictionary.Add("EInvoiceUndeliverableList_Outbox", request);
            Assert.NotNull(request.contents);
            System.Diagnostics.Debug.WriteLine("Cevap Teslim Edilemeyen Fatura Listesi");
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }

            System.Diagnostics.Debug.WriteLine(request.pageable);
        }

        [Test, Order(3)]//Reddedilen Faturalar  ---- DENE
        public void EInvoiceRejected_Outbox()
        {
            var request = _izibizClient.EInvoice().RejectedEInvoiceList_Outbox(Authentication.Token);
            dictionary.Add("EInvoiceRejected_Outbox", request);
            Assert.NotNull(request.contents);
            System.Diagnostics.Debug.WriteLine("Reddedilen Fatura Listesi");
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }

        [Test, Order(4)]//Giden Faturalar Ubl 
        public void EInvoiceOutbox_Ubl()
        {
            var request = _izibizClient.EInvoice().EInvoiceUbl_Outbox(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EInvoice), nameof(EI.DocumentType.XML), request);
        }


        [Test, Order(5)]//Giden Faturalar Ubl 
        public void EInvoiceOutbox_PDF()
        {
            var request = _izibizClient.EInvoice().EInvoicePDF_Outbox(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EInvoice), nameof(EI.DocumentType.PDF), request);
        }


        [Test, Order(6)]//Giden Fatura durumları
        public void EInvoiceInboxStatus()
        {
            var request = _izibizClient.EInvoice().EInvoiceStatus(Authentication.Token, nameof(EI.Status.Outbox));
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine(request);
        }

    }
}
