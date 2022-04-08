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
        EExchangeResponse eCurrencyResponse;
        CheckandExchangeResponse eExchangeDowloadResponse;
        Dictionary<string, byte[]> dicECurrencyList = new Dictionary<string, byte[]>();

        public BaseResponse<object> EExchangeInformation(EExchangeRequest request, string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            var deserializerData2 = JsonConvert.SerializeObject(request);

            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }

        public EExchangeResponse EExchangeList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<EExchangeResponse>>(responseData);
            eCurrencyResponse = deserializerData.data;
            return eCurrencyResponse;
        }


        public Dictionary<string, byte[]> EExchangeDocument(string token, Enum documentType)
        {
            dicECurrencyList.Clear();
            foreach (var eExchange in eCurrencyResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/exchanges/" + eExchange.id + "/preview/"+documentType.ToString().ToLower();
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicECurrencyList.Add(eExchange.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eExchange.documentNo + "mevcut değildir.");
                }
            }
            return dicECurrencyList;
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
                var deserializerData = JsonConvert.DeserializeObject<BaseResponse<CheckandExchangeResponse>>(responseData);
                eExchangeDowloadResponse = deserializerData.data;
                return eExchangeDowloadResponse;

        }

        public BaseResponse<object> EExchangeCompressUBL(string token,EExchangeCompressUblRequest request)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges/ubl";
            var responseData = (string)baseAdapter.HttpReqRes(token, url,"POST",request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }


    }
}
