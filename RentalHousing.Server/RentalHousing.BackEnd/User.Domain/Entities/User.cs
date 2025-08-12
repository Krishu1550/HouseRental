namespace User.Domain.Entities
{
    public class User
    {

        public Guid Id { get; set; }
        public string UserName { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<string> Roles { get; set; } = new List<string>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Address> Addresses { get; set; } = new List<Address>();


    }
}
