using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDD.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts;
        public DbSet<Address> Addresses;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("contact_name");
                entity.Property(e => e.Lastname).HasColumnName("contact_lastname");
                entity.Property(e => e.Email).HasColumnName("email");
            });


            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AddressLine).HasColumnName("address_line");
                entity.Property(e => e.ContactId).HasColumnName("contact_id");
            });
        }
    }
}
