using AutoMapper;
using DDD.Application.Services.Abstractions;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Utilities.DTOs.Contact;
using DDD.Utilities.DTOs.ContactDependents;
using DDD.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DDD.Application.Services.Root
{
    public class ContactService : IContactService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IUnitOfWork unitOfWork,
                                IMapper mapper,
                                ILogger<ContactService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ContactDto> GetContactById(int contactId)
        {
            var contactDB = await _unitOfWork.ContactRepo.GetContactById(contactId);
            if (contactDB == null)
                throw new NotFoundException("Contact not found");

            return _mapper.Map<ContactDto>(contactDB);
        }

        public async Task<ContactDto> CreateContact(NewContactDto newContact)
        {
            try
            {

                await _unitOfWork.BeginTransaction();

                var contactDB = new Contact();
                contactDB.Name = newContact.Name;
                contactDB.Lastname = newContact.Lastname;
                contactDB.Email = newContact.Email;

                await _unitOfWork.ContactRepo.Add(contactDB);

                await _unitOfWork.SaveChanges();

                foreach (NewAddressDto newAdress in newContact.NewAddresses)
                {
                    contactDB.AddAddressInPuntaCana(new Address()
                    {
                        AddressLine = newAdress.AddressLine,
                        ContactId = contactDB.Id
                    });
                    await _unitOfWork.SaveChanges();
                }


                await _unitOfWork.CommitTransaction();
                return _mapper.Map<ContactDto>(contactDB);


            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }


        public async Task<IEnumerable<ContactDto>> GetContacts()
        {
            try
            {
                var contacts = await _unitOfWork.ContactRepo.GetContactsWithAddresses();

                return _mapper.Map<List<ContactDto>>(contacts);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task<ContactDto> UpdateContact(UpdateContactDTO updatedContact)
        {
            try
            {

                var contactDB = await _unitOfWork.ContactRepo.GetContactById(updatedContact.Id);
                if (contactDB == null)
                    ExceptionFactory("Provided contact not found.");

                await _unitOfWork.BeginTransaction();

                contactDB.Name = updatedContact.Name;
                contactDB.Lastname = updatedContact.Lastname;
                contactDB.Email = updatedContact.Email;



                foreach (var updatedAddress in updatedContact.Addresses)
                {
                    if (updatedAddress.Id == null)
                    {
                        contactDB.AddAddressInPuntaCana(new Address() { 
                            AddressLine = updatedAddress.AddressLine,
                            ContactId = contactDB.Id
                        });
                        break;
                    }

                    var addressDB = await _unitOfWork.AddressRepo.GetById((int)updatedAddress.Id);
                    contactDB.ChangeOfAddressMustBeInPuntaCana(addressDB, updatedAddress.AddressLine);

                }


                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitTransaction();

                return _mapper.Map<ContactDto>(contactDB);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                await _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }

        }


        public async Task<object> DeleteContact(int contactId)
        {
            try
            {
                var contactDB = await _unitOfWork.ContactRepo.GetContactById(contactId);
                if (contactDB == null)
                    ExceptionFactory("Contact not found");


                _unitOfWork.ContactRepo.Remove(contactDB);
                await _unitOfWork.SaveChanges();
                return new { message = "Contact deleted." };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw new Exception("Something went wrong deleting the contact, please contact IT.");
            }
        }


        public async Task<object> DeleteAddress(int addressId)
        {
            var addressDB = await _unitOfWork.AddressRepo.GetById(addressId);
            if (addressDB == null)
                ExceptionFactory("Address not found");

            _unitOfWork.AddressRepo.Remove(addressDB);
            await _unitOfWork.SaveChanges();
            return new { message = "Address deleted." };
        }



        public Exception ExceptionFactory(string message)
        {
            message = message == "" ? "There was an error, please contact IT department": message;
            throw new Exception(message);
        }
    }
}
