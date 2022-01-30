namespace Rs.Net.Core.Grpc.Clients.RegistrationServices.Models
{
    public class User
    {
        public User()
        {
            Address = new Address();
        }
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }

    }

    public static class UserExtension
    {
        public static Models.User ToModel(this Clients.Models.User user)
        {
            return new Models.User
            {
                DateOfBirth = user.DateOfBirth.ToDateTime(),
                Address = new Models.Address
                {
                    AddressLine1 = user.Address.AddressLine1,
                    AddressLine2 = user.Address.AddressLine2,
                    Country = user.Address.Country,
                    PostCode = user.Address.PostCode,
                    State = user.Address.State
                },
                EmailAddress = user.EmailAddress,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                FirstName = user.FirstName 
            };
        }
    }
}
