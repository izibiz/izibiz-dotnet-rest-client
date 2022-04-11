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

namespace Samples.EDespatch
{
   // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class EDespatch
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       

        [Test,Order(1)]
        public void GetEDespatchList()
        {
            var response = _izibizClient.EDespatch().EDespatchList(Authentication.Token);
            Assert.NotNull(response);
            foreach (var req in response.contents)
            {
                System.Diagnostics.Debug.WriteLine("Irsaliye uuid : " + req.uuid + "Irsaliye ID : " + req.documentNo);
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }


        [Test, Order(2)]
        public void GetEDespatchListStatus()
        {
            var response = _izibizClient.EDespatch().EDespatchStatusInquery(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine(" Gelen E-İrsaliye Durum Sorgulama : "+ response.data);
        }

        [Test, Order(3)]
        public void GetEDespatchStatus()
        {
            var response = _izibizClient.EDespatch().EDespatchStatus(Authentication.Token,EI.Status.Inbox);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("E-İrsaliye Durumları : " + response.data);
        }

        [Test, Order(4)]//Giden İrsaliyelerin html 
        public void GetEDespatchOutboxHtml()
        {
            var response = _izibizClient.EDespatch().GetEDespatchDocument(Authentication.Token,EI.DocumentType.HTML);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatch, EI.DocumentType.HTML, response);
        }


        [Test, Order(5)]//Giden İrsaliyelerin xml 
        public void GetEDespatchOutboxUBL()
        {
            var response = _izibizClient.EDespatch().GetEDespatchDocument(Authentication.Token, EI.DocumentType.UBL);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatch, EI.DocumentType.XML, response);
        }

        [Test, Order(6)]//Giden İrsaliyelerin pdf
        public void GetEDespatchOutboxPDF()
        {
            var response = _izibizClient.EDespatch().GetEDespatchDocument(Authentication.Token, EI.DocumentType.PDF);
            Assert.NotNull(response);
            FolderOperations.SaveToDisk(EI.Type.EDespatch, EI.DocumentType.PDF, response);
        }

     
    }
}
