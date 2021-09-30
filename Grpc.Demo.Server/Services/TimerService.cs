namespace Grpc.Demo.Server.Services
{
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Grpc.Demo.Definition.Timer;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class TimerService : Definition.Timer.TimerService.TimerServiceBase
    {
        private readonly ILogger<TimerService> logger;

        public TimerService(ILogger<TimerService> logger)
        {
            this.logger = logger;
        }

        public override async Task StreamTime(StreamTimeRequest request, IServerStreamWriter<StreamTimeResponse> responseStream, ServerCallContext context)
        {
            logger.LogInformation($"Received stream time request with delay {request.Delay}.");

            while (!context.CancellationToken.IsCancellationRequested)
            {
                logger.LogInformation($"Streaming current time to client.");
                await responseStream.WriteAsync(new StreamTimeResponse
                {
                    Time = Timestamp.FromDateTime(DateTime.UtcNow),
                });
                await Task.Delay(Convert.ToInt32(request.Delay.ToTimeSpan().TotalMilliseconds));
            }

            logger.LogInformation($"Finished streaming time.");
        }
    }
}
