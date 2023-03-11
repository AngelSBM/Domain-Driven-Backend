using DDD.Utilities.DTOs.ContactDependents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Utilities.DTOs.Contact
{
    public class NewContactDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public IEnumerable<NewAddressDto> NewAddresses { get; set; }
    }
}
