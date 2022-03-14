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
    public class AuthAdapter : BaseAdapter
    {
        BaseAdapter baseAdapter = new BaseAdapter();


        public AuthResponse Login(AuthRequest authRequest)
        {
            AuthResponse authResponse = null;
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                string url = BaseAdapter.BaseUrl + "/v1/auth/token";
                HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                var serialisedData = JsonConvert.SerializeObject(authRequest);
                byte[] auth = Encoding.UTF8.GetBytes(serialisedData);
                httpWebRequest.ContentLength = (long)auth.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(auth, 0, auth.Length);
                stream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream read = response.GetResponseStream();
                StreamReader receive = new StreamReader(read, Encoding.UTF8);
                var responseAuth = receive.ReadToEnd();
                var account = JsonConvert.DeserializeObject<BaseResponse<AuthResponse>>(responseAuth);
                authResponse = account.data;
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);   
            }

            return authResponse;

        }

        public LoggedInUserCompanyResponse LoggedInUser(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/customers/me";
            var responseLoggedInUser = (string)baseAdapter.HttpReqRes(token,url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<LoggedInUserCompanyResponse>>(responseLoggedInUser);
            LoggedInUserCompanyResponse loggedInUserCompany = deserializerData.data;
            return loggedInUserCompany;
        }



    }
}
