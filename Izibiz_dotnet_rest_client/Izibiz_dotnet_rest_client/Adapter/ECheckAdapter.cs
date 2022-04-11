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
        ECheckResponse eCheckResponse;
        CheckandExchangeResponse eCheckDowloadResponse;
        ECheckResponse eCheckResponseList;
        Dictionary<string, byte[]> dicTicketList = new Dictionary<string, byte[]>();

        public ECheckAdapter()
        {

        }


        public CheckandExchangeResponse ECheckDownload(string token, Enum documenttype)
        {
            string url = BaseAdapter.BaseUrl + "/v1/echecks/download/" + documenttype.ToString().ToLower();
            eCheckDowloadResponse = null;
            object[] payloads = new object[1];
            var payload = new
            {
                id = eCheckResponseList.contents[0].id
            };
            payloads[0] = payload;
            var responseData = (string)baseAdapter.HttpReqRes(token, url, "POST", payloads);
            eCheckDowloadResponse = FolderOperations.BaseDeserialize<CheckandExchangeResponse>(responseData);
            return eCheckDowloadResponse;

        }

        public string ECheckDelete(string token)
        {

            string responseData = null;
            //BaseResponse<object> deserializerData=null;
            try
            {
                string url = BaseAdapter.BaseUrl + "/v1/echecks/" + eCheckResponseList.contents[0].id;
                responseData = (string)baseAdapter.HttpReqRes(token, url, "DELETE");
                return responseData;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return responseData;
        }


        public ECheckResponse ECheckList(string token)
        {

            string url = BaseAdapter.BaseUrl + "/v1/echecks";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            eCheckResponse = FolderOperations.BaseDeserialize<ECheckResponse>(responseData);
            eCheckResponseList = eCheckResponse;
            return eCheckResponse;
        }


        public Dictionary<string, byte[]> ECheckDocument(string token, Enum documentType)
        {
            dicTicketList.Clear();
            foreach (var eCheck in eCheckResponse.contents)
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
