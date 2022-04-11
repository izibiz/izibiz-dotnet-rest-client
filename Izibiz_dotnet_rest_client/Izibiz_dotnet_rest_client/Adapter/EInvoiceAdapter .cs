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
        EInvoiceResponse eInvoiceList;
        Dictionary<string, byte[]> dicEInvoiceList = new Dictionary<string, byte[]>();
        Dictionary<string, byte[]> dicEInvoiceList_outbox = new Dictionary<string, byte[]>();
        BaseAdapter baseAdapter = new BaseAdapter();

        public EInvoiceResponse EInvoiceList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox?dateType=DELIVERY&status=New&startDate=" + baseAdapter.startDate+ "&endDate=" + baseAdapter.endDate+"&page=1&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eInvoiceListResponse= FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }


        public EInvoiceResponse UndeliverableAnswerEInvoiceList(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/inbox?status=ResponseUnDelivered&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }
       
        public Dictionary<string, byte[]> InboxViewEInvoice(string token,Enum documentType)
        {
            dicEInvoiceList.Clear();
            foreach (var inboxHtml in eInvoiceListResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType.Equals(EI.DocumentType.HTML))
                    {  url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/" + inboxHtml.id + "/html"; }
                    else if(documentType.Equals(EI.DocumentType.XML))
                    {  url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/" + inboxHtml.id + "/preview/ubl"; }
                    else if (documentType.Equals(EI.DocumentType.PDF))
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
        

        public BaseResponse<object> EInvoiceStatus(string token,Enum status)
        {
            BaseResponse<object> deserializerData;
            deserializerData = baseAdapter.Status(token, EI.Type.EInvoice, status);
            return deserializerData;
        }

        //OUTBOX

        public EInvoiceResponse EInvoiceList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/outbox?page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData =(string)baseAdapter.HttpReqRes(token, url);
            eInvoiceListResponse_Outbox = FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
            return eInvoiceListResponse_Outbox;

        }

        public EInvoiceResponse PendingApprovalEInvoice_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/outbox?status=WaitingForResponse&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }


        public EInvoiceResponse ApprovalExpiredEInvoice(string token,Enum productStatus)
        {
            string url = BaseAdapter.BaseUrl+"/v1/einvoices/"+productStatus.ToString().ToLower()+"?status=ResponseTimeExpired&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }

        public EInvoiceResponse UndeliverableEInvoiceList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/outbox?status=UnDelivered&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }


        public EInvoiceResponse RejectedEInvoiceList(string token,Enum productStatus)
        {

            string url = BaseAdapter.BaseUrl+ "/v1/einvoices/"+ productStatus.ToString().ToLower() + "?status=Rejected&dateType=DELIVERY&startDate=" + baseAdapter.startDate + "&endDate=" + baseAdapter.endDate + "&page=0&pageSize=100&sortProperty=createDate&sort=asc";

            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EInvoiceResponse>(responseData);
        }

         /* Şimdilik çalışmıyor inboxdaki html alanı prewiev olduktan sonra çalışır.
        public Dictionary<string, byte[]> GetEInvoiceDocument(string token, string documentType,string productStatus)
        {
            dicEInvoiceList_outbox.Clear();
            dicEInvoiceList.Clear();
            if(productStatus==nameof(EI.Status.Outbox))
            {
                eInvoiceList = eInvoiceListResponse;
            }
            else if(productStatus == nameof(EI.Status.Inbox))
            {
                eInvoiceList = eInvoiceListResponse_Outbox;
            }
            foreach (var invoice in eInvoiceList.contents)
            {
                try
                {

                    string url = BaseAdapter.BaseUrl + "/v1/einvoices/"+productStatus.ToLower()+"/" + invoice.id + "/preview/" + documentType.ToLower();
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicEInvoiceList_outbox.Add(invoice.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(invoice.documentNo + "mevcut değildir.");
                }
            }
            return dicEInvoiceList_outbox;
        }
        */

        public Dictionary<string, byte[]> GetEInvoiceDocument_Outbox(string token, Enum documentType)
        {
            dicEInvoiceList_outbox.Clear();
            EInvoiceList_Outbox(token);
            foreach (var outboxUbl in eInvoiceListResponse_Outbox.contents)
            {
                try
                {

                    string url = BaseAdapter.BaseUrl + "/v1/einvoices/outbox/" + outboxUbl.id + "/preview/"+documentType.ToString().ToLower();
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
