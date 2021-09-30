# Grpc.Demo
Demostration of the usage of the gGRPC tooling in C#.

## Projects
- Grpc.Demo.Definition: dotnet library with proto files defining the services, procedures, and messages. Auto-generates C# code for clients and servers during build.
- Grpc.Demo.Client: dot net console app with demo clients.
- Grpc.Demo.Server: aspnet core app with demo services.

## Services:
- GreeterServcice: demonstration of a non-streaming gRPC.
- TimerService: demonstration of a server streaming gRPC.
- AverageService: demonstration of client streaminng gRPC.
- EchoService: demonstration of a bi-directional streaming gRPC.

## How to run:
- Must have [dotnet 3.1 sdk](https://dotnet.microsoft.com/download/dotnet/3.1) installed
- Both client and server can be run at the same time to test the communication. [Configure Visual Studio](https://docs.microsoft.com/en-us/visualstudio/ide/how-to-set-multiple-startup-projects?view=vs-2019) to launch both projects when pressing the Start button or F5.

## How to update the services:
- Update the proto files, or add a new one. 
  - If the proto file is new, make sure its referenced in the csproj as follows:
  ```
    <ItemGroup>
      <Protobuf Include="newfile.proto" />
    </ItemGroup>
  ```
- Update the server by either adding a new service or making changes in an existing one. 
  - If the changes made in the proto files are not reflected in the auto-generated code for the server, try building manually the Definition project to check for any possible sintax errors in the proto files.
  - If the service is new, add it to the Startup.cs file where the `UseEndpoints` configuration is done as follows:
  ```
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<Services.NewService>();
    });
  ```
  
- Update the client to make a new gRPC call by using a new client for a new service or updating an existing one.
  - If the changes made in the proto files are not reflected in the auto-generated code for the client, try building manually the Definition project to check for any possible sintax errors in the proto files.
