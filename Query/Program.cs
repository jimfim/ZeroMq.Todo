﻿using System;
using NetMQ;
using NetMQ.Sockets;

namespace Query
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RequestSocket(">tcp://localhost:5556");
            int count = 0;
            Console.WriteLine("Press enter to send message");
            while (true)
            {
                Console.Read();
                count++;
                Console.WriteLine("Sending to Server: {0}",count);
                client.SendFrame(count.ToString());
                // Receive the response from the client socket
                string m2 = client.ReceiveFrameString();
                Console.WriteLine("From Server: {0}",m2);
            }

        }
    }
}