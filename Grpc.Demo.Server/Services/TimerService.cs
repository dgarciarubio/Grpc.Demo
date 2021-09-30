namespace Grpc.Demo.Server.Services
{
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Grpc.Demo.Definition.Timer;
    using System;
    using System.Threading.Tasks;

    public class TimerService : Definition.Timer.TimerService.TimerServiceBase
    {
        public override async Task StreamTime(StreamTimeRequest request, IServerStreamWriter<StreamTimeResponse> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new StreamTimeResponse
                {
                    Time = Timestamp.FromDateTime(DateTime.UtcNow),
                });
                await Task.Delay(Convert.ToInt32(request.Delay.ToTimeSpan().TotalMilliseconds));
            }
        }
    }
}
