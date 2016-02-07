using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CooperatorsDirect.Models;
using CooperatorsDirect.Security;

namespace CooperatorsDirect.DAL
{
    public class CooperatorsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CooperatorsContext>
    {
        protected override void Seed(CooperatorsContext context)
        {
            var clients = new List<Client>
            {
                new Client{Nom="Gilbert", Prenom="Alex", Email="alexgilbert@yahoo.com", NoPolice="123456", Password=PasswordHashing.HashPassword("abcdef"), Adresse="943, Cantin, Lévis, Québec, Canada", DateNaissance=DateTime.Parse("1996-09-30")}
            };

            clients.ForEach(s => context.Students.Add(s));
            context.SaveChanges();
            
            
        }
    }
}