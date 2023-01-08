using EchoServerEx.Ex;
using System.Net.Sockets;
using System.Net;

namespace EchoServerEx
{
    public class EchoServer
    {
        public event AcceptedEventHandler? Accepted = null;
        public event ReceivedEventHandler? Received = null;
        public event ClosedEventHandler? Closed = null;

        public string IpString { get; private set; }
        public int Port { get; private set; }

        public EchoServer(string ipAddr, int port)
        {
            IpString = ipAddr;
            Port = port;
        }

        Socket? socket = null;

        public bool Start()
        {

            try
            {
                // 소켓 생성
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 인터 페이스 연결
                IPAddress ipAddr = IPAddress.Parse(IpString);
                IPEndPoint ipEnd = new IPEndPoint(ipAddr, Port);
                socket.Bind(ipEnd);

                // 백로그큐 크기 설정
                socket.Listen(5);

                // Accept Loop
                AcceptLoopAsync();


            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                return false;
            }
            return true;
        }


        public void socketClose()
        {
            if (socket != null)
            {
                socket?.Close();
            }
            
        }

        // 비동기 대리자
        public delegate void AcceptDele();
        private void AcceptLoopAsync()
        {
            //AcceptDele dele = new AcceptDele(AcceptLoop);
            //AcceptDele dele = AcceptLoop;
            //dele.BeginInvoke(null, null);
            ((AcceptDele)AcceptLoop).BeginInvoke(null, null);
        }

        private void AcceptLoop()
        {
            Socket? acceptedSocket = null;
            while (true) 
            {
                acceptedSocket = socket?.Accept();
                acess(acceptedSocket);
            }
        }

        // 비동기 대리자
        public delegate void acessDele(Socket socket);
        private void acess(Socket? acceptedSocket)
        {
            acessDele dele = new acessDele(acessAsync);
            dele.BeginInvoke(acceptedSocket, null, null);
        }

        private void acessAsync(Socket? acceptedSocket)
        {
            IPEndPoint remoteEP = acceptedSocket.RemoteEndPoint as IPEndPoint;
            if (Accepted != null)
            {
                Accepted(this, new AcceptedEventArgs(remoteEP));
            }

            try
            {
                byte[] packet = new byte[1024]; // 바이트 단위로 수신

                while (true)
                {
                    acceptedSocket.Receive(packet);
                    MemoryStream memoryStream = new MemoryStream(packet);
                    BinaryReader binaryReader = new BinaryReader(memoryStream);
                    string readstring = binaryReader.ReadString();

                    binaryReader.Close();
                    memoryStream.Close();

                    if (Received != null)
                    {
                        Received(this, new ReceivedEventArgs(remoteEP, readstring));
                    }
                    acceptedSocket.Send(packet);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            finally
            {
                acceptedSocket.Close();

                if (Closed != null)
                {
                    Closed(this, new ClosedEventArgs(remoteEP));
                }
            }

        }
    }
}