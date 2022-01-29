using Grpc.Core;
using Rs.Net.Core.MyGRPC;

namespace Rs.Net.Core.GrpcService.Services
{
    public class DownloadService : Downloader.DownloaderBase
    {
        private readonly ILogger<UploaderService> logger;
        private readonly IConfiguration configuration;

        public DownloadService(ILogger<UploaderService> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public override async Task DownloadFile(FileId request, IServerStreamWriter<MyGRPC.FileStream> responseStream, ServerCallContext context)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var fileId = request.Id;

            // does not matter for this example
            if (fileId == null)
            {
                throw new ArgumentNullException(nameof(request.Id));
            }

            var filesBytes = await File.ReadAllBytesAsync("./uploads/argenti_logo.png");

            foreach (var fileByte in filesBytes)
            {
                await responseStream.WriteAsync(new MyGRPC.FileStream { Data = Google.Protobuf.ByteString.CopyFrom(fileByte) });
            }

        }
    }
}
