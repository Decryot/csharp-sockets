using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace socketclient
{
    class CLIENT_SIDE
    {
        static Socket client;
        static string past;
        public static void Main(string[] args)
        {
            Console.WriteLine("[CLIENT TERMINAL]");
            Console.WriteLine(" ");
            connectToServer();

            AppDomain.CurrentDomain.ProcessExit += Exit;
        }

        static void connectToServer() {
            string host = Dns.GetHostName();

            IPAddress address = Dns.GetHostEntry(host).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(address, 1234);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try {
                IPEndPoint localEndPoint;
                client.Connect(endPoint);
                localEndPoint = client.RemoteEndPoint as IPEndPoint;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("successfully connected to the server");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[IP: {0} PORT: {1}]", localEndPoint.Address, localEndPoint.Port);
                Console.WriteLine(" ");

                byte[] bytes = new byte[256];

                int a = client.Receive(bytes);
                if (Encoding.UTF8.GetString(bytes) != past) {
                    Console.WriteLine("SERVER MESSAGE: '{0}'", Encoding.UTF8.GetString(bytes));
                    past = Encoding.UTF8.GetString(bytes);
                }
                Console.WriteLine(" ");
            }
            catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("error found while connecting to '{0}': retrying in 5 seconds...",address);
                Thread.Sleep(5000);
                connectToServer();
            }
        }

        static void Exit(object sender, EventArgs e) {
            client.Disconnect(true);
        }
    }
}