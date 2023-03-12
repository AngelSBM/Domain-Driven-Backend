using DDD.Utilities.DTOs.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Application.Services.Abstractions
{
    public interface IContactService
    {
        public Task<ContactDto> GetContactById(int contactId);
        public Task<IEnumerable<ContactDto>> GetContacts();
        public Task<ContactDto> CreateContact(NewContactDto newContact);
        public Task<ContactDto> UpdateContact(UpdateContactDTO updatedContact);
        public Task<object> DeleteContact(int contactId);
        public Task<object> DeleteAddress(int addressId);
    }
}
