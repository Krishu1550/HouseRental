namespace Appartment.Application.DTOs
{
        public class ApartmentDto
        {
            public int Id { get; set; }
            public string OwnerId { get; set; } = string.Empty;
            public AddressDto Address { get; set; } = new();
            public decimal Price { get; set; }
            public int Bedrooms { get; set; }
            public string Description { get; set; } = string.Empty;
        }
}
