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
    public class ECheckAdapter
    {

        BaseAdapter baseAdapter = new BaseAdapter();
        ECheckResponse eTicketResponse;
        CheckandExchangeResponse eCheckDowloadResponse;
        ECheckResponse eTicketResponseList;
        Dictionary<string, byte[]> dicTicketList = new Dictionary<string, byte[]>();

        public ECheckAdapter()
        {

        }


        public CheckandExchangeResponse ECheckDownload(string token,Enum documenttype)
        {
            string url = BaseAdapter.BaseUrl + "/v1/echecks/download/" + documenttype.ToString().ToLower();
            eCheckDowloadResponse = null;
            object[] payloads = new object[1];
            var payload = new
            {
                id = eTicketResponseList.contents[0].id
            };
            payloads[0] = payload;
            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", payloads);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<CheckandExchangeResponse>>(responseData);
            eCheckDowloadResponse = deserializerData.data;
            return eCheckDowloadResponse;
   
        }

        public string ECheckDelete(string token)
        {
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox?dateType=DELIVERY&status=New&startDate=2021-10-13&endDate=2021-10-28&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            string responseData = null;
            //BaseResponse<object> deserializerData=null;
            try
            {
                string url = BaseAdapter.BaseUrl + "/v1/echecks/" + eTicketResponseList.contents[0].id;
                 responseData = (string)baseAdapter.HttpReqRes(token, url,"DELETE");
               // deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
                //eTicketResponse = deserializerData.data;
                //eTicketResponseList = eTicketResponse;
                return responseData;
                //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                //var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                //httpRequest.Method = "DELETE";

                //httpRequest.Accept = "*/*";
                //httpRequest.Headers["Authorization"] = "Bearer " + token;
                //HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
                //Stream receiveStream = response.GetResponseStream();
                //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                //responseData = readStream.ReadToEnd();

                //   var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return responseData;
        }


            public ECheckResponse ECheckList(string token)
        {
            //string url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox?dateType=DELIVERY&status=New&startDate=2021-10-13&endDate=2021-10-28&page=0&pageSize=20&sort=desc&sortProperty=supplierName";
            string url = BaseAdapter.BaseUrl + "/v1/echecks";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<ECheckResponse>>(responseData);
            eTicketResponse = deserializerData.data;
            eTicketResponseList = eTicketResponse;
            return eTicketResponse;
        }


        public Dictionary<string, byte[]> ECheckDocument(string token, Enum documentType)
        {
            dicTicketList.Clear();
            foreach (var eCheck in eTicketResponse.contents)
            {
                try
                {
                    string url = BaseAdapter.BaseUrl + "/v1/echecks/" + eCheck.id + "/preview/" + documentType.ToString().ToLower();
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicTicketList.Add(eCheck.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eCheck.documentNo + "mevcut değildir.");
                }
            }
            return dicTicketList;
        }


        public BaseResponse<object> ECheckInformation(ECheckRequest requestBody, string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/echecks";
            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", requestBody);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(responseData);
            //eCurrencyResponse = deserializerData.data;
            return deserializerData;
        }

        public BaseResponse<object> ECheckStatus(string token)
        {
            var deserializerData = baseAdapter.Status(token, EI.Type.ECheck);
            return deserializerData;
        }

    }
}
