using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        public Task<IEnumerable<Contact>> GetContactsWithAddresses();
    }
}
