using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NetMQ;
using NetMQ.Sockets;

namespace QueryProxy
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
            var query = new PullSocket(_configuration["PullSocket"]);
            var response = new PushSocket(_configuration["PushSocket"]);

            Console.WriteLine("Intermediary started, and waiting for messages");
            var proxy = new Proxy(query, response);
            proxy.Start();
        }
    }
}