using System;
using NetMQ;
using NetMQ.Sockets;

namespace Response
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new ResponseSocket(">tcp://localhost:5557");
            // Receive the message from the server socket
            while (true)
            {
                string m1 = server.ReceiveFrameString();
                Console.WriteLine("From Client: {0}", m1);
                // Send a response back from the server
                server.SendFrame($"Hi Back : {m1}");
            }
        }
    }
}