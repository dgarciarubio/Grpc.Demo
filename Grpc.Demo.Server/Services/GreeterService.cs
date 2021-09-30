namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Greeter;
    using System.Threading.Tasks;

    public class GreeterService : Definition.Greeter.GreeterService.GreeterServiceBase
    {
        public override Task<GreetResponse> Greet(GreetRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GreetResponse
            {
                Greeting = $"Hello {request.Name}",
            });
        }
    }
}
