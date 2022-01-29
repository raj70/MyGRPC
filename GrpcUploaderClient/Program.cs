// See https://aka.ms/new-console-template for more information

using Google.Protobuf;
using Grpc.Net.Client;
using Rs.Net.Core.MyGRPC;

const int ChunkSize = 1024 * 32; // 32 KB

using var channel = GrpcChannel.ForAddress("https://localhost:7240");

var client = new Uploader.UploaderClient(channel);

Console.WriteLine("Starting call");
var call = client.UploadFile();

Console.WriteLine("Sending file metadata");
await call.RequestStream.WriteAsync(new Rs.Net.Core.MyGRPC.FileStream
{
    Metadata = new FileMetadata
    {
        FileName = "argenti_log.png"
    }
});

var buffer = new byte[ChunkSize];
await using var readStream = File.OpenRead("argenti_log.png");

while (true)
{
    var count = await readStream.ReadAsync(buffer);
    if (count == 0)
    {
        break;
    }

    Console.WriteLine("Sending file data chunk of length " + count);
    await call.RequestStream.WriteAsync(new Rs.Net.Core.MyGRPC.FileStream
    {
        Data = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, count))
    });
}

Console.WriteLine("Complete request");
await call.RequestStream.CompleteAsync();

var response = await call;
Console.WriteLine("Upload id: " + response.Id);

Console.WriteLine("Shutting down");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

