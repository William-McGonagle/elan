using System;
using System.Net;
using System.Text;
using Elan.HttpParser;

namespace Elan
{
    public class ElanEndpoint
    {

        public virtual HttpResponse RunEndpoint(HttpRequest request) {

            HttpResponse res = new HttpResponse();

            res.body = Encoding.UTF8.GetBytes("Not Implemented");
            res.code = "501";

            return res;

        }

    }
}
