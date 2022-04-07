using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz.Request
{

    public class EExchangeCompressUblRequest
    {
        public string documentAction { get; set; }
        public bool compressed { get; set; }
        public string content { get; set; }
    }

}
