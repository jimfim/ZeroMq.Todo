using System;
using NetMQ;
using NetMQ.Sockets;

namespace QueryProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var query = new ResponseSocket("@tcp://localhost:5556");
            var response = new RequestSocket("@tcp://localhost:5557");
            Console.WriteLine("Intermediary started, and waiting for messages");
            var proxy = new Proxy(query, response);
            proxy.Start();
        }
    }
}