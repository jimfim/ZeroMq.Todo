using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace Server
{
    internal class Program
    {
        public static void Main()
        {
            using var receiver = new PullSocket(">tcp://127.0.0.1:1234");
            while (true)
            {
                string workload = receiver.ReceiveFrameString();
                Console.WriteLine($"Recieved {workload}");
            }
        }
    }
}