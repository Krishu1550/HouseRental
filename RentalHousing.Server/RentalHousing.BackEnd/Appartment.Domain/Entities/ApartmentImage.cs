namespace Appartment.Domain.Entities
{
    public class ApartmentImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        public bool IsPrimary { get; set; }  
    }
}
