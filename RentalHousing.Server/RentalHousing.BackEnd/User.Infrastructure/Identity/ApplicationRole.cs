using Microsoft.AspNetCore.Identity;

namespace User.Infrastructure.Identity
{
    public class ApplicationRole:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
