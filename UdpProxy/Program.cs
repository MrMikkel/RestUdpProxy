using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Diagnostics;

namespace UdpProxy
{
    class Program
    {
        private static string URL = "https://restexcuses.azurewebsites.net/api/Excuse";

        static void Main(string[] args)
        {
            Console.WriteLine("UDP Listener");
            using (UdpClient socket = new UdpClient())
            {
                socket.Client.Bind(new IPEndPoint(IPAddress.Any, 5005));


                using (HttpClient client = new HttpClient())
                {
                    while (true)
                    {
                        IPEndPoint from = null;

                        byte[] data = socket.Receive(ref from);
                        //string timeStamp = GetTimestamp(DateTime.UtcNow);
                        //DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;

                        string stringData = Encoding.UTF8.GetString(data);

                        Console.WriteLine("Server received: " + stringData + " From " + from.Address);

                        HttpContent content = new StringContent(stringData, Encoding.UTF8, "application/json");

                        client.PostAsync(URL, content);
                    }
                }

            }
        }
    }
}
