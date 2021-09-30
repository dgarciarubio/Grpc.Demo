namespace Grpc.Demo.Client
{
    using Grpc.Demo.Definition.Greeter;
    using Grpc.Net.Client;
    using System;
    using System.Threading.Tasks;

    static partial class Program
    {
        private static async Task Greet()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new GreeterService.GreeterServiceClient(channel);

                Console.Write("Tell me your name: ");
                string name = Console.ReadLine();
                
                var response = await client.GreetAsync(new GreetRequest
                {
                    Name = name,
                });

                Console.WriteLine(response.Greeting);
            }
        }
    }
}
