﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CooperatorsDirect.Models;
using CooperatorsDirect.Security;

namespace CooperatorsDirect.DAL
{
    public class CooperatorsInitializer : System.Data.Entity.DropCreateDatabaseAlways<CooperatorsContext>
    {
        protected override void Seed(CooperatorsContext context)
        {
            var clients = new List<User>
            {
                new User{Nom="Admin", Prenom="Sys", Email="thesysadmin@custom.net", NoPolice="123456", Password=PasswordHashing.HashPassword("abcdef"), Adresse="It is a mystery", DateNaissance=DateTime.Parse("1990-01-30"), Role=Roles.admin},
                new User{Nom="Gilbert", Prenom="Alex", Email="alexgilbert@yahoo.com", NoPolice="123456", Password=PasswordHashing.HashPassword("abcdef"), Adresse="943, Cantin, Lévis, Québec, Canada", DateNaissance=DateTime.Parse("1996-09-30")},
                new User{Nom="Bouchard", Prenom="Pascale", Email="pascale_bouchard@live.ca", NoPolice="123456", Password=PasswordHashing.HashPassword("abcdef"), Adresse="4, Rue des hêtres, Ste-Brigitte-De-Laval, Québec, Canada", DateNaissance=DateTime.Parse("1996-03-27")}
            };

            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();
            
            
        }
    }
}