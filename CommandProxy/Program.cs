﻿using System;
using NetMQ;
using NetMQ.Sockets;

namespace Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new PullSocket("@tcp://127.0.0.1:5678");
            var worker = new PushSocket("@tcp://127.0.0.1:1234");
            Console.WriteLine("Intermediary started, and waiting for messages");
            var proxy = new Proxy(client, worker);
            proxy.Start();
        }
    }
}