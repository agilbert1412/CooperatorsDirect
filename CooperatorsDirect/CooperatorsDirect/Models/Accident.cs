using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public class Accident
    {

        public int AccidentID { get; set; }

        public DateTime DateAccidentEnregistre { get; set; }

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

        [Display(Name = "Situation des véhicules impliqués")]
        public SituationVehicule SituationVehicules { get; set; }

        [Display(Name = "Circonstances de l'accident")]
        public SituationAccident CirconstancesAccident { get; set; }

        [Display(Name = "Numero de notre véhicule")]
        public int NumeroVehicule { get; set; }

        public Accident()
        {

        }

        public List<Bitmap> GetExamples()
        {
            return Accident.GetExamples(CirconstancesAccident);
        }

        public double GetMyResponsability()
        {
            return Accident.GetResponsabilities(CirconstancesAccident)[NumeroVehicule];
        }

        public double[] GetResponsabilities()
        {
            return Accident.GetResponsabilities(CirconstancesAccident);
        }

        public static List<Bitmap> GetExamples(SituationAccident sit)
        {
            throw new NotImplementedException("Retourne les images d'exemples, ou une liste vide si il n'y en a pas");
            return new List<Bitmap>();
        }

        public static double[] GetResponsabilities(SituationAccident sit)
        {
            throw new NotImplementedException("Retourne le pourcentage de responsabilité des membres, en ordre.");
            return new double[2]{ 1, 0 };
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

    public enum SituationAccident
    {
        CirculantMemeVoie,
        VirageChausseeLaterale,
        VehiculePrenantStationnement,
        VehiculeQuittantStationnement,
        VehiculeEnStationnement,
        VehiculeEnStationnementIllegal,
        CollisionLaterale,
        ChangementVoie,
        DepassementChausseeLaterale,
        DepassementChausseeLateraleIntersection,
        VehiculeChevauchantAxeMediant,
        VehiculesPositionIndeterminee,
        VehiculeChevauchantLigneContinue,
        PrioriteADroite,
        PrioriteDePassage,
        ArretOuFeuDefectueux,
        VehiculeQuittantChausseeLaterale,
        NonRespectSignalisation,
        VirageSurFlecheVerte,
        VirageADroiteSurFeuRouge,
        MarcheArriereDemiTour,
        OuvertureDunePortiere,
        CollisionEnChaine,
        Carambolage,
        CollisionParcStationnementSansSignalisation
    }
}