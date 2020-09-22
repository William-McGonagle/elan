using System;
using System.Net;
using System.Text;
using Elan;
using Elan.HttpParser;

namespace Elan.Endpoints
{
    public class StaticEndpoint : ElanEndpoint
    {

        byte[] response;
        string contentType = "text/html";

        public StaticEndpoint(string _response) {

            response = Encoding.UTF8.GetBytes(_response);

        }

        public StaticEndpoint(string _response, string _contentType)
        {

            response = Encoding.UTF8.GetBytes(_response);
            contentType = _contentType;

        }

        public override HttpResponse RunEndpoint(HttpRequest req)
        {

            HttpResponse res = new HttpResponse();

            res.body = response;
            res.headers["Content-Type"] = contentType;

            return res;

        }

    }
}
