using DDD.Utilities.DTOs.ContactDependents;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Utilities.DTOs.Contact
{
    public class ContactDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }

    }
}
