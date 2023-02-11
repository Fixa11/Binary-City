using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Binary_City.Models
{
    public class BinaryCityModel : DbContext
    {
        public BinaryCityModel()
                : base("name=BinaryCity")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientsContact> ClientsContacts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Contact>()
              .HasIndex(Contact => Contact.Email)
                .IsUnique();
        }

    }
}