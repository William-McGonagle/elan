using System;
using System.IO;
using System.Net;
using System.Text;
using Elan.HttpParser;
using Elan.Util;

namespace Elan.Endpoints
{
    public class FileEndpoint : ElanEndpoint
    {

        string contentType = "text/plaintext";
        string path = "";

        public FileEndpoint(string _path)
        {

            path = _path;
            contentType = MIMEMap.getMIMEType(_path.Split(".")[_path.Split(".").Length - 1]);

        }

        public FileEndpoint(string _path, string _contentType)
        {

            path = _path;
            contentType = _contentType;

        }

        public override HttpResponse RunEndpoint(HttpRequest req)
        {

            HttpResponse res = new HttpResponse();

            res.body = File.ReadAllBytes(path);
            res.headers["Content-Type"] = contentType;
            res.headers["Cache-Control"] = "public";

            return res;

        }

    }
}
