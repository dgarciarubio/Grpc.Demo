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

