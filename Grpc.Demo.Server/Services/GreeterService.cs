namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Greeter;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class GreeterService : Definition.Greeter.GreeterService.GreeterServiceBase
    {
        private readonly ILogger<GreeterService> logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            this.logger = logger;
        }

        public override Task<GreetResponse> Greet(GreetRequest request, ServerCallContext context)
        {
            logger.LogInformation($"Received greet request with name {request.Name}. Sending back response.");

            return Task.FromResult(new GreetResponse
            {
                Greeting = $"Hello {request.Name}!",
            });
        }
    }
}
