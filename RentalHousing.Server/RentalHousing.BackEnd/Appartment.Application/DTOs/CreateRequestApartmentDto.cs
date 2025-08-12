namespace Appartment.Application.DTOs
{
    public class CreateRequestApartmentDto
    {
        public int ApartmentId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
