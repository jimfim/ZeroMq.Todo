using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;

namespace Response
{
    public class QueryListener : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var server = new ResponseSocket(">tcp://localhost:5557");
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                await Task.Factory.StartNew(() =>
                {
                    var m1 = server.ReceiveFrameString();
                    Console.WriteLine("From Client: {0}", m1);
                    server.SendFrame($"Hi Back : {m1}");
                }, cancellationToken);

            }
        }
    }
}