using Izibiz_dotnet_rest_client.Operations;
using Izibiz_dotnet_rest_client.Request;
using Izibiz_dotnet_rest_client.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Izibiz_dotnet_rest_client.Adapter
{
    public class ESmmAdapter
    {
        BaseAdapter baseAdapter = new BaseAdapter();
        ESmmResponse eSmmResponse;
        Dictionary<string, byte[]> dicCreditNoteList = new Dictionary<string, byte[]>();

        public ESmmResponse List(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/esmms";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<ESmmResponse>>(responseData);
            eSmmResponse = deserializerData.data;
            return eSmmResponse;
        }


        public Dictionary<string, byte[]> ESmmDocument(string token, string documentType)
        {
            dicCreditNoteList.Clear();
            foreach (var eSmm in eSmmResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType == nameof(EI.DocumentType.XML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/esmms/" + eSmm.id + "/preview/ubl";
                    }
                    else if (documentType == nameof(EI.DocumentType.HTML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/esmms/" + eSmm.id + "/preview/html";
                    }
                    //else if (documentType == nameof(EI.DocumentType.PDF))
                    //{
                    //    url = BaseAdapter.BaseUrl + "/v1/esmms/" + eSmm.id + "/preview/pdf";
                    //}
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicCreditNoteList.Add(eSmm.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(eSmm.documentNo + "mevcut değildir.");
                }
            }
            return dicCreditNoteList;
        }

        public BaseResponse<object> ESmmStatus(string token)
        {
            var deserializerData = baseAdapter.Status(token, nameof(EI.Type.ESmm));
            return deserializerData;
        }

    }
}
