namespace Grpc.Demo.Client
{
    using System;
    using System.Threading.Tasks;

    static partial class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Demonstrating non-streaming gRPC:");
            await Greet();

            Console.WriteLine();
            Console.WriteLine("Demonstrating server-side streaming gRPC:");
            await GetTime();

            Console.WriteLine();
            Console.WriteLine("Demonstrating client-side streaming gRPC:");
            await CalculateAverage();

            Console.WriteLine();
            Console.WriteLine("Demonstrating bi-directional streaming gRPC:");
            await Echo();
        }
    }
}
