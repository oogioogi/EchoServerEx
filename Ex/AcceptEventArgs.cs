using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EchoServerEx.Ex
{
    public delegate void AcceptedEventHandler(object sender, AcceptedEventArgs e);

    public class AcceptedEventArgs: EventArgs
    {
        public IPEndPoint RemoteEP { get; private set; }

        public int Port { get { return RemoteEP.Port; } }

        public string IpString { get { return RemoteEP.ToString(); } }

        public AcceptedEventArgs(IPEndPoint remoteEP)
        {
            RemoteEP = remoteEP;
        }
    }
}
