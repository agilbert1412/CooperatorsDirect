using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public class Accident
    {

        public int AccidentID { get; set; }

        [Display(Name = "Il y a eu une collision entre au moins 2 véhicules")]
        public bool AuMoinsDeuxVehicules { get; set; }
        [Display(Name = "L'accident s'est produit au Québec")]
        public bool ProduitAuQuebec { get; set; }
        [Display(Name = "Les propriétaires des véhicules impliqués sont identifiés")]
        public bool ProprietairesIdentifies { get; set; }
        [Display(Name = "Les véhicules appartiennent à des propriétaires différents")]
        public bool ProprietairesDifferents { get; set; }
        [Display(Name = "Le conducteur a heurté son propre véhicule")]
        public bool ConducteurHeurtePropreVehicule { get; set; }

        [Display(Name = "Détails pertinents")]
        public string Details { get; set; }

        private List<Message> _messages;
        public List<Message> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value.OrderByDescending(m => m.SentTime).ToList();
            }
        }

        public Accident()
        {

        }

    }

    public enum SituationVehicule
    {
        [Display(Name = "Véhicules en circulation dans le même sens sur la même chaussée")]
        MemeDirectionMemeChaussee,
        [Display(Name = "Véhicules en circulation dans le même sens sur des voies différentes")]
        MemeDirectionVoieDifferente,
        [Display(Name = "Véhicules en circulation en sens inverse")]
        SensInverse,
        [Display(Name = "Véhicules provenant de chaussées transversales ou latérales")]
        ProvenanceTransversaleOuLaterales,
        [Display(Name = "Divers")]
        Autres
    }
}