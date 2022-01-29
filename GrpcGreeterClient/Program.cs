// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7240");
var client = new Rs.Net.Core.MyGRPC.Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(new Rs.Net.Core.MyGRPC.HelloRequest
{
    Name = "Rajen"
});

Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
