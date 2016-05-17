using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CooperatorsDirect.DAL;
using CooperatorsDirect.Models;
using CooperatorsDirect.Security;
using System.Web.Script.Serialization;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using PagedList;
using System.Data.Entity.Migrations;

namespace CooperatorsDirect.Controllers
{
    public class AccidentsController : Controller
    {
        private CooperatorsContext db = new CooperatorsContext();

        public List<Accident> AllRapports()
        {
            var accidents = db.GetAllAccidents();
            return accidents;
        }

        public Accident GetRapport(int id)
        {
            if (AllRapports().Where(r => r.ID == id).Count() < 1)
                return null;
            var role = SessionPersiter.User.Role;
            var rap = AllRapports().Where(r => r.ID == id).First();
            if (role == Roles.admin || role == Roles.employe || rap.UserID == SessionPersiter.User.UserID)
                return rap;
            else
                throw new ArgumentException("Accès refusé");
        }

        // GET: Accidents
        [CustomAuthorize(Roles.admin, Roles.client, Roles.employe, Roles.reparateur)]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            var listeAccidents = db.Accidents.ToList();
            if (SessionPersiter.User.Role == Roles.client)
            {
                listeAccidents = listeAccidents.Where(a => a.UserID == SessionPersiter.User.UserID).ToList();
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateRapportSortParam = String.IsNullOrEmpty(sortOrder) ? "daterapport_desc" : "daterapport_asc";
            ViewBag.DateRapportSortParam = sortOrder == "daterapport_asc" ? "daterapport_desc" : "daterapport_asc";
            ViewBag.DateAccidentSortParam = sortOrder == "dateaccident_asc" ? "dateaccident_desc" : "dateaccident_asc";
            ViewBag.PositionSortParam = sortOrder == "position_asc" ? "position_desc" : "position_asc";
            ViewBag.CirconstancesSortParam = sortOrder == "circonstances_asc" ? "circonstances_desc" : "circonstances_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                if (currentFilter == null)
                    currentFilter = "";
                searchString = currentFilter.ToLower();
            }
            ViewBag.CurrentFilter = searchString;

            listeAccidents = listeAccidents.Where(a => a.DateAccidentEnregistre.ToString().ToLower().Contains(searchString) ||
                                                        a.DateAccidentProduit.ToString().ToLower().Contains(searchString) ||
                                                        a.Localisation.ToLower().Contains(searchString) |
                                                        GetDisplayName(a.CirconstancesAccident).ToLower().Contains(searchString)).ToList();

            switch (sortOrder)
            {
                case "daterapport_asc":
                    listeAccidents = listeAccidents.OrderBy(a => a.DateAccidentEnregistre).ToList();
                    break;
                case "dateaccident_asc":
                    listeAccidents = listeAccidents.OrderBy(a => a.DateAccidentProduit).ToList();
                    break;
                case "position_asc":
                    listeAccidents = listeAccidents.OrderBy(a => a.Localisation).ToList();
                    break;
                case "circonstances_asc":
                    listeAccidents = listeAccidents.OrderBy(a => a.CirconstancesAccident).ToList();
                    break;
                case "daterapport_desc":
                    listeAccidents = listeAccidents.OrderByDescending(a => a.DateAccidentEnregistre).ToList();
                    break;
                case "dateaccident_desc":
                    listeAccidents = listeAccidents.OrderByDescending(a => a.DateAccidentProduit).ToList();
                    break;
                case "position_desc":
                    listeAccidents = listeAccidents.OrderByDescending(a => a.Localisation).ToList();
                    break;
                case "circonstances_desc":
                    listeAccidents = listeAccidents.OrderByDescending(a => a.CirconstancesAccident).ToList();
                    break;
                default:
                    listeAccidents = listeAccidents.OrderBy(a => a.DateAccidentEnregistre).OrderBy(a => a.DateAccidentProduit).ToList();
                    break;
            }
            int size = (pageSize ?? 5);
            ViewBag.CurrentSize = size;
            int pageNumber = (page ?? 1);
            return View(listeAccidents.ToPagedList(pageNumber, size));
        }

