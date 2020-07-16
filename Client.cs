using System;
using System.Net;
using System.Net.Sockets;

namespace socketclient
{
    class CLIENT_SIDE
    {
        static Socket client;
        public static void Main(string[] args)
        {
            Console.WriteLine("CLIENT TERMINAL");
            string host = Dns.GetHostName();

            IPAddress address = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, 1234);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(endPoint);
        }
    }
}
