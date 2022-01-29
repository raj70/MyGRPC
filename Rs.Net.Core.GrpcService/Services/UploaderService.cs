using Grpc.Core;
using Rs.Net.Core.MyGRPC;

namespace Rs.Net.Core.GrpcService.Services
{
    public class UploaderService : Uploader.UploaderBase
    {
        private readonly ILogger<UploaderService> logger;
        private readonly IConfiguration configuration;

        public UploaderService(ILogger<UploaderService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }
        public override async Task<FileId> UploadFile(IAsyncStreamReader<MyGRPC.FileStream> requestStream, ServerCallContext context)
        {
            var fileId = Guid.NewGuid();
            var uploadPath = Path.Combine(configuration["StoredFilesPath"], fileId.ToString());

            Directory.CreateDirectory(uploadPath);

            await using var writeStream = File.Create(Path.Combine(uploadPath, "data.bin"));
            await foreach(var message in requestStream.ReadAllAsync())
            {
                if(message.Metadata != null)
                {
                    await File.WriteAllTextAsync(Path.Combine(uploadPath, "metadata.json"), message.Metadata.ToString());
                }
                if (message.Data != null)
                {
                    await writeStream.WriteAsync(message.Data.Memory);
                }
            }

            return new FileId { Id = fileId.ToString()};
        }
    }
}
