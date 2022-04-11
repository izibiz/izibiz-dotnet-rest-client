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
    public class EDespatchReceiptAdapter
    {
        EDespatchReceiptResponse eDespatchReceiptResponse;
        EDespatchReceiptResponse eDespatchReceiptResponseOutbox;
        
        Dictionary<string, byte[]> dicEDespacthReceiptList = new Dictionary<string, byte[]>();
        Dictionary<string, byte[]> dicEDespacthReceiptList_outbox = new Dictionary<string, byte[]>();
        BaseAdapter baseAdapter = new BaseAdapter();
       
        public EDespatchReceiptResponse EDespatchReceiptList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox?dateType=DELIVERY&" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eDespatchReceiptResponse = FolderOperations.BaseDeserialize<EDespatchReceiptResponse>(responseData);
            return eDespatchReceiptResponse;
        }

        public BaseResponse<object> EDespatchReceiptStatusInquery(string token)
        {
            var id = eDespatchReceiptResponse.contents[0].id;
            string url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox/" + id;
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            return deserializerData;
        }


        public Dictionary<string, byte[]> EDespatchReceiptDocument(string token,Enum documentType)
        {
            dicEDespacthReceiptList.Clear();
            foreach (var inboxUbl in eDespatchReceiptResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType.Equals(EI.DocumentType.XML))
                    {
                         url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox/" + inboxUbl.id + "/preview/ubl";
                    }else if(documentType.Equals(EI.DocumentType.HTML))
                    {
                         url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox/" + inboxUbl.id + "/html";
                    }
                    else if (documentType.Equals(EI.DocumentType.PDF))
                    {
                         url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox/" + inboxUbl.id + "/pdf";
                    }
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicEDespacthReceiptList.Add(inboxUbl.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(inboxUbl.documentNo + "mevcut değildir.");
                }
            }
            return dicEDespacthReceiptList;
        }


        public BaseResponse<object> EDespatchReceiptStatus(string token)
        {
            var deserializerData = baseAdapter.Status(token,EI.Type.EDespatchReceipt,EI.Status.Inbox);
            return deserializerData;
        }


        //Outbox


        public EDespatchReceiptResponse EDespatchReceiptList_Outbox(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/outbox?" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=100&sortProperty=createDate&sort=asc";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eDespatchReceiptResponseOutbox = FolderOperations.BaseDeserialize<EDespatchReceiptResponse>(responseData);
            return eDespatchReceiptResponseOutbox;
        }   
    }
}
