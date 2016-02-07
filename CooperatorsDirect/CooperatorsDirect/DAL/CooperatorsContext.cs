using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CooperatorsDirect.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CooperatorsDirect.DAL
{
    public class CooperatorsContext : DbContext
    {
    
        public CooperatorsContext() : base("CooperatorsContext")
        {
        }
        
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Add(Client user)
        {
            Clients.Add(user);
        }
    }
}