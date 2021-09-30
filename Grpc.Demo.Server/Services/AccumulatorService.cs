namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Average;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class AccumulatorService : AverageService.AverageServiceBase
    {
        public override async Task<CalculateAverageResponse> CalculateAverage(IAsyncStreamReader<CalculateAverageRequest> requestStream, ServerCallContext context)
        {
            var numbers = new List<double>();
            await foreach (var request in requestStream.ReadAllAsync())
            {
                numbers.Add(request.Number);
            }
            return new CalculateAverageResponse
            {
                Average = numbers.Average(),
            };
        }
    }
}
