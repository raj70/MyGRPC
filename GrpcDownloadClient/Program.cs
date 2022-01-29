// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("https://localhost:7240");
var client = new Rs.Net.Core.MyGRPC.Downloader.DownloaderClient(channel);

var call = client.DownloadFile(new Rs.Net.Core.MyGRPC.FileId { Id = "fake_id" });

var fileDir = AppDomain.CurrentDomain.BaseDirectory + "/Files";
Directory.CreateDirectory(fileDir);

using (var writeStream = File.Create(Path.Combine(fileDir, "argenti_logo.png"))){

    // https://grpc.io/docs/languages/csharp/basics/#streaming-rpcs
    while (await call.ResponseStream.MoveNext())
    {
        var fileStream = call.ResponseStream.Current;
        // https://developers.google.com/protocol-buffers/docs/reference/csharp/class/google/protobuf/byte-string
        await writeStream.WriteAsync(fileStream.Data.ToByteArray());
    }
}
Console.WriteLine("Shutting down");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();

