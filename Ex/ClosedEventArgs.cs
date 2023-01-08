using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EchoServerEx.Ex
{
    public delegate void ClosedEventHandler(object sender, ClosedEventArgs e);

    public class ClosedEventArgs: EventArgs
    {
        public IPEndPoint RemoteEP { get; private set; }

        public int Port { get { return RemoteEP.Port; } }

        public string IpString { get { return RemoteEP.ToString(); } }

        public ClosedEventArgs(IPEndPoint remoteEP)
        {
            RemoteEP = remoteEP;
        }
    }
}
