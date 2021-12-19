using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Creation;
using Entities.DataTransferObjects.Update;
using Entities.Models;

namespace Entities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, ContactCreationDto>().ReverseMap();
            CreateMap<Contact, ContactUpdateDto>().ReverseMap();

            CreateMap<TelephoneNumber, TelephoneNumberDto>().ReverseMap();
            CreateMap<TelephoneNumber, TelephoneNumberCreationDto>().ReverseMap();
            CreateMap<TelephoneNumber, TelephoneNumberUpdateDto>().ReverseMap();
        }
    }
}