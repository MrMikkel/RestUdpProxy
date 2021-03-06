using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace UdpProxy
{
    class Program
    {
        private static string URL = "https://restexcuses.azurewebsites.net/api/Movements";

        //public static async void Post(HttpContent post)
        //{
        //    HttpClient http = new HttpClient();
        //    await http.PostAsync(URL, post);
        //}
        static async Task Main(string[] args)
        {
            Console.WriteLine("UDP Listener");
            using (UdpClient socket = new UdpClient())
            {
                socket.Client.Bind(new IPEndPoint(IPAddress.Any, 5005));


                using (HttpClient client = new HttpClient())
                {
                    DateTime timestamp = DateTime.MinValue;

                    while (true)
                    {
                        IPEndPoint from = null;

                        byte[] data = socket.Receive(ref from);
                        Console.WriteLine((DateTime.Now - timestamp).TotalMilliseconds);
                        if ((DateTime.Now-timestamp).TotalMilliseconds>2000)
                        {
                            string stringData = Encoding.UTF8.GetString(data);

                            Movement movement = new Movement(stringData, DateTime.UtcNow);
                            Console.WriteLine("Server received: " + stringData + " From " + from.Address);

                            //HttpContent content = new StringContent(stringData, Encoding.UTF8, "application/json");

                            /*HttpResponseMessage response = */
                            await client.PostAsJsonAsync(URL, movement);

                            timestamp = DateTime.Now;
                            //Console.WriteLine(response.Content.ReadAsStringAsync().Result.Length);
                            //Post(content);
                            //client.PostAsync(URL, content);
                            //Thread.Sleep(1000);
                        }


                    }
                }

            }
        }
    }
}
