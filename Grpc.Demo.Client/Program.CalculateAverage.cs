namespace Grpc.Demo.Client
{
    using Grpc.Demo.Definition.Average;
    using Grpc.Net.Client;
    using System;
    using System.Threading.Tasks;

    static partial class Program
    {
        static async Task CalculateAverage()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new AverageService.AverageServiceClient(channel);

                using (var streamingCall = client.CalculateAverage())
                {
                    while (true)
                    {
                        Console.Write("Write the next number to calculate average (non-number to finish): ");
                        string input = Console.ReadLine();
                        if (!int.TryParse(input, out int number))
                        {
                            break;
                        }
                        await streamingCall.RequestStream.WriteAsync(new CalculateAverageRequest
                        {
                            Number = number,
                        });
                    }

                    await streamingCall.RequestStream.CompleteAsync();
                    var response = await streamingCall;
                    Console.WriteLine($"The average is {response.Average}");
                }
            }
        }
    }
}
