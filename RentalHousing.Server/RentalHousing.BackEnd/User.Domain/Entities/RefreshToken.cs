using System.ComponentModel.DataAnnotations.Schema;

namespace User.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public DateTime? RevokedAt { get; set; }


        [NotMapped]
        public bool IsActive => DateTime.UtcNow < ExpiresAt;

        [NotMapped]
        public bool IsRevoked => RevokedAt.HasValue && RevokedAt.Value < DateTime.UtcNow;

    }
}
