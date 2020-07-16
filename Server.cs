using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace csharpsockets
{
    class SERVER_SIDE
    {
        static Socket server;
        public static int Main(string[] args)
        {
            Console.WriteLine("SERVER TERMINAL");

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string host = Dns.GetHostName();
            int port = 1234;

            IPAddress address = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, port);

            server.Bind(endPoint);
            server.Listen(5);
            Console.WriteLine("server connected");

            while (true) {
                Socket client = server.Accept();
                IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint;

                Console.WriteLine("client connected from " + clientEndPoint.Address);
                byte[] welcomeBytes = Encoding.ASCII.GetBytes("welcome to the server");

                client.Send(welcomeBytes);
                Thread.Sleep(100);
            }
        }
    }
}
