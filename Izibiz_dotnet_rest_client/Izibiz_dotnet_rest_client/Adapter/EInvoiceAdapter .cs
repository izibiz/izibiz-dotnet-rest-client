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
    public class EInvoiceAdapter
    {
        EInvoiceResponse eInvoiceResponse;
        EInvoiceResponse eInvoiceListResponse;
        EInvoiceResponse eInvoiceListResponse_Outbox;
        Dictionary<string, byte[]> dicEInvoiceList = new Dictionary<string, byte[]>();
        Dictionary<string, byte[]> dicEInvoiceList_outbox = new Dictionary<string, byte[]>();
        BaseAdapter baseAdapter = new BaseAdapter();

        public EInvoiceResponse EInvoiceList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox?dateType=DELIVERY&status=New&startDate=" + baseAdapter.startDate+ "&endDate=" + baseAdapter.endDate+"&page=1&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            eInvoiceListResponse = eInvoiceResponse;
            return eInvoiceResponse;
        }


        public EInvoiceResponse ApprovalExpiredEInvoice(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/inbox?status=ResponseTimeExpired&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }

        public EInvoiceResponse UndeliverableAnswerEInvoiceList(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/inbox?status=ResponseUnDelivered&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }

        public EInvoiceResponse RejectedEInvoiceList(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/inbox?status=Rejected&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }

        public Dictionary<string, byte[]> InboxViewEInvoice(string token,string documentType)
        {
            dicEInvoiceList.Clear();
            foreach (var inboxHtml in eInvoiceListResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType == "HTML")
                    {  url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/" + inboxHtml.id + "/html"; }
                    else if(documentType == "XML")
                    {  url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/" + inboxHtml.id + "/preview/ubl"; }
                    else if (documentType == "PDF")
                    {  url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/" + inboxHtml.id + "/preview/pdf"; }
                    var responseData = baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes((string)responseData);
                    dicEInvoiceList.Add(inboxHtml.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(inboxHtml.documentNo + "mevcut değildir.");
                }
            }
            return dicEInvoiceList;
        }


        public BaseResponse<object> EInvoiceStatus(string token,string status)
        {
            BaseResponse<object> deserializerData;
            if (status == "Inbox")
            {
                deserializerData = baseAdapter.Status(token, nameof(EI.Type.EInvoice), nameof(EI.Status.Inbox));
            }
            else
            {
                deserializerData = baseAdapter.Status(token, nameof(EI.Type.EInvoice), nameof(EI.Status.Outbox));
            }
            return deserializerData;
        }

        //OUTBOX

        public EInvoiceResponse EInvoiceList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/outbox?page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData =(string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            eInvoiceListResponse_Outbox = eInvoiceResponse;
            return eInvoiceResponse;
        }

        public EInvoiceResponse PendingApprovalEInvoice_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/outbox?status=WaitingForResponse&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }


        public EInvoiceResponse ApprovalExpiredEInvoice_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/outbox?status=ResponseTimeExpired&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData =(string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            //dictionary.Add("ApprovalExpiredEInvoice", eInvoiceResponse);
            return eInvoiceResponse;
        }

        public EInvoiceResponse UndeliverableEInvoiceList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/outbox?status=UnDelivered&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>((string)responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }


        public EInvoiceResponse RejectedEInvoiceList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/outbox?status=Rejected&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&page=0&pageSize=100&sortProperty=createDate&sort=asc";

            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EInvoiceResponse>>(responseData);
            eInvoiceResponse = deserializerData.data;
            return eInvoiceResponse;
        }

        public Dictionary<string, byte[]> EInvoiceUbl_Outbox(string token)
        {
            EInvoiceList_Outbox(token);
            foreach (var outboxUbl in eInvoiceListResponse_Outbox.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl +"/v1/einvoices/outbox/" + outboxUbl.id + "/preview/ubl";
                    var responseData =(string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicEInvoiceList_outbox.Add(outboxUbl.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(outboxUbl.documentNo + "mevcut değildir.");
                }
            }
            return dicEInvoiceList_outbox;
        }

        public Dictionary<string, byte[]> EInvoicePDF_Outbox(string token)
        {
            dicEInvoiceList_outbox.Clear();
            foreach (var outboxUbl in eInvoiceListResponse_Outbox.contents)
            {
                try
                {
                   
                    string url = BaseAdapter.BaseUrl + "/v1/einvoices/outbox/" + outboxUbl.id + "/preview/pdf";
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicEInvoiceList_outbox.Add(outboxUbl.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(outboxUbl.documentNo + "mevcut değildir.");
                }
            }
            return dicEInvoiceList_outbox;
        }


    }
}
