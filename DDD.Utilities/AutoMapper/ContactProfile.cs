using AutoMapper;
using DDD.Domain.Entities;
using DDD.Utilities.DTOs.Contact;
using DDD.Utilities.DTOs.ContactDependents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Utilities.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile() 
        {
            CreateMap<Contact, ContactDto>()
                .ReverseMap();

            CreateMap<Contact, ContactDto>()
                .ForMember(x => x.Addresses, options => options.MapFrom(MapAddresses));
        }

        public IEnumerable<AddressDto> MapAddresses(Contact contact, ContactDto contactDto)
        {
            List<AddressDto> result = new List<AddressDto>();
            foreach (var address in contact.Addresses)
            {
                result.Add(new AddressDto
                {
                    Id = address.Id,
                    AddressLine = address.AddressLine          
                });
            }

            return result;

        }
        
    }
}
