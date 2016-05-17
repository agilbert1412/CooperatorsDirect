using System;
using System.ComponentModel.DataAnnotations;

namespace CooperatorsDirect.Models
{
    public class Commentaire
    {
        [Display(Name = "Numéro du commentaire")]
        public long ID { get; set; }

        [Display(Name = "Numéro de la demande")]
        public int DemandeID { get; set; }

        [Display(Name = "Utilisateur qui a commenté")]
        public int utilisateurID { get; set; }

        [Display(Name = "Utilisateur qui a commenté")]
        public string utilisateurName { get; set; }

        [Display(Name = "Utilisateur qui a commenté")]
        public string utilisateurSurname { get; set; }

        [Display(Name = "Date du commentaire")]
        public DateTime DateCommentaire { get; set; }

        [Display(Name = "Nouveau Commentaire")]
        public string Text { get; set; }

        public Commentaire()
        {
            DateCommentaire = DateTime.Now;
        }
    }
}
