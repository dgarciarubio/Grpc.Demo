namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Echo;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class EchoService : Definition.Echo.EchoService.EchoServiceBase
    {
        private readonly ILogger<EchoService> logger;

        public EchoService(ILogger<EchoService> logger)
        {
            this.logger = logger;
        }

        public override async Task Echo(IAsyncStreamReader<EchoRequest> requestStream, IServerStreamWriter<EchoResponse> responseStream, ServerCallContext context)
        {
            logger.LogInformation($"Initiated echo process. Waiting for numbers from client.");

            await foreach (var request in requestStream.ReadAllAsync())
            {
                logger.LogInformation($"Received echo request with message {request.Message} from a streaming client. Sending back echo.");
                await responseStream.WriteAsync(new EchoResponse
                {
                    Message = request.Message,
                });
            }

            logger.LogInformation($"Client finished streaming messages.");
        }
    }
}
