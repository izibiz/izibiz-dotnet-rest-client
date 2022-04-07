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
    public class CreditNoteAdapter
    {
        BaseAdapter baseAdapter = new BaseAdapter();
        CreditNoteResponse creditNoteResponse;
        Dictionary<string, byte[]> dicCreditNoteList = new Dictionary<string, byte[]>();

        public CreditNoteResponse List(string token)
        {
            string url = BaseAdapter.BaseUrl + "/v1/ecreditnotes";
            var responseData = (string)baseAdapter.HttpReqRes(token, url);
            var deserializerData = JsonConvert.DeserializeObject<BaseResponse<CreditNoteResponse>>(responseData);
            creditNoteResponse = deserializerData.data;
            return creditNoteResponse;
        }


        public Dictionary<string, byte[]> GetCreditNoteDocument(string token, Enum documentType)
        {
            dicCreditNoteList.Clear();
            foreach (var creditnote in creditNoteResponse.contents)
            {
                try
                {
                    string url = "";
                    if (documentType.Equals(EI.DocumentType.XML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/ecreditnotes/" + creditnote.id + "/preview/ubl";
                    }
                    else if (documentType.Equals(EI.DocumentType.HTML))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/ecreditnotes/" + creditnote.id + "/preview/html";
                    }
                    else if (documentType.Equals(EI.DocumentType.PDF))
                    {
                        url = BaseAdapter.BaseUrl + "/v1/ecreditnotes/" + creditnote.id + "/preview/pdf";
                    }
                    var responseData = (string)baseAdapter.HttpReqRes(token, url);
                    byte[] bytes = Encoding.ASCII.GetBytes(responseData);
                    dicCreditNoteList.Add(creditnote.documentNo, bytes);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(creditnote.documentNo + "mevcut değildir.");
                }
            }
            return dicCreditNoteList;
        }

         public BaseResponse<object> GetCreditNoteStatus(string token)
        {
            var deserializerData = baseAdapter.Status(token, EI.Type.CreditNote);
            return deserializerData;
        }

    }
}
