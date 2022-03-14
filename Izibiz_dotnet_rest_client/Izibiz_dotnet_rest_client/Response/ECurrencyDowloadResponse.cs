using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz_dotnet_rest_client.Response
{
    public class ECurrencyDowloadResponse
    {
        public string filename { get; set; }
        public byte[] content { get; set; }
    }
}
