using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public int ContactId { get; set; }

        public Contact Contact { get; set; }

    }
}
