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


        public EDespatchResponse EDespatchList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox?dateType=DELIVERY&" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eDespatchResponse= FolderOperations.BaseDeserialize<EDespatchResponse>(responseData);
            eDespatchResponseList = eDespatchResponse;
            return eDespatchResponse;
        }

        public BaseResponse<object> EDespatchStatusInquery(string token)
        {
            var id = eDespatchResponse.contents[0].id;
            string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox/"+id;
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            return deserializerData;
        }


        public BaseResponse<object> EDespatchStatus(string token,Enum status)
        {
            BaseResponse<object> deserializerData;
            deserializerData = baseAdapter.Status(token, EI.Type.EDespatch, status);
            return deserializerData;
        }



        public Dictionary<string, byte[]> GetEDespatchDocument(string token,Enum documentType)
        {
            dicDespatchList.Clear();
            foreach (var despatch in eDespatchResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox/" + despatch.id + "/preview/"+documentType.ToString().ToLower();
                    var responseData = baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes((string)responseData);
                    dicDespatchList.Add(despatch.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(despatch.documentNo + "mevcut değildir.");
                }
            }
            return dicDespatchList;
        }


        public EDespatchReceiptResponse ReceiptEDespatchList(string token)
        {
            
            string url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox?dateType=DELIVERY&status=New&" + baseAdapter.startDate + "&" + baseAdapter.endDate + "&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            return FolderOperations.BaseDeserialize<EDespatchReceiptResponse>(responseData);
        }


    }
}
