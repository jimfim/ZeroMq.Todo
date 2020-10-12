using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;

namespace TodoList.App.CommandProxy
{
    public class ProxyService : BackgroundService
    {
        private readonly IConfiguration _configuration;

        public ProxyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await new TaskFactory().StartNew(StartProxy, cancellationToken);
        }

        private void StartProxy()
        {
            var client = new PullSocket(_configuration["PullSocket"]); //"@tcp://127.0.0.1:5678"
            var worker = new PushSocket(_configuration["PushSocket"]); //"@tcp://127.0.0.1:1234"
            Console.WriteLine("Intermediary started, and waiting for messages");
            var proxy = new Proxy(client, worker);
            proxy.Start();
        }
    }
}