using System;
using System.Collections.Generic;
using System.Text;

namespace Elan.HttpParser
{
    public class HttpParser
    {

        public static byte[] GenerateResponse(HttpResponse res) {

            // Imma just do this with a blocking approach
            string output = "";

            output += res.version;
            output += " ";
            output += res.code;
            output += " ";
            output += res.codeMessage;
            output += "\n";

            foreach (var header in res.headers)
            {

                output += header.Key + ": " + header.Value + "\n";

            }

            output += "\n";
            output += Encoding.UTF8.GetString(res.body);

            return Encoding.UTF8.GetBytes(output);

        }

        public static HttpRequest ParseRequest(byte[] input) {

            byte head = 0x00;
             
            string method = ""; 
            string path = "";
            string version = "";

            Dictionary<string, string> headers = new Dictionary<string, string>();

            string headerLabel = "";
            string headerValue = "";

            byte[] body = new byte[512 * 512];

            for (int i = 0; i < input.Length; i++)
            {

                switch (head)
                {

                    case 0x00:
                        // Request Method/Type

                        if (input[i] == 0x20)
                        {
                            head = 0x01;
                        }
                        else
                        {
                            method += (char)(input[i]);
                        }

                    break;

                    case 0x01:
                        // Path

                        if (input[i] == 0x20)
                        {
                            head = 0x02;
                        }
                        else {
                            path += (char)input[i];
                        }

                        break;

                    case 0x02:
                        // Version

                        if (input[i] == 0x20 || input[i] == 0x0D || input[i] == 0x0A)
                        {
                            head = 0x03;
                        }
                        else
                        {
                            version += (char)input[i];
                        }

                        break;

                    case 0x03:
                        // Start Header

                        if (input[i] != 0x20 || input[i] != 0x0D || input[i] != 0x0A)
                        {
                            headerLabel = "" + Convert.ToChar(input[i]);
                            headerValue = "";
                            head = 0x04;
                        }

                        break;

                    case 0x04:

                        // Header Label

                        if (input[i] == 0x3A)
                        {
                            // Colin Found

                            if (input[i + 1] == 0x20) i++; // Ignore Whitespace 
                            head = 0x05;

                        }
                        else {

                            headerLabel += Convert.ToChar(input[i]);

                        }

                        break;

                    case 0x05:

                        // Header Value

                        if (input[i] == 0x0D || input[i] == 0x0A)
                        {
                            // Newline Found

                            headers[headerLabel] = headerValue;

                            if (input[i + 1] == 0x0D || input[i + 1] == 0x0A)
                            {
                                head = 0x06;
                                i++;
                            }
                            else
                            {
                                head = 0x03;
                            }

                        }
                        else
                        {

                            headerValue += Convert.ToChar(input[i]);

                        }

                        break;

                    case 0x06:

                        Buffer.BlockCopy(input, i + 2, body, 0, input.Length - (i + 2));
                        i = input.Length; // exit for-loop

                        break;
                }

            }

            return new HttpRequest(method, path, version, headers, body);

        }

    }
}
