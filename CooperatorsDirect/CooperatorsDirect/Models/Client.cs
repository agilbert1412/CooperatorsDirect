using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public class Client
    {

        public int ClientID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string NoPolice { get; set; }
        public string Password { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Adresse { get; set; }
        [Display(Name = "Roles")]
        [Required]
        public Roles Role { get; set; }

        public Client()
        {

        }
    }
}