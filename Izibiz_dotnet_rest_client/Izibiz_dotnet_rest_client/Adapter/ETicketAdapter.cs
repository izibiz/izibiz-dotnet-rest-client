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
    public class ETicketAdapter
    {
       
        BaseAdapter baseAdapter = new BaseAdapter();
        ETicketResponse eTicketResponse;
        ETicketResponse eTicketResponseList;
        Dictionary<string, byte[]> dicTicketList = new Dictionary<string, byte[]>();

        public ETicketAdapter()
        {

        }

        public string ETicketDelete(string token)
        {
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox?dateType=DELIVERY&status=New&startDate=2021-10-13&endDate=2021-10-28&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            string responseData = null;
            try
            {
                string url = BaseAdapter.BaseUrl + "/v1/echecks/" + eTicketResponseList.contents[0].id;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";

                httpRequest.Accept = "*/*";
                httpRequest.Headers["Authorization"] = "Bearer " + token;
                HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                responseData = readStream.ReadToEnd();

             //   var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return responseData;
        }

        public ETicketResponse ETicketList(string token)
        {
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox?dateType=DELIVERY&status=New&startDate=2021-10-13&endDate=2021-10-28&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            string url = BaseAdapter.BaseUrl + "/v1/echecks";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<ETicketResponse>>(responseData);
            eTicketResponse = deserializerData.data;
            eTicketResponseList = eTicketResponse;
            return eTicketResponse;
        }


        public Dictionary<string, byte[]> ETicketDocument(string token, string documentType)
        {
            dicTicketList.Clear();
            foreach (var eTicket in eTicketResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType == nameof(EI.DocumentType.XML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/echecks/" + eTicket.id + "/preview/ubl";
                    }
                    else if (documentType == nameof(EI.DocumentType.HTML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/echecks/" + eTicket.id + "/preview/html";
                    }
                    else if (documentType == nameof(EI.DocumentType.PDF))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/echecks/" + eTicket.id + "/preview/pdf";
                    }
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicTicketList.Add(eTicket.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eTicket.documentNo + "mevcut değildir.");
                }
            }
            return dicTicketList;
        }


        public BaseResponse<object> ETicketInformation(ETicketRequest request, string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/echecks";
            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }

        public BaseResponse<object> ETicketStatus(string token)
        {
            var deserializerData = baseAdapter.Status(token, nameof(EI.Type.ETicket));
            return deserializerData;
        }

    }
}
