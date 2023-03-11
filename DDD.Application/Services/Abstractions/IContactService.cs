using DDD.Utilities.DTOs.Contact;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Application.Services.Abstractions
{
    public interface IContactService
    {
        public Task<IEnumerable<ContactDto>> GetContacts();
        public Task<ContactDto> CreateContact(NewContactDto newContact);
        public Task<ContactDto> UpdateContact(ContactDto updatedContact);
        public Task<ContactDto> DeleteContact(int contactId);
    }
}
