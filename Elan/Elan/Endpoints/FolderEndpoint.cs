using System;
using System.IO;
using System.Net;
using System.Text;
using Elan.HttpParser;
using Elan.Util;

namespace Elan.Endpoints
{
    public class FolderEndpoint : ElanEndpoint
    {

        string path = "";

        public FolderEndpoint(string _path)
        {

            path = _path;

        }

        public override HttpResponse RunEndpoint(HttpRequest req)
        {

            HttpResponse res = new HttpResponse();

            res.body = File.ReadAllBytes(path);
            res.headers["Content-Type"] = MIMEMap.getMIMEType(path.Split(".")[path.Split(".").Length - 1]);

            return res;

        }

    }
}
