// See https://aka.ms/new-console-template for more information

// 
using Grpc.Core;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("https://localhost:7065");
var client = new Rs.Net.Core.Grpc.Clients.ClientRegistrater.ClientRegistraterClient(channel);

var user = new Rs.Net.Core.Grpc.Clients.Models.User
{
    Address = new Rs.Net.Core.Grpc.Clients.Models.Address
    {
        AddressLine1 = "362 Geroge street",
        AddressLine2 = "Sydney",
        PostCode = 2000,
        Country = "Australia",
        State = "NSW"
    },
    DateOfBirth = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(new DateTime(1983, 07, 12, 0, 0, 0, DateTimeKind.Utc)),
    EmailAddress = "g@z.com",
    FirstName = "Michle",
    LastName = "Smith",

};

var metadata = new Metadata();
metadata.Add(new Metadata.Entry("requester", "Console Application"));
metadata.Add(new Metadata.Entry("developer", "Rajen Shrestha"));

var response = client.Rego(user, metadata);

Console.WriteLine("Response: " + response.Messages);
Console.WriteLine("Shutting down");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

Console.WriteLine("Hello, World!");
