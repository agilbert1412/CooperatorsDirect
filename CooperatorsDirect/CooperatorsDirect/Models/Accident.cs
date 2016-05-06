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

        [Display(Name = "Date du rapport d'accident")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAccidentEnregistre { get; set; }

        [Display(Name = "Date et heure de l'accident")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAccidentProduit { get; set; }

        [Display(Name = "Lieu de l'accident")]
        public string Localisation { get; set; }

        [Display(Name = "Raison du déplacement")]
        public string RaisonDeplacement { get; set; }

        [Display(Name = "Blessures dans le véhicule (si oui, indiquer le nom et prénom du blessé, sa date de naissance, "
            + " sa position dans le véhicule et si la ceinture de sécurité était porté")]
        public string Blessures { get; set; }

        [Display(Name = "Témoins de l'accident")]
        public string Temoins { get; set; }

        [Display(Name = "Information sur l'autre véhicule impliqué (nom, adresse, numéro de téléphone, "
            +"numéro de permis de conduire, numéro de certificat d’immatriculation et coordonnées de l’assureur.)")]
        public string InformationsAutreVoiture { get; set; }

        [Display(Name = "Information supplémentaire (Si le cas n'est pas couvert par les situations précédentes, "
            +"fournir ici la description la plus complète possible de l'incident)")]
        public string DetailsSupplementaires { get; set; }

        [Display(Name = "Photo de l'accident et du constat à l'amiable")]
        public List<UneImage> Photographies { get; set; }

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
            AuMoinsDeuxVehicules = true;
            ProduitAuQuebec = true;
            ProprietairesIdentifies = true;
            ProprietairesDifferents = true;
            ConducteurHeurtePropreVehicule = false;
        }

        public static List<SituationAccident> GetCirconstances(SituationVehicule sit)
        {
            var lst = new List<SituationAccident>();
            switch (sit)
            {
                case SituationVehicule.MemeDirectionMemeChaussee:
                    lst.Add(SituationAccident.CirculantMemeVoie);
                    lst.Add(SituationAccident.VirageChausseeLaterale);
                    lst.Add(SituationAccident.VehiculePrenantStationnement);
                    lst.Add(SituationAccident.VehiculeQuittantStationnement);
                    lst.Add(SituationAccident.VehiculeEnStationnement);
                    lst.Add(SituationAccident.VehiculeEnStationnementIllegal);
                    break;
                case SituationVehicule.MemeDirectionVoieDifferente:
                    lst.Add(SituationAccident.CollisionLaterale);
                    lst.Add(SituationAccident.ChangementVoie);
                    lst.Add(SituationAccident.DepassementChausseeLaterale);
                    lst.Add(SituationAccident.DepassementChausseeLateraleIntersection);
                    break;
                case SituationVehicule.SensInverse:
                    lst.Add(SituationAccident.VehiculeChevauchantAxeMediant);
                    lst.Add(SituationAccident.VehiculesPositionIndeterminee);
                    lst.Add(SituationAccident.VehiculeChevauchantLigneContinue);
                    break;
                case SituationVehicule.ProvenanceTransversaleOuLaterales:
                    lst.Add(SituationAccident.PrioriteADroite);
                    lst.Add(SituationAccident.PrioriteDePassage);
                    lst.Add(SituationAccident.ArretOuFeuDefectueux);
                    lst.Add(SituationAccident.VehiculeQuittantChausseeLaterale);
                    break;
                case SituationVehicule.Autres:
                    lst.Add(SituationAccident.NonRespectSignalisation);
                    lst.Add(SituationAccident.VirageSurFlecheVerte);
                    lst.Add(SituationAccident.VirageADroiteSurFeuRouge);
                    lst.Add(SituationAccident.MarcheArriereDemiTour);
                    lst.Add(SituationAccident.OuvertureDunePortiere);
                    lst.Add(SituationAccident.CollisionEnChaine);
                    lst.Add(SituationAccident.Carambolage);
                    lst.Add(SituationAccident.CollisionParcStationnementSansSignalisation);
                    break;
                default:
                    break;
            }
            return lst;
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

        /// <summary>
        /// Retourne les images d'exemples, ou une liste vide si il n'y en a pas
        /// </summary>
        /// <param name="sit">Situation de l'accident</param>
        /// <returns>Liste d'images selon la situation</returns>
        public static List<Bitmap> GetExamples(SituationAccident sit)
        {

            // à compléter en mettant les images sur http://photobucket.com/

            var liste = new List<Bitmap>();
            switch (sit)
            {
                case SituationAccident.ArretOuFeuDefectueux:
                    liste.Add(getImage("http://i1382.photobucket.com/albums/ah245/PhotobucketMKTG/Print_Shop_Intro.png~original"));
                    break;
                case SituationAccident.Carambolage:
                    // liste.Add(getImage("URL"));
                    // liste.Add(getImage("URL"));
                    // liste.Add(getImage("URL"));
                    break;
                default:
                    break;
            }
            return liste;
        }

        private static Bitmap getImage(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            return new Bitmap(responseStream);
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
        [Display(Name = "Collision entre deux véhicules circulant sur la même voie")]
        CirculantMemeVoie,
        [Display(Name = "Virage sur une chaussée latérale ou dans une entrée")]
        VirageChausseeLaterale,
        [Display(Name = "Véhicule prenant un stationnement ou s'engageant dans un parc de stationnement")]
        VehiculePrenantStationnement,
        [Display(Name = "Véhicule quittant un stationnement")]
        VehiculeQuittantStationnement,
        [Display(Name = "Véhicule en stationnement")]
        VehiculeEnStationnement,
        [Display(Name = "Véhicule en stationnement illégal hors agglomération et sans feux la nuit")]
        VehiculeEnStationnementIllegal,
        [Display(Name = "Collision Latérale")]
        CollisionLaterale,
        [Display(Name = "Changement de voie")]
        ChangementVoie,
        [Display(Name = "Dépassement sur des chaussées latérales")]
        DepassementChausseeLaterale,
        [Display(Name = "Dépassement sur des chaussées latérale dans une intersection")]
        DepassementChausseeLateraleIntersection,
        [Display(Name = "Véhicule chevauchant l'axe médiant")]
        VehiculeChevauchantAxeMediant,
        [Display(Name = "Véhicule dont la position ne peut pas être déterminée")]
        VehiculesPositionIndeterminee,
        [Display(Name = "Véhicule chevauchant une ligne continue")]
        VehiculeChevauchantLigneContinue,
        [Display(Name = "Priorité à droite")]
        PrioriteADroite,
        [Display(Name = "Priorité de passage")]
        PrioriteDePassage,
        [Display(Name = "Panneau d'arrêt et feu de signalisation défecteux ou inopérant")]
        ArretOuFeuDefectueux,
        [Display(Name = "Véhicule quittant une chaussées latérale")]
        VehiculeQuittantChausseeLaterale,
        [Display(Name = "Non respect de la signalisation")]
        NonRespectSignalisation,
        [Display(Name = "Virage sur une flèche verte")]
        VirageSurFlecheVerte,
        [Display(Name = "Virage à droite sur un feu rouge")]
        VirageADroiteSurFeuRouge,
        [Display(Name = "Marche arrière et demi-tour")]
        MarcheArriereDemiTour,
        [Display(Name = "Ouverture d'une portière")]
        OuvertureDunePortiere,
        [Display(Name = "Collision en chaîne")]
        CollisionEnChaine,
        [Display(Name = "Carambolage")]
        Carambolage,
        [Display(Name = "Collision survenant dans un parc de stationnement sans signalisation")]
        CollisionParcStationnementSansSignalisation
    }
}