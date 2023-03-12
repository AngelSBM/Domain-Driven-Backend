using DDD.Application.Services.Abstractions;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Utilities.DTOs.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.Controllers
{
    [Route("Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }


        //Errors are being handled in the ExceptionHanlder middleware
        [HttpGet]
        [Route("{contactId}")]
        public async Task<IActionResult> GetById(int contactId) =>
            new OkObjectResult(await _contactService.GetContactById(contactId));

        //Errors are being handled in the ExceptionHanlder middleware
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll() => new OkObjectResult(await _contactService.GetContacts());



        //Errors are being handled in the ExceptionHanlder middleware
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(NewContactDto newContact) => 
            new OkObjectResult(await _contactService.CreateContact(newContact));


        //Errors are being handled in the ExceptionHanlder middleware
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateContactDTO updatedContact) =>
            new OkObjectResult(await _contactService.UpdateContact(updatedContact));


        //Errors are being handled in the ExceptionHanlder middleware
        [HttpDelete]
        [Route("Delete/{contactId}")]
        public async Task<IActionResult> Delete(int contactId) =>
            new OkObjectResult(await _contactService.DeleteContact(contactId));


        //Errors are being handled in the ExceptionHanlder middleware
        [HttpDelete]
        [Route("DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId) =>
            new OkObjectResult(await _contactService.DeleteAddress(addressId));

    }
}
