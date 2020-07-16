using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace socketclient
{
    class CLIENT_SIDE
    {
        static Socket client;
        public static void Main(string[] args)
        {
            Console.WriteLine("[CLIENT TERMINAL]");
            string host = Dns.GetHostName();

            IPAddress address = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, 1234);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try {
                client.Connect(endPoint);
                IPEndPoint localEndPoint = client.RemoteEndPoint as IPEndPoint;
                byte[] bytes = new byte[256];

                int a = client.Receive(bytes);
                Console.WriteLine(Encoding.UTF8.GetString(bytes));

                Console.WriteLine("connected to the server");
                Console.WriteLine("[IP: " + localEndPoint.Address + " PORT: " + localEndPoint.Port + "]");


            }catch (Exception e) {
                Console.WriteLine("the server does not exist");
            }
        }
    }
}