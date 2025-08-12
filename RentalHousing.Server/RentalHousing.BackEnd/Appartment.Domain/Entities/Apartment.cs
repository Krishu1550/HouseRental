namespace Appartment.Domain.Entities
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ApartmentType Type { get; set; } // Rent or Sale
        public Address Address { get; set; } 
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSqFt { get; set; }
        public string OwnerId { get; set; } = string.Empty;
        public List<ApartmentImage> Images { get; set; } = new();
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
    }

}
