using System.Net.Sockets;
using System.Net;
using System.Text;

namespace test_server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                IPAddress ip = IPAddress.Any;
                int port = 8000;

                TcpListener server = new TcpListener(ip, port);
                server.Start();

                Console.WriteLine("Server started...");

                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Client connected...");

                NetworkStream stream = client.GetStream();

                while (true)
                {
                    byte[] message = new byte[1024];
                    int bytesRead = stream.Read(message, 0, message.Length);

                    if (bytesRead == 0)
                        break;

                    Console.WriteLine("Received: " + Encoding.ASCII.GetString(message, 0, bytesRead));

                    byte[] data = Encoding.ASCII.GetBytes("Hello from server");
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: Hello from server");
                }

                client.Close();
                server.Stop();
            }
        }
    }
}