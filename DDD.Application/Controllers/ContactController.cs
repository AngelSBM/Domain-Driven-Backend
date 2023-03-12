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

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _contactService.GetContacts();
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(NewContactDto newContact)
        {
            try
            {
                var response = await _contactService.CreateContact(newContact);
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
