using System;
using System.Collections.Generic;
using System.IO;

namespace Elan.Elan.Util
{
    public class CodeMap
    {

        public static Dictionary<string, string> map = null;

        public static string getHttpCode(string fileType)
        {

            if (map == null) loadHttpCodes();
            return map[fileType];

        }

        public static void loadHttpCodes()
        {

            map = new Dictionary<string, string>();
            string path = "Elan/UtilStored/CodeMap.txt";
            string[] HttpCodes = File.ReadAllLines(path);

            for (int i = 0; i < HttpCodes.Length; i++)
            {
                string[] line = HttpCodes[i].Split(": ");
                map[line[0]] = line[1];

            }

        }

    }
}
