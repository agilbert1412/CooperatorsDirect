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
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<UneImage> Images { get; set; }
        public DbSet<Police> Polices { get; set; }
        public DbSet<Commentaire> Comments { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public void Add(User user)
        {
            Clients.Add(user);
        }

        public List<Accident> GetAllAccidents()
        {
            List<Accident> allAccidents = new List<Accident>();

            foreach (Accident r in Accidents)
            {
                r.Comments = new List<Commentaire>();
                allAccidents.Add(r);
            }
            
            foreach (var comm in Comments)
            {
                allAccidents.Find(r => r.ID == comm.DemandeID).Comments.Add(comm);
            }

            return allAccidents;
        }

    }
}