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
        
        public DbSet<User> Clients { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<UneImage> Images { get; set; }
        public DbSet<Police> Polices { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Add(User user)
        {
            Clients.Add(user);
        }

    }
}