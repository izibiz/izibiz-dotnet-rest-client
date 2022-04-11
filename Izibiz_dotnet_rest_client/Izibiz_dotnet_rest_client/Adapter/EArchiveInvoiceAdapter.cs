using Izibiz.Operations;
using Izibiz.Request;
using Izibiz.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Izibiz.Adapter
{
    public class EArchiveInvoiceAdapter
    {
        EArchiveInvoiceResponse eArchiveInvoiceResponse;
        BaseAdapter baseAdapter = new BaseAdapter();
        Dictionary<string, byte[]> htmlImage = new Dictionary<string, byte[]>();
        Dictionary<string, byte[]> pdfImage = new Dictionary<string, byte[]>();

        public EArchiveInvoiceResponse list(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/earchives";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eArchiveInvoiceResponse = FolderOperations.BaseDeserialize<EArchiveInvoiceResponse>(responseData);
            return eArchiveInvoiceResponse;
        }


        public Dictionary<string, byte[]> EArchiveInvoiceHtml(string token)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            foreach (var invArchive in eArchiveInvoiceResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/earchives/" + invArchive.id + "/html";
                      var eArchiveInterrogation = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(eArchiveInterrogation);
                    htmlImage.Add(invArchive.documentNo, bytes);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }

            }


            return htmlImage;
        }

        public Dictionary<string, byte[]> EArchiveInvoicePDF(string token)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            foreach (var ınvArchive in eArchiveInvoiceResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/earchives/" + ınvArchive.id + "/pdf";
                    var eArchiveInterrogation = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(eArchiveInterrogation);
                    pdfImage.Add(ınvArchive.documentNo, bytes);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }
            }
            return pdfImage;
        }


        public BaseResponse<object> EArchiveStatus(string token)
        {   
            var deserializerData = baseAdapter.Status(token, EI.Type.EArchive);
            return deserializerData;
        }

    }
}
