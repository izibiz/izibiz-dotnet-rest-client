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
    public class ECurrencyAdapter
    {
        BaseAdapter baseAdapter = new BaseAdapter();
        ECurrencyResponse eCurrencyResponse;
        ECurrencyDowloadResponse eCurrencyDowloadResponse;
        Dictionary<string, byte[]> dicECurrencyList = new Dictionary<string, byte[]>();

        public BaseResponse<object> ECurrencyInformation(ECurrencyRequest request, string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            var deserializerData2 = JsonConvert.SerializeObject(request);

            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }

        public ECurrencyResponse ECurrencyList(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<ECurrencyResponse>>(responseData);
            eCurrencyResponse = deserializerData.data;
            return eCurrencyResponse;
        }


        public Dictionary<string, byte[]> ECurrencyDocument(string token, string documentType)
        {
            dicECurrencyList.Clear();
            foreach (var eCurrency in eCurrencyResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType == nameof(EI.DocumentType.XML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/exchanges/" + eCurrency.id + "/preview/ubl";
                    }
                    else if (documentType == nameof(EI.DocumentType.HTML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/exchanges/" + eCurrency.id + "/preview/html";
                    }
                    else if (documentType == nameof(EI.DocumentType.PDF))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/exchanges/" + eCurrency.id + "/preview/pdf";
                    }
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicECurrencyList.Add(eCurrency.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eCurrency.documentNo + "mevcut değildir.");
                }
            }
            return dicECurrencyList;
        }


        public ECurrencyDowloadResponse ECurrencySendJson(string token)
        {
                string url = BaseAdapter.BaseUrl + "/v1/exchanges/download/ubl";
                object[] payloads = new object[1];
                var payload = new
                {
                    id = 65
                };
                payloads[0] = payload;
                var responseData = (string)baseAdapter.HttpReqRes(token, url,"POST",payloads);
                var deserializerData = JsonConvert.DeserializeObject<BaseResponse<ECurrencyDowloadResponse>>(responseData);
                eCurrencyDowloadResponse = deserializerData.data;
                return eCurrencyDowloadResponse;

        }

        public BaseResponse<object> ECurrencyCompressUBL(string token,ECurrencyCompressUblRequest request)
        {
            string url = BaseAdapter.BaseUrl + "/v1/exchanges/ubl";
            var responseData = (string)baseAdapter.HttpReqRes(token, url,"POST",request);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }


    }
}
