namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Average;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class AverageService : Definition.Average.AverageService.AverageServiceBase
    {
        private readonly ILogger<AverageService> logger;

        public AverageService(ILogger<AverageService> logger)
        {
            this.logger = logger;
        }

        public override async Task<CalculateAverageResponse> CalculateAverage(IAsyncStreamReader<CalculateAverageRequest> requestStream, ServerCallContext context)
        {
            logger.LogInformation($"Initiated calculate average process. Waiting for numbers from client.");

            var numbers = new List<double>();
            await foreach (var request in requestStream.ReadAllAsync())
            {
                logger.LogInformation($"Received calculate average request with number {request.Number} from a streaming client.");
                numbers.Add(request.Number);
            }

            logger.LogInformation($"Client finished streaming numbers. Sending back average.");
            return new CalculateAverageResponse
            {
                Average = numbers.Any() ? numbers.Average() : double.NaN,
            };
        }
    }
}
