using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ProxyTester.Proxy
{
    class ProxyThread
    {
        private Proxy _proxy;
        private Uri _url;
        private bool _success = false;
        private bool _isReady = false;

        public ProxyThread(Proxy proxy, string url)
        {
            _proxy = proxy;
            _url = new Uri(url);
        }

        public void Start(Object state)
        {

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);

            WebProxy webProxy = new WebProxy(_url, false, new string[0], new NetworkCredential(_proxy.User, _proxy.Pass));
            httpWebRequest.Proxy = webProxy;
            httpWebRequest.Timeout = 3000;

            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();
                _proxy.ProxyItem.Status = "failed";
            }
            catch (WebException)
            {
                _proxy.ProxyItem.Status = "success";
            }

            _proxy.UpdateRowThreadSafe();
        }

    }
}
