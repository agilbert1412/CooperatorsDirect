using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        [Display(Name = "Numéro de police d'assurance")]
        public string NoPolice { get; set; }
        public string Password { get; set; }
        [Display(Name = "Date de naissance")]
        public DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        [Display(Name = "Roles")]
        [Required]
        public Roles Role { get; set; }

        public User()
        {
            Role = Roles.client;
            setDateRight();
        }

        public User(string nom, string prenom, string email, string noPolice, string password, DateTime dateNaissance, string adresse, Roles role = Roles.client)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            NoPolice = noPolice;
            Password = password;
            DateNaissance = dateNaissance;
            Adresse = adresse;
            Role = role;
            setDateRight();
        }

        public User(string nom, string prenom, string email, string noPolice, string password, string dateNaissance, string adresse, Roles role = Roles.client)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            NoPolice = noPolice;
            Password = password;
            DateNaissance = DateTime.Parse(dateNaissance);
            Adresse = adresse;
            Role = role;
            setDateRight();
        }

        private void setDateRight()
        {
            DateNaissance = DateTime.Parse(DateNaissance.ToShortDateString());
        }
    }
}