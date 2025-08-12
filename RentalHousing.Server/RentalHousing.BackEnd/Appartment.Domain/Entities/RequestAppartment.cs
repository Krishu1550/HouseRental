using System.ComponentModel.DataAnnotations.Schema;


namespace Appartment.Domain.Entities
{
    public class RequestAppartment
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to User
        public int ApartmentId { get; set; } // Foreign key to Apartment
        public DateTime RequestDate { get; set; }
        public RequestStatus Status { get; set; } // e.g., Pending, Approved, Rejected
        public string Message { get; set; }
        // Navigation properties
        [ForeignKey(nameof(ApartmentId))]
        public Apartment Apartment { get; set; }


    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
