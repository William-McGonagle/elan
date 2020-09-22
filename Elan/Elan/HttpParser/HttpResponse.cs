using System;
using System.Collections.Generic;

namespace Elan.HttpParser
{
    public class HttpResponse
    {

        public HttpResponse() {

            code = "200";
            codeMessage = "OK";
            version = "HTTP/1.1";
            headers = new Dictionary<string, string>();
            body = new byte[0];

        }

        public HttpResponse(string _code, string _codeMessage, string _version, Dictionary<string, string> _headers, byte[] _body)
        {

            code = _code;
            codeMessage = _codeMessage;
            version = _version;
            headers = _headers;
            body = _body;

        }

        public string code;
        public string codeMessage;
        public string version;

        public Dictionary<string, string> headers;
        public byte[] body;

    }
}
