using AutoMapper;
using User.Application.DTOs;
using User.Domain.Entities;

namespace User.Application.ProfileMapper
{
    public class UserProfile: Profile
    {

        public UserProfile() 
        { 
            CreateMap<Address,AddressDto>().ReverseMap();

        }    
    }
}
