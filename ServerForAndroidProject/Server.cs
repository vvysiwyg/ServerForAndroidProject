using ServerForAndroidProject.data;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ServerForAndroidProject
{
    class Server
    {
        private TcpListener listener;
        private PharmacyNetworkList pnl;
        private string pnlJSON;
        private string filePath = "info.txt";

        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public async Task startServerAsync()
        {
            pnlJSON = readFile(filePath);
            pnl = JsonSerializer.Deserialize<PharmacyNetworkList>(pnlJSON);

            try
            {
                listener.Start();

                while (true)
                {
                    var tcpClient = await listener.AcceptTcpClientAsync();

                    await Task.Run(async () => await ClientHandler(tcpClient));
                }
            }
            catch (InvalidOperationException se)
            {
                Console.WriteLine(se.Message);
            }
            finally
            {
                listener.Stop();
            }
        }

        async Task ClientHandler(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();
            var response = new List<byte>();
            int bytesRead = -2;
            while (true)
            {
                while ((bytesRead = stream.ReadByte()) != '\n' && stream.DataAvailable)
                {
                    response.Add((byte)bytesRead);
                }

                if (response.Count == 0)
                    break;

                var message = Encoding.UTF8.GetString(response.ToArray());

                if (message != null)
                {
                    Console.WriteLine($"Got: {message}");

                    if (message.Equals("*"))
                    {
                        await stream.WriteAsync(Encoding.UTF8.GetBytes(pnlJSON + "\n"));
                    }

                    string firstString = message.Substring(0, 1);
                    string secondString = "";

                    if (message.Length != 1)
                        secondString = message.Substring(1, 1);

                    if (firstString.Equals("d"))
                    {
                        if (secondString.Equals("0"))
                        {
                            string id = message.Substring(3);
                            pnl.deletePharmacyNetwork(new Guid(id));
                        }
                        else if (secondString.Equals("1"))
                        {
                            string[] id = message.Substring(3).Split('&');
                            pnl.deletePharmacy(new Guid(id[0]), new Guid(id[1]));
                        }
                        pnlJSON = JsonSerializer.Serialize(pnl);
                        writeFile(filePath, pnlJSON);
                    }
                    else if (firstString.Equals("e"))
                    {
                        if (secondString.Equals("0"))
                        {
                            string pnJSON = message.Substring(3);
                            PharmacyNetwork tempPn = JsonSerializer.Deserialize<PharmacyNetwork>(pnJSON);
                            pnl.updatePharmacyNetwork(tempPn);
                        }
                        else if (secondString.Equals("1"))
                        {
                            string[] jsonElems = message.Substring(3).Split('&');
                            Pharmacy tempP = JsonSerializer.Deserialize<Pharmacy>(jsonElems[1]);
                            pnl.updatePharmacy(new Guid(jsonElems[0]), tempP);
                        }
                        pnlJSON = JsonSerializer.Serialize(pnl);
                        writeFile(filePath, pnlJSON);
                    }
                    else if (firstString.Equals("a"))
                    {
                        if (secondString.Equals("0"))
                        {
                            string pnJSON = message.Substring(3);
                            PharmacyNetwork tempPn = JsonSerializer.Deserialize<PharmacyNetwork>(pnJSON);
                            pnl.addPharmacyNetwork(tempPn);
                        }
                        else if (secondString.Equals("1"))
                        {
                            string[] jsonElems = message.Substring(3).Split('&');
                            Pharmacy tempP = JsonSerializer.Deserialize<Pharmacy>(jsonElems[1]);
                            pnl.addPharmacy(new Guid(jsonElems[0]), tempP);
                        }
                        pnlJSON = JsonSerializer.Serialize(pnl);
                        writeFile(filePath, pnlJSON);
                    }
                }
                response.Clear();
            }
            tcpClient.Close();
            Console.WriteLine("Connection is closed.");
        }

        public static string readFile(string path)
        {
            byte[] encoded = File.ReadAllBytes(path);
            return Encoding.UTF8.GetString(encoded);
        }

        public static void writeFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path, text);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ~Server()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }

    }
}
