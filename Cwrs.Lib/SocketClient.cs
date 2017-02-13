using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Configuration;

namespace Cwrs.Lib.SocketClient
{
    public class Client
    {
        public Client()
        {

        }

        public static string Connect(string serverIP,int port, string message)
        {
            string output = "";

            try
            {
                // Create a TcpClient. 
                // The client requires a TcpServer that is connected 
                // to the same address specified by the server and port 
                // combination.
                TcpClient client = new TcpClient(serverIP, port);

                // Translate the passed message into ASCII and store it as a byte array.
                Byte[] data = new Byte[256];
                data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing. 
                // Stream stream = client.GetStream();
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                output = "Sent: " + message;

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                output = responseData;

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                output = "ArgumentNullException: " + e;
            }
            catch (SocketException e)
            {
                output = "SocketException: " + e.ToString();
            }

            return output;
        }
    }
}