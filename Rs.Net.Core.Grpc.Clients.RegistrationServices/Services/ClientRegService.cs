using Grpc.Core;
using Rs.Net.Core;
using Rs.Net.Core.Grpc.Clients.Dtos;
using Rs.Net.Core.Grpc.Clients.Models;
using Rs.Net.Core.Grpc.Clients.RegistrationServices.Models;

namespace Rs.Net.Core.Grpc.Clients.RegistrationServices.Services
{
    public class ClientRegService : ClientRegistrater.ClientRegistraterBase
    {
        private readonly ILogger<ClientRegService> _logger;
        private readonly List<Models.User> _users;

        public ClientRegService(ILogger<ClientRegService> logger)
        {
            _logger = logger;
            _users = new List<Models.User>();
        }

        public async override Task<ResponseMessage> Rego(Clients.Models.User request, ServerCallContext context)
        {
            // _logger.Log(LogLevel.Information, "Context", context.Host, context.Peer);
            context.RequestHeaders.ToList().ForEach(x => Console.WriteLine(x));
            var user = request.ToModel();
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }

            return await Task.Run(() => new ResponseMessage { Code = context.Status.StatusCode.ToString(), Messages = "Successful" });
        }
    }
}
