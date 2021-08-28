using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

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
        public ProxyItem ProxyItem;
        private ListView _listView;
        private int _id = -2;
        private Dispatcher _dispatcher;

        public Proxy(string proxyString, ListView listView, Dispatcher dispatcher)
        {
            _proxyString = proxyString;
            _listView = listView;
            _dispatcher = dispatcher;

            Parse();
            GenerateRow();
        }

        private void GenerateRow()
        {
            ProxyItem = new ProxyItem { IP = Host, Port = Port.ToString(), User = User, Pass = Pass, Status = "pending", Speed = ""};
            _id = _listView.Items.Add(ProxyItem);
        }

        public void UpdateRow()
        {
            _listView.Items[_id] = ProxyItem;
        }

        public void UpdateRowThreadSafe()
        {
            if (_id < 0 || ProxyItem is null) return;
          
            _dispatcher.BeginInvoke(new Action(delegate ()
            {
                UpdateRow();
            }));
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

        public void Run()
        {
            ProxyThread proxyThread = new ProxyThread(this, "http://techkings.de");
            
            ThreadPool.SetMaxThreads(10, 10);

            ProxyItem.Status = "testing";
            UpdateRow();

            ThreadPool.QueueUserWorkItem(proxyThread.Start);
        }
    }
}
