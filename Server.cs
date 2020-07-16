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
        static Socket client;
        public static void Main(string[] args)
        {
            Console.WriteLine("[SERVER TERMINAL]");

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string host = Dns.GetHostName();
            int port = 1234;

            IPAddress address = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, port);

            server.Bind(endPoint);
            server.Listen(5);
            Console.WriteLine("server connected");

            getClients();
        }

        static void  getClients() {
            while (true) {
                client = server.Accept();
                IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint;

                Console.WriteLine("client connected from [IP: {0}, PORT: {1}]", clientEndPoint.Address, clientEndPoint.Port);
                string message = "welcome to the server";

                byte[] welcomeBytes = Encoding.UTF8.GetBytes(message);
                byte[] bytes = new byte[1024];

                int i = client.Send(welcomeBytes);
                Console.WriteLine("Sent {0} bytes, message sent: '{1}'", i, message);
                i = client.Receive(bytes);
                Console.WriteLine(Encoding.UTF8.GetString(bytes));

                Thread.Sleep(100);
            }
        }
    }
}
