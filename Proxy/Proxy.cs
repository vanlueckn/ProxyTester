using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyTester.Proxy
{
    class Proxy
    {
        private string _proxyString;
        public string Host;
        public int Port;
        public string User;
        public string Pass;
        public bool Ready = false;
        public bool Success = false;
        private ProxyItem _proxyItem;

        public Proxy(string proxyString)
        {
            _proxyString = proxyString;
            Parse();
            GenerateRow();
        }

        private void GenerateRow()
        {
            _proxyItem = new ProxyItem { IP = Host, Port = Port.ToString(), User = User, Pass = Pass, Status = "pending", Speed = ""};

        }

        private bool Validate()
        {
            if (_proxyString is null || _proxyString.Split(":").Length < 3) return false;

            return true;
        }

        private void Parse()
        {
            if (!Validate()) throw new InvalidProxyException("Provided string is invalid");

            string[] splittedProxy = _proxyString.Split(":");

            Host = splittedProxy[0];
            Port = int.Parse(splittedProxy[1]);
            User = splittedProxy[2];
            Pass = splittedProxy[3];

        }
    }
}
