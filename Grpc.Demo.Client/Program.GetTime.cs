namespace Grpc.Demo.Client
{
    using Google.Protobuf.WellKnownTypes;
    using Grpc.Core;
    using Grpc.Demo.Definition.Timer;
    using Grpc.Net.Client;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    static partial class Program
    {
        static async Task GetTime()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new TimerService.TimerServiceClient(channel);

                Console.Write("Specify delay in seconds: ");
                var delay = TimeSpan.FromSeconds(int.Parse(Console.ReadLine()));
                Console.Write("Specify timeout in seconds: ");
                var timeout = TimeSpan.FromSeconds(int.Parse(Console.ReadLine()));
                var cts = new CancellationTokenSource(timeout);

                using (var streamingCall = client.StreamTime(new StreamTimeRequest { Delay = Duration.FromTimeSpan(delay), }, cancellationToken: cts.Token))
                {
                    try
                    {
                        await foreach (var response in streamingCall.ResponseStream.ReadAllAsync(cts.Token))
                        {
                            Console.WriteLine($"It is now {response.Time}");
                        }
                    }
                    catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
                    {
                    }
                }
            }
        }
    }
}
