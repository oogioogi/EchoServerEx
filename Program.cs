
namespace EchoServerEx
{
	class EchoServerEx
	{

		public static void Main()
		{
            EchoServer echo = new EchoServer("192.168.0.10", 11000);
            echo.Accepted += AcceptedEventHandler;
            echo.Received += ReceivedEventHandler;
            echo.Closed += ClosedEventHandler;

			if (echo.Start() == false)
			{
				Console.WriteLine("Server Run Fail");
			}
			Console.ReadKey();
        }

        private static void ClosedEventHandler(object sender, Ex.ClosedEventArgs e)
        {
            Console.WriteLine($"{e.IpString} : {e.Port} 에서 닫혔음");
        }

        private static void ReceivedEventHandler(object sender, Ex.ReceivedEventArgs e)
        {
            Console.WriteLine($"{e.IpString} : {e.Port} 에서 {e.Msg} 받음");
        }

        private static void AcceptedEventHandler(object sender, Ex.AcceptedEventArgs e)
        {
            Console.WriteLine($"{e.IpString} : {e.Port} 에서 연결됨");
        }
    }

}
