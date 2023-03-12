using DDD.Application.Services.Abstractions;
using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Application.Controllers
{
    [Route("Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IRepository<Address> _addressRepository;
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService, IRepository<Address> repo)
        {
            _contactService = contactService;
            _addressRepository = repo;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _addressRepository.GetAll();
            return new OkObjectResult(response);
        }

    }
}