        // GET: Accidents/Details/5
        [CustomAuthorize(Roles.admin, Roles.client, Roles.employe, Roles.reparateur)]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accident accident = GetRapport(int.Parse(id.ToString()));
            if (accident == null)
            {
                return HttpNotFound();
            }
            return View(accident);
        }

        [CustomAuthorize(Roles.admin, Roles.client, Roles.employe, Roles.reparateur)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "ID,newComment")] Accident r)
        {
            var realR = GetRapport(r.ID);
            var c = r.newComment;
            var u = SessionPersiter.User;
            c.utilisateurID = u.UserID;
            c.utilisateurName = u.Prenom;
            c.utilisateurSurname = u.Nom;
            c.DemandeID = r.ID;
            db.Comments.Add(c);
            db.SaveChanges();
            r.Comments = realR.Comments.ToList();
            r.ID = realR.ID;
            r.AuMoinsDeuxVehicules = realR.AuMoinsDeuxVehicules;
            r.Blessures = realR.Blessures;
            r.CirconstancesAccident = realR.CirconstancesAccident;
            r.ConducteurHeurtePropreVehicule = realR.ConducteurHeurtePropreVehicule;
            r.DateAccidentEnregistre = realR.DateAccidentEnregistre;
            r.DateAccidentProduit = realR.DateAccidentProduit;
            r.Details = realR.Details;
            r.DetailsSupplementaires = realR.DetailsSupplementaires;
            r.InformationsAutreVoiture = realR.InformationsAutreVoiture;
            r.Localisation = realR.Localisation;
            r.NumeroVehicule = realR.NumeroVehicule;
            r.Photographies = realR.Photographies;
            r.ProduitAuQuebec = realR.ProduitAuQuebec;
            r.ProprietairesDifferents = realR.ProprietairesDifferents;
            r.ProprietairesIdentifies = realR.ProprietairesIdentifies;
            r.RaisonDeplacement = realR.RaisonDeplacement;
            r.SituationVehicules = realR.SituationVehicules;
            r.Temoins = realR.Temoins;
            r.UserFirstName = realR.UserFirstName;
            r.UserID = realR.UserID;
            r.UserLastName = realR.UserLastName;
            r.newComment = null;
            if (!string.IsNullOrWhiteSpace(c.Text))
            {
                if (r.isValid())
                {
                    db.Set<Accident>().AddOrUpdate(r);
                    //db.Entry(r).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Details", "Accidents", new { id = r.ID });
        }


        // GET: Accidents/Create
        [CustomAuthorize(Roles.admin, Roles.employe, Roles.reparateur)]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Accidents/Rapporter
        [CustomAuthorize(Roles.admin, Roles.client, Roles.employe, Roles.reparateur)]
        public ActionResult Rapporter()
        {
            Accident a = new Accident()
            {
                DateAccidentEnregistre = DateTime.Now,
                DateAccidentProduit = DateTime.Now,
                UserID = SessionPersiter.User.UserID,
                UserFirstName = SessionPersiter.User.Prenom,
                UserLastName = SessionPersiter.User.Nom
            };
            return View(a);
        }

        [HttpPost]
        public string ExportCirconstanceJson(SituationVehicule sit)
        {
            var values = Accident.GetCirconstances(sit);
            var dict = new Dictionary<string, List<String>>();
            foreach(var circ in values)
            {
                dict.Add(circ.ToString(), Accident.GetExamplesPath(circ));
                dict[circ.ToString()].Insert(0, GetDisplayName(circ));
            }

            var json = new JavaScriptSerializer().Serialize(dict);

            return json;
        }

        public static string GetDisplayName(Enum value)
        {
            Type enumType = value.GetType();
            var enumValue = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(enumValue)[0];

            var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            var outString = ((DisplayAttribute)attrs[0]).Name;

            if (((DisplayAttribute)attrs[0]).ResourceType != null)
            {
                outString = ((DisplayAttribute)attrs[0]).GetName();
            }

            return outString;
        }

        // POST: Accidents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateAccidentEnregistre,DateAccidentProduit,Localisation,RaisonDeplacement,Blessures,Temoins,InformationsAutreVoiture,DetailsSupplementaires,AuMoinsDeuxVehicules,ProduitAuQuebec,ProprietairesIdentifies,ProprietairesDifferents,ConducteurHeurtePropreVehicule,Details,SituationVehicules,CirconstancesAccident,NumeroVehicule")] Accident accident)
        {
            accident.UserID = SessionPersiter.User.UserID;
            accident.UserFirstName = SessionPersiter.User.Prenom;
            accident.UserLastName = SessionPersiter.User.Nom;
            if (ModelState.IsValid)
            {
                db.Accidents.Add(accident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accident);
        }

        // POST: Accidents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rapporter([Bind(Include = "ID,DateAccidentEnregistre,DateAccidentProduit,Localisation,RaisonDeplacement,Blessures,Temoins,InformationsAutreVoiture,DetailsSupplementaires,AuMoinsDeuxVehicules,ProduitAuQuebec,ProprietairesIdentifies,ProprietairesDifferents,ConducteurHeurtePropreVehicule,Details,SituationVehicules,CirconstancesAccident,NumeroVehicule")] Accident accident)
        {
            accident.UserID = SessionPersiter.User.UserID;
            accident.UserFirstName = SessionPersiter.User.Prenom;
            accident.UserLastName = SessionPersiter.User.Nom;
            if (ModelState.IsValid)
            {
                db.Accidents.Add(accident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accident);
        }

        // GET: Accidents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accident accident = db.Accidents.Find(id);
            if (accident == null)
            {
                return HttpNotFound();
            }
            return View(accident);
        }

        // POST: Accidents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateAccidentEnregistre,DateAccidentProduit,Localisation,RaisonDeplacement,Blessures,Temoins,InformationsAutreVoiture,DetailsSupplementaires,AuMoinsDeuxVehicules,ProduitAuQuebec,ProprietairesIdentifies,ProprietairesDifferents,ConducteurHeurtePropreVehicule,Details,SituationVehicules,CirconstancesAccident,NumeroVehicule")] Accident accident)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accident).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accident);
        }

        // GET: Accidents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accident accident = db.Accidents.Find(id);
            if (accident == null)
            {
                return HttpNotFound();
            }
            return View(accident);
        }

        // POST: Accidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accident accident = db.Accidents.Find(id);
            db.Accidents.Remove(accident);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
