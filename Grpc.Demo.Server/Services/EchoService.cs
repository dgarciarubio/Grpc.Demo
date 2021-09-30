namespace Grpc.Demo.Server.Services
{
    using Grpc.Core;
    using Grpc.Demo.Definition.Echo;
    using System.Threading.Tasks;

    public class EchoService : Definition.Echo.EchoService.EchoServiceBase
    {
        public override async Task Echo(IAsyncStreamReader<EchoRequest> requestStream, IServerStreamWriter<EchoResponse> responseStream, ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(new EchoResponse
                {
                    Message = request.Message,
                });
            }
        }
    }
}
