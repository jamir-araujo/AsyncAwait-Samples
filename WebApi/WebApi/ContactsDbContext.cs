using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class ContactsDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(c =>
             {
                 c.HasKey(p => p.Id);
             });
        }
    }

    public class Contact
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
