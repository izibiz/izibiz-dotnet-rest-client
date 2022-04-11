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
  // [Ignore("Waiting for Joe to fix his bugs", Until = "2022-07-31 12:00:00Z")]
    public class ESmm
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();

        [Test, Order(1)]
        public void GetESmmList()
        {
            var response = _izibizClient.ESmm().List(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("E-Smm Listesi : ");
            foreach (var esmm in response.contents)
            {
                    System.Diagnostics.Debug.WriteLine("E-Smm uuid : " + esmm.uuid + "E-Smm ID : " + esmm.documentNo); 
            }
            System.Diagnostics.Debug.WriteLine(response.pageable);
        }


        [Test, Order(2)]
        public void GetESmmInquiry_HTML()
        {
            var response = _izibizClient.ESmm().GetESmmDocument(Authentication.Token,EI.DocumentType.HTML);
            Assert.NotNull(response);
            Assert.IsTrue(response.Count() > 0);
            FolderOperations.SaveToDisk(EI.Type.ESmm, EI.DocumentType.HTML, response);
        }

        [Test, Order(3)]
        public void GetESmmInquiry_UBL()
        {
            var response = _izibizClient.ESmm().GetESmmDocument(Authentication.Token,EI.DocumentType.UBL);
            Assert.NotNull(response);
            Assert.IsTrue(response.Count() > 0);
            FolderOperations.SaveToDisk(EI.Type.ESmm, EI.DocumentType.XML, response);
        }

        [Test, Order(4)]
        public void GetESmmStatus()
        {
            var response = _izibizClient.ESmm().GetESmmStatus(Authentication.Token);
            Assert.NotNull(response);
            System.Diagnostics.Debug.WriteLine("E-Smm Durumları : " + response.data);

        }


    }
}
