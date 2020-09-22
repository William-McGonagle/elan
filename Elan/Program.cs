using System;
using System.Text;
using System.Text.RegularExpressions;
using Elan;
using Elan.Endpoints;
using Elan.HttpParser;

class Program
{
    static void Main(string[] args)
    {

        ElanServer server = new ElanServer();

        server.AddEndpoint(new Regex(@"^\/$"), new StaticEndpoint("/", "text/plaintext"));
        server.AddEndpoint(new Regex(@"(^/test$)|(^/test/$)"), new StaticEndpoint("/test"));
        server.AddEndpoint(new Regex(@"(^/json$)|(^/json/$)"), new FileEndpoint("/Users/mcgonaglew/Projects/Elan/Elan/gutenberg.txt"));
        server.AddEndpoint(new Regex(@"(^/cachedJson$)|(^/cachedJson/$)"), new CachedFileEndpoint("/Users/mcgonaglew/Projects/Elan/Elan/gutenberg.txt"));
        server.AddEndpoint(new Regex(@"(^/folder)|(^/folder/$)"), new FolderEndpoint("/Users/mcgonaglew/Projects/Elan/Elan/gutenberg.txt"));

        server.Start(8000);

    }

}
