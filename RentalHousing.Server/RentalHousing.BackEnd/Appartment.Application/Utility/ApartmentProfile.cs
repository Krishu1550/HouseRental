using Appartment.Application.DTOs;
using Appartment.Domain.Entities;
using AutoMapper;

namespace Appartment.Application.Utility
{
    public class ApartmentProfile:Profile
    {

        public ApartmentProfile()
        {
            CreateMap<Apartment, ApartmentDto>().ReverseMap();
            CreateMap<CreateApartmentDto, Apartment>();
            CreateMap<UpdateApartmentDto, Apartment>();

            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<RequestAppartment, RequestApartmentDto>().ReverseMap();
            CreateMap<CreateRequestApartmentDto, RequestAppartment>();
            CreateMap<UpdateRequestApartmentDto, RequestAppartment>();
        }

    }
}
