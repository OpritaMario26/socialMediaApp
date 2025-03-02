using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WinFormsApp1
{
    internal class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public override string ToString()
        {
            return name; 
        }
    }
}
