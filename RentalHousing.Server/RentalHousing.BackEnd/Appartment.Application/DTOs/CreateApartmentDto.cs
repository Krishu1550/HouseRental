using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appartment.Application.DTOs
{
    public class CreateApartmentDto
    {
        public string OwnerId { get; set; } = string.Empty;
        public AddressDto Address { get; set; } = new();
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
