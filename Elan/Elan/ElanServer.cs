using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Elan.HttpParser;

namespace Elan
{
    public class ElanServer
    {

        private HttpListener listener;
        private bool serverRunning;

        public List<ElanEndpoint> endpointPaths = new List<ElanEndpoint>();
        public List<Regex> regexPaths = new List<Regex>();

        public HttpListenerResponse HttpListenerResponse { get; private set; }

        public void AddEndpoint(Regex regex, ElanEndpoint endpoint) {

            endpointPaths.Add(endpoint);
            regexPaths.Add(regex);

        }

        public async Task HandleIncomingConnections()
        {
            while (serverRunning)
            {

                HttpListenerContext ctx = await listener.GetContextAsync();
                string url = ctx.Request.RawUrl;
                

            }

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
            Console.WriteLine("\n######## STARTED ########\n");

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
                    bool furfilled = false;

                    HttpResponse res = new HttpResponse();

                    for (int i = 0; i < endpointPaths.Count; i++)
                    {

                        if (regexPaths[i].IsMatch(url))
                        {

                            furfilled = true;
                            res = endpointPaths[i].RunEndpoint(req);
                            break;

                        }

                    }

                    if (furfilled == false) res = dontReturn(req);

                    byte[] output = HttpParser.HttpParser.GenerateResponse(res);
                    ns.Write(output, 0, output.Length);

                    ns.Close();
                    client.Dispose();

                }

            }

        }

        public void Stop() {

            serverRunning = false;
            Console.WriteLine("\n######### ENDED #########\n");

        }

    }
}
