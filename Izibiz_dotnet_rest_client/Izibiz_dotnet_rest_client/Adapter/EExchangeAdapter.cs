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
    public class EExchangeAdapter
    {
        BaseAdapter baseAdapter = new BaseAdapter();
        EExchangeResponse eExchangeResponse;
        //CheckandExchangeResponse eExchangeDowloadResponse;
        Dictionary<string, byte[]> dicEExchangeList = new Dictionary<string, byte[]>();

        public BaseResponse<object> EExchangeInformation(EExchangeRequest request, string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            //var deserializerData2 = JsonConvert.SerializeObject(request);

            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            return deserializerData;
        }

        public EExchangeResponse EExchangeList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eExchangeResponse = FolderOperations.BaseDeserialize<EExchangeResponse>(responseData);
            return eExchangeResponse;
        }


        public Dictionary<string, byte[]> EExchangeDocument(string token, Enum documentType)
        {
            dicEExchangeList.Clear();
            foreach (var eExchange in eExchangeResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/exchanges/" + eExchange.id + "/preview/"+documentType.ToString().ToLower();
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicEExchangeList.Add(eExchange.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eExchange.documentNo + "mevcut değildir.");
                }
            }
            return dicEExchangeList;
        }


        public CheckandExchangeResponse EExchangeSendJson(string token)
        {
                string url = BaseAdapter.BaseUrl + "/v1/exchanges/download/ubl";
                object[] payloads = new object[1];
                var payload = new
                {
                    id = 65
                };
                payloads[0] = payload;
                var responseData = (string)baseAdapter.HttpReqRes(token, url,"POST",payloads);
            return FolderOperations.BaseDeserialize<CheckandExchangeResponse>(responseData);
        }

        public BaseResponse<object> EExchangeCompressUBL(string token,EExchangeCompressUblRequest request)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges/ubl";
            var responseData = (string)baseAdapter.HttpReqRes(token, url,"POST",request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            return deserializerData;
        }


    }
}
