using Izibiz_dotnet_rest_client.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Izibiz_dotnet_rest_client.Adapter
{
    public class BaseAdapter
    {
        public const string BaseUrl = "https://apitest.izibiz.com.tr";
        public string endDate = DateTime.Now.ToString("yyyy-MM-dd");
        public string startDate = DateTime.Now.AddYears(-2).ToString("yyyy-MM-dd");


        public object HttpReqRes(string token, string url, string requestType = "", object dto = null)
        {
            object responseData = null;
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.UseDefaultCredentials = true;
                httpWebRequest.Headers["Authorization"] = "Bearer " + token;
                httpWebRequest.ContentType = "application/json";
                if (requestType == "POST")
                {
                    httpWebRequest.PreAuthenticate = true;
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                    var serialisedData = JsonConvert.SerializeObject(dto);
                    byte[] serialisedByte = Encoding.UTF8.GetBytes(serialisedData);
                    httpWebRequest.ContentLength = (long)serialisedByte.Length;

                    Stream stream = httpWebRequest.GetRequestStream();
                    stream.Write(serialisedByte, 0, serialisedByte.Length);
                    stream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                responseData = readStream.ReadToEnd();
            }
            catch (WebException e)
            {
                Stream receiveStream = e.Response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                responseData = readStream.ReadToEnd();

                System.Diagnostics.Debug.WriteLine(responseData);
            }
            return responseData;
        }


        public BaseResponse<object> Status(string token, string type, string status = "")
        {
            string url = "";
            if (type == "EInvoice")
            {
                if (status == "Inbox")
                {
                    url = BaseAdapter.BaseUrl + "/v1/einvoices/inbox/lookup-statuses";
                }
                else if (status == "Outbox")
                {
                    url = BaseAdapter.BaseUrl + "/v1/einvoices/outbox/lookup-statuses";
                }
            }
            else if (type == "EDespatch")
            {
                if (status == "Inbox")
                { url = BaseAdapter.BaseUrl + "/v1/edespatches/inbox/lookup-statuses"; }
                else
                {
                    url = BaseAdapter.BaseUrl + "/v1/edespatches/outbox/lookup-statuses";
                }
            }
            else if (type == "EArchive")
            {
                url = BaseAdapter.BaseUrl + "/v1/earchives/lookup-statuses";
            }
            else if (type == "EDespatchReceipt")
            {
                if (status == "Inbox")
                {
                    url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/inbox/lookup-statuses";
                }
                else
                {
                    url = BaseAdapter.BaseUrl + "/v1/edespatch-responses/outbox/lookup-statuses";
                }
            }
            else if (type == "CreditNote")
            {
                url = BaseAdapter.BaseUrl + "/v1/ecreditnotes/lookup-statuses";
            }
            else if (type == "ESmm")
            {
                url = BaseAdapter.BaseUrl + "/v1/esmms/lookup-statuses";
            }

            var StatusResponse = (string)HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<object>>(StatusResponse);
            return deserializerData;
        }


       
    }
}
