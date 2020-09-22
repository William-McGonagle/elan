using System;
using System.Collections.Generic;

namespace Elan.HttpParser
{
    public class HttpRequest
    {

        public HttpRequest(string _method, string _path, string _version, Dictionary<string, string> _headers, byte[] _body) {

            method = _method;
            path = _path;
            version = _version;
            headers = _headers;
            body = _body;

        }

        public string method;
        public string path;
        public string version;

        public Dictionary<string, string> headers;
        public byte[] body;

    }
}
