using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IContactRepository ContactRepo { get; set; }
        public IRepository<Address> AddressRepo { get; set; }

        public Task SaveChanges();
        public Task BeginTransaction();
        public Task CommitTransaction();
        public Task RollbackTransaction();

    }
}
