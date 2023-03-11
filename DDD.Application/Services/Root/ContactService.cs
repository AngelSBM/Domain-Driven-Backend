﻿using AutoMapper;
using DDD.Application.Services.Abstractions;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Utilities.DTOs.Contact;
using DDD.Utilities.DTOs.ContactDependents;
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

        public async Task<ContactDto> CreateContact(NewContactDto newContact)
        {
            try
            {

                _unitOfWork.BeginTransaction();

                var contactDB = new Contact();
                contactDB.Name = newContact.Name;
                contactDB.Lastname = newContact.Lastname;
                contactDB.Email = newContact.Email;

                _unitOfWork.ContactRepo.Add(contactDB);

                foreach (NewAddressDto newAdress in newContact.NewAddresses)
                {
                    contactDB.Addresses.Add(new Address() 
                    {
                        AddressLine = newAdress.AddressLine,
                        ContactId = contactDB.Id
                    });
                }


                await _unitOfWork.CommitTransaction();
                return _mapper.Map<ContactDto>(contactDB);  
                               

            }
            catch(Exception ex) 
            {
                await _unitOfWork.RollbackTransaction();
                throw new Exception("Something went wrong creating a contact.");
            }             

        }

        public Task<ContactDto> DeleteContact(int contactId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ContactDto>> GetContacts()
        {
            var contacts = await _unitOfWork.ContactRepo.GetContactsWithAddresses();

            return _mapper.Map<List<ContactDto>>(contacts);

        }

        public Task<ContactDto> UpdateContact(ContactDto updatedContact)
        {
            throw new System.NotImplementedException();
        }
    }
}