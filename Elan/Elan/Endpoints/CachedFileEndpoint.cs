using System;
using System.IO;
using System.Net;
using System.Text;
using Elan;
using Elan.HttpParser;
using Elan.Util;

namespace Elan.Endpoints
{
    public class CachedFileEndpoint : ElanEndpoint
    {

        string contentType = "";
        byte[] fileData;

        public CachedFileEndpoint(string _path)
        {

            fileData = File.ReadAllBytes(_path);
            contentType = MIMEMap.getMIMEType(_path.Split(".")[_path.Split(".").Length - 1]);

        }

        public CachedFileEndpoint(string _path, string _contentType)
        {

            fileData = File.ReadAllBytes(_path);
            contentType = _contentType; 

        }

        public override HttpResponse RunEndpoint(HttpRequest req)
        {

            HttpResponse res = new HttpResponse(); 

            res.body = fileData;
            res.headers["Content-Type"] = contentType;
            res.headers["Cache-Control"] = "public";

            return res;

        }

    }
}
