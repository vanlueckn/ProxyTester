using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyTester.Proxy
{
    class InvalidProxyException : Exception
    {
        public InvalidProxyException()
        {
        }

        public InvalidProxyException(string message)
            : base(message)
        {
        }

        public InvalidProxyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
