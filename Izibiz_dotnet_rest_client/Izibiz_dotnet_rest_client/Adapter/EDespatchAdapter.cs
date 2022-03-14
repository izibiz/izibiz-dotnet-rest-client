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
    public class EDespatchAdapter
    {
       
        BaseAdapter baseAdapter = new BaseAdapter();
        EDespatchResponse eDespatchResponse;
        EDespatchResponse eDespatchResponseList;
        Dictionary<string, byte[]> dicDespatchList = new Dictionary<string, byte[]>();

        public EDespatchAdapter()
        {

        }

        public EDespatchResponse EDespatchList(string token)
        {
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox?dateType=DELIVERY&status=New&startDate=2021-10-13&endDate=2021-10-28&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox?dateType=DELIVERY&" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EDespatchResponse>>(responseData);
            eDespatchResponse = deserializerData.data;
            eDespatchResponseList = eDespatchResponse;
            return eDespatchResponse;
        }

        public BaseResponse<object> EDespatchStatusInquery(string token)
        {
            var id = eDespatchResponse.contents[0].id;
            string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox/"+id;
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
          //  eDespatchResponse = deserializerData.data;
            return deserializerData;
        }


        public BaseResponse<object> EDespatchStatus(string token,string status)
        {
            BaseResponse<object> deserializerData;
            if (status == "Outbox")
            {
                 deserializerData = baseAdapter.Status(token, nameof(EI.Type.EDespatch), nameof(EI.Status.Outbox));
            }
            else
            {
                deserializerData = baseAdapter.Status(token, nameof(EI.Type.EDespatch), nameof(EI.Status.Inbox));
            }
            return deserializerData;
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox/lookup-statuses";
            //var EDespatchStatus = (string)baseAdapter.HtmlReqRes(token, url);
            //var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(EDespatchStatus);
            //return deserializerData;
        }


        public Dictionary<string, byte[]> OutboxHtmlEDespatch(string token)
        {
            dicDespatchList.Clear();
            foreach (var outboxHtml in eDespatchResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox/" + outboxHtml.id + "/preview/html";
                    var responseData = baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes((string)responseData);
                    dicDespatchList.Add(outboxHtml.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(outboxHtml.documentNo + "mevcut değildir.");
                }
            }
            return dicDespatchList;
        }


        public Dictionary<string, byte[]> OutboxEDespatchUbl(string token)
        {
            dicDespatchList.Clear();
            foreach (var outboxUbl in eDespatchResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox/" + outboxUbl.id + "/preview/ubl";
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicDespatchList.Add(outboxUbl.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(outboxUbl.documentNo + "mevcut değildir.");
                }
            }
            return dicDespatchList;
        }


        public Dictionary<string, byte[]> PdfOutboxEDespatch(string token)
        {
            dicDespatchList.Clear();
            foreach (var outboxPdf in eDespatchResponse.contents)
            {
                try
                {

                    string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox/" + outboxPdf.id + "/preview/pdf";
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicDespatchList.Add(outboxPdf.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(outboxPdf.documentNo + "mevcut değildir.");
                }
            }
            return dicDespatchList;
        }


        public EDespatchReceiptResponse ReceiptEDespatchList(string token)
        {
            
            string url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox?dateType=DELIVERY&status=New&" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EDespatchReceiptResponse>>(responseData);
            return deserializerData.data;
        }


    }
}
