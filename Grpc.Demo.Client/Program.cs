namespace Grpc.Demo.Client
{
    using System.Threading.Tasks;

    static partial class Program
    {
        static async Task Main(string[] args)
        {
            await KeepGreeting();
        }
    }
}
