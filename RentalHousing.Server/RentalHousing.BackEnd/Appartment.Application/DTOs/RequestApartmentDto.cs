namespace Appartment.Application.DTOs
{
    public class RequestApartmentDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
