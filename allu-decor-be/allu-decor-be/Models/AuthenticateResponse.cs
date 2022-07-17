namespace allu_decor_be.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Address = user.Address;
            District = user.District;
            City = user.City;
            Email = user.Email;
            Phone = user.Phone;
            Role = user.Role;
            Token = token;
        }
    }
}
