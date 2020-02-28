using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Magazine
{
    public static class StaticHelper
    {
        public static string URL = "";
        public static void ReadFromFile()
        {
            string text = File.ReadAllText(@"url.txt", Encoding.UTF8);
            URL = text;
        }
    }
}
