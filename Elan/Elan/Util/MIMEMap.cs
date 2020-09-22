using System;
using System.Collections.Generic;
using System.IO;

namespace Elan.Util
{
    public class MIMEMap
    {

        public static Dictionary<string, string> map = null;

        public static string getMIMEType(string fileType) {

            if (map == null) loadMIMETypes();
            return map[fileType];

        }

        public static void loadMIMETypes() {

            map = new Dictionary<string, string>();
            string path = "/Users/mcgonaglew/Projects/Elan/Elan/Elan/UtilStored/MIMEMap.txt";
            string[] MIMETypes = File.ReadAllLines(path);

            for (int i = 0; i < MIMETypes.Length; i++)
            {
                string[] line = MIMETypes[i].Split(": ");
                map[line[0]] = line[1];

            }

        }

    }
}
