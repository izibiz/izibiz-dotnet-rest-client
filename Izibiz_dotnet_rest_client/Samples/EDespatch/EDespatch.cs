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

namespace Samples.EDespatch
{
   // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EDespatch
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       

        [Test,Order(1)]
        public void EDespatchList()
        {
            var request = _izibizClient.EDespatch().EDespatchList(Authentication.Token);
            Assert.NotNull(request);
            foreach (var req in request.contents)
            {
                System.Diagnostics.Debug.WriteLine("Fatura uuid : " + req.uuid + "Fatura ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }


        [Test, Order(2)]
        public void EDespatchListStatus()
        {
            var request = _izibizClient.EDespatch().EDespatchStatusInquery(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Durum Sorgulama : "+ request.data);
        }

        [Test, Order(3)]
        public void EDespatchStatus()
        {
            var request = _izibizClient.EDespatch().EDespatchStatus(Authentication.Token,nameof(EI.Status.Inbox));
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("E-İrsaliye Durumları : " + request.data);
        }

        [Test, Order(4)]//Giden İrsaliyelerin html 
        public void EDespatchOutboxHtml()
        {
            var request = _izibizClient.EDespatch().OutboxHtmlEDespatch(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatch), nameof(EI.DocumentType.HTML), request);
        }


        [Test, Order(5)]//Giden İrsaliyelerin xml 
        public void EDespatchOutboxUBL()
        {
            var request = _izibizClient.EDespatch().OutboxEDespatchUbl(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatch), nameof(EI.DocumentType.XML), request);
        }

        [Test, Order(6)]//Giden İrsaliyelerin pdf
        public void EDespatchOutboxPDF()
        {
            var request = _izibizClient.EDespatch().PdfOutboxEDespatch(Authentication.Token);
            Assert.NotNull(request);
            FolderOperations.SaveToDisk(nameof(EI.Type.EDespatch), nameof(EI.DocumentType.PDF), request);
        }

     
    }
}
