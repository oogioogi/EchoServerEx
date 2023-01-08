using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EchoServerEx.Ex
{
    public delegate void ReceivedEventHandler(object sender, ReceivedEventArgs e);

    public class ReceivedEventArgs: EventArgs
    {
        public IPEndPoint RemoteEP { get; private set; }

        public int Port { get { return RemoteEP.Port; } }

        public string IpString { get { return RemoteEP.ToString(); } }

        public string Msg { get; private set; }

        public ReceivedEventArgs(IPEndPoint remoteEP, string msg)
        {
            RemoteEP = remoteEP;
            Msg = msg;
        }
    }
}
