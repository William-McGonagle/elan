using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Elan.HttpParser;

namespace Elan
{
    public class ElanServer
    {

        public List<ElanEndpoint> endpointPaths = new List<ElanEndpoint>();
        public List<Regex> regexPaths = new List<Regex>();

        public void AddEndpoint(Regex regex, ElanEndpoint endpoint) {

            endpointPaths.Add(endpoint);
            regexPaths.Add(regex);

        }

        public HttpResponse dontReturn(HttpRequest req) {

            HttpResponse res = new HttpResponse();

            res.body = Encoding.UTF8.GetBytes($"Sorry, Endpoint at {req.path} Cannot Be Found");
            res.code = "404";

            return res;

        }

        public static string data = null;

        public void Start(int port) {

            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();

            Console.WriteLine(File.ReadAllText("/Users/mcgonaglew/Projects/Elan/Elan/Elan/ElanLogo.txt"));

            while (true)
            {

                TcpClient client = server.AcceptTcpClient();
                NetworkStream ns = client.GetStream();
                
                while (client.Connected)
                {

                    // Get Data from TCP Request
                    byte[] msg = new byte[2048];
                    ns.Read(msg, 0, msg.Length);

                    // Parse TCP Request into HTTP
                    HttpRequest req = HttpParser.HttpParser.ParseRequest(msg);

                    string url = req.path;
                    HttpResponse res = null;

                    for (int i = 0; i < endpointPaths.Count; i++)
                    {

                        if (regexPaths[i].IsMatch(url))
                        {

                            res = endpointPaths[i].RunEndpoint(req);
                            break;

                        }

                    }

                    // Make sure there is always a response.
                    if (res == null) res = dontReturn(req);

                    // Generate Response from Response Variable
                    byte[] output = HttpParser.HttpParser.GenerateResponse(res);
                    ns.Write(output, 0, output.Length);

                    // Close the Connection
                    ns.Close();
                    client.Dispose();

                }

            }

        }

    }
}
