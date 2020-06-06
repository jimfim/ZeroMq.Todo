using System;
using System.Diagnostics;
using NetMQ;
using NetMQ.Sockets;

namespace Client
{
    internal class Program
    {
        public static void Main()
        {
            //'@' (to bind the socket) or '>' (to connect the socket).
            using var sender = new PushSocket(">tcp://127.0.0.1:5678");
            int count = 0;
            Console.WriteLine("Press enter when worker are ready");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (count < 1000000)
            {
                Console.Read();
                count++;
                Console.Out.WriteLineAsync($"Sending {count}");
                sender.SendFrame(count.ToString());
            }
        }
    }
}