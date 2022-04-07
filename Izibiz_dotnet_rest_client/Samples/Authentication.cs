using Izibiz;
using Izibiz.Response;
using Izibiz.Adapter;
using Izibiz.Request;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples
{
    public class Authentication
    {
       private readonly IzibizClient _izibizClient = new IzibizClient();
       public static string Token;

        [Test,Order(1)]
        public void Authentications()
        {
            var request = new AuthRequest
            {
                username = "izibiz-dev",
                password = "izi321",
            };

            var response = _izibizClient.Auth().Login(request);
            if (response == null)
            { 
                System.Environment.Exit(0);
            }
            Assert.NotNull(response);
            Token = response.accessToken;
        }


        [Test,Order(2)]
        public void LoggedInUserCompany()
        {
            
               var response = _izibizClient.Auth().LoggedInUser(Token);

        }
    }
}
