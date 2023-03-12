using DDD.Domain.Entities;
using DDD.Domain.Interfaces;
using DDD.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context,
                    IContactRepository contactRepo,
                    IRepository<Address> addressRepo) 
        {
            _context = context;

            ContactRepo = contactRepo;
            AddressRepo = addressRepo;
        }

        public IContactRepository ContactRepo { get; set; }
        public IRepository<Address> AddressRepo { get; set; }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
