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

namespace Samples.ESmm
{
   //[Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class ESmm
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();

        [Test, Order(1)]
        public void ESmmList()
        {
            var request = _izibizClient.ESmm().List(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("E-Smm Listesi : ");
            foreach (var esmm in request.contents)
            {
                    System.Diagnostics.Debug.WriteLine("Fatura uuid : " + esmm.uuid + "Fatura ID : " + esmm.documentNo); 
            }
            System.Diagnostics.Debug.WriteLine(request.pageable);
        }


        [Test, Order(2)]
        public void ESmmInquiry_HTML()
        {
            var request = _izibizClient.ESmm().ESmmDocument(Authentication.Token,nameof(EI.DocumentType.HTML));
            Assert.NotNull(request);
            Assert.IsTrue(request.Count() > 0);
            FolderOperations.SaveToDisk(nameof(EI.Type.ESmm), nameof(EI.DocumentType.HTML), request);
        }

        [Test, Order(3)]
        public void ESmmInquiry_XML()
        {
            var request = _izibizClient.ESmm().ESmmDocument(Authentication.Token,nameof(EI.DocumentType.XML));
            Assert.NotNull(request);
            Assert.IsTrue(request.Count() > 0);
            FolderOperations.SaveToDisk(nameof(EI.Type.ESmm), nameof(EI.DocumentType.XML), request);
        }

        [Test, Order(4)]
        public void ESmmStatus()
        {
            var request = _izibizClient.ESmm().ESmmStatus(Authentication.Token);
            Assert.NotNull(request);
            System.Diagnostics.Debug.WriteLine("E-Smm Durumları : " + request.data);

        }


    }
}
