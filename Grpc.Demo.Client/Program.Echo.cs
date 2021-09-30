namespace Grpc.Demo.Client
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Echo;
    using Grpc.Net.Client;
    using System;
    using System.Threading.Tasks;

    static partial class Program
    {
        static async Task KeepGreeting()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new EchoService.EchoServiceClient(channel);

                using (var streamingCall = client.Echo())
                {
                    var readTask = Task.Run(async () =>
                    {
                        await foreach (var response in streamingCall.ResponseStream.ReadAllAsync())
                        {
                            Console.WriteLine(response.Message);
                        }
                    });

                    Console.WriteLine("Type any messages and press enter to receive an echo.");
                    while (true)
                    {
                        string message = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(message))
                        {
                            break;
                        }
                        await streamingCall.RequestStream.WriteAsync(new EchoRequest
                        {
                            Message = message,
                        });
                    }

                    await streamingCall.RequestStream.CompleteAsync();
                    await readTask;
                }
            }
        }
    }
}
