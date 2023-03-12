using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Implementations
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetContactsWithAddresses()
        {
            var contacts = await _context.Set<Contact>()
                                         .Include(x => x.Addresses)
                                         .ToListAsync();
            return contacts;
                                   
            
        }
    }
}
