namespace User.Application.DTOs
{
    public class AddressDto
    {

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        // Navigation property to User
        public Guid UserId { get; set; }
        //public User User { get; set; } // Assuming a User class exists in the same namespace
    }
}
