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
            var clients = new List<User>
            {
                new User{Nom="Admin", Prenom="Sys", Email="thesysadmin@custom.net", Password=PasswordHashing.HashPassword("abcdef"), Adresse="It is a mystery", DateNaissance=DateTime.Parse("1990-01-30"), Role=Roles.admin},
                new User{Nom="Gilbert", Prenom="Alex", Email="alexgilbert@yahoo.com", NoPolice="123", Password=PasswordHashing.HashPassword("abcdef"), Adresse="943, Cantin, Lévis, Québec, Canada", DateNaissance=DateTime.Parse("1996-09-30")},
                new User{Nom="Bouchard", Prenom="Pascale", Email="pascale_bouchard@live.ca", NoPolice="1234", Password=PasswordHashing.HashPassword("abcdef"), Adresse="4, Rue des hêtres, Ste-Brigitte-De-Laval, Québec, Canada", DateNaissance=DateTime.Parse("1996-03-27")},
                new User{Nom="employe", Prenom="worker", Email="averageworker@coopera.tors", Password=PasswordHashing.HashPassword("abcdef"), Adresse="I'm way too poor to have a house", DateNaissance=DateTime.Parse("1988-04-03"), Role=Roles.employe},
            };

            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();

            var polices = new List<Police>
            {
                new Police{PoliceID = "123", UserID = context.Clients.Where(c => c.NoPolice == "123").First().UserID},
                new Police{PoliceID= "1234", UserID = context.Clients.Where(c => c.NoPolice == "1234").First().UserID},
            };

            polices.ForEach(s => context.Polices.Add(s));

            var r = new Random();

            var Rapports = new List<Accident>
            {
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.CollisionEnChaine, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[0].UserID, UserFirstName = context.Clients.ToList()[0].Prenom, UserLastName = context.Clients.ToList()[0].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.Carambolage, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[0].UserID, UserFirstName = context.Clients.ToList()[0].Prenom, UserLastName = context.Clients.ToList()[0].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.DepassementChausseeLaterale, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[1].UserID, UserFirstName = context.Clients.ToList()[1].Prenom, UserLastName = context.Clients.ToList()[1].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.CirculantMemeVoie, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[1].UserID, UserFirstName = context.Clients.ToList()[1].Prenom, UserLastName = context.Clients.ToList()[1].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.VehiculeEnStationnement, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[2].UserID, UserFirstName = context.Clients.ToList()[2].Prenom, UserLastName = context.Clients.ToList()[2].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.VirageSurFlecheVerte, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[2].UserID, UserFirstName = context.Clients.ToList()[2].Prenom, UserLastName = context.Clients.ToList()[2].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.VirageChausseeLaterale, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[3].UserID, UserFirstName = context.Clients.ToList()[3].Prenom, UserLastName = context.Clients.ToList()[3].Nom},
                new Accident() { Blessures = "", CirconstancesAccident = SituationAccident.VehiculeQuittantStationnement, DateAccidentEnregistre = DateTime.Now.AddDays(0 - r.Next(0, 5)), DateAccidentProduit = DateTime.Now.AddDays(0 - r.Next(5, 10)), Details = "", DetailsSupplementaires = "", InformationsAutreVoiture = "", Localisation = "Chez moi", RaisonDeplacement = "why not?", Temoins = "", UserID = context.Clients.ToList()[3].UserID, UserFirstName = context.Clients.ToList()[3].Prenom, UserLastName = context.Clients.ToList()[3].Nom},
            };

            Rapports.ForEach(s => context.Accidents.Add(s));
            context.SaveChanges();
            
            
        }
    }
}