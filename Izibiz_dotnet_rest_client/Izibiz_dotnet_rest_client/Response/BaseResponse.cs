using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Response
{
    
        public class BaseResponse<T>
        {
            public T data { get; set; }
            public Error error { get; set; }
        }
        public class Error
        {
            public string code { get; set; }
            public string message { get; set; }
            public string group { get; set; }
        }

    
}
