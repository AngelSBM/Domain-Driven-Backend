using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public ICollection<Address> Addresses { get; set; }


        public void AddAddressInPuntaCana(Address address)
        {

            if (!IsAddressInPuntaCana(address.AddressLine))
            {
                throw new InvalidOperationException("Address must be in Punta Cana city.");
            }

            if (Addresses.Contains(address))
            {
                throw new InvalidOperationException("You already have this address");
            }

            Addresses.Add(address);
        }

        private bool IsAddressInPuntaCana(string address) 
        { 
            bool isAddressInPuntaCana = false;

            if (address.ToLower().Contains("punta cana")){
                isAddressInPuntaCana = true;
            }
            return isAddressInPuntaCana;
        }
    }
}
