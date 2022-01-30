namespace Rs.Net.Core.Grpc.Clients.RegistrationServices.Models
{
    public class Address
    {
        public Address()
        {

        }
        public Guid AddressId { get; set; } = Guid.NewGuid();
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public string Country { get; set; }
    }
}
