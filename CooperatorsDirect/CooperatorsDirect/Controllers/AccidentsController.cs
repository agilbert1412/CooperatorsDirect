﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CooperatorsDirect.DAL;
using CooperatorsDirect.Models;

namespace CooperatorsDirect.Controllers
{
    public class AccidentsController : Controller
    {
        private CooperatorsContext db = new CooperatorsContext();

        // GET: Accidents
        public ActionResult Index()
        {
            return View(db.Accidents.ToList());
        }

        // GET: Accidents/Details/5
        public ActionResult Details(int? id)
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

        // GET: Accidents/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Accidents/Rapporter
        public ActionResult Rapporter()
        {
            Accident a = new Accident()
            {
                DateAccidentEnregistre = DateTime.Now,
                DateAccidentProduit = DateTime.Now,
            };
            return View(a);
        }

        // POST: Accidents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccidentID,DateAccidentEnregistre,DateAccidentProduit,Localisation,RaisonDeplacement,Blessures,Temoins,InformationsAutreVoiture,DetailsSupplementaires,AuMoinsDeuxVehicules,ProduitAuQuebec,ProprietairesIdentifies,ProprietairesDifferents,ConducteurHeurtePropreVehicule,Details,SituationVehicules,CirconstancesAccident,NumeroVehicule")] Accident accident)
        {
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
        public ActionResult Edit([Bind(Include = "AccidentID,DateAccidentEnregistre,DateAccidentProduit,Localisation,RaisonDeplacement,Blessures,Temoins,InformationsAutreVoiture,DetailsSupplementaires,AuMoinsDeuxVehicules,ProduitAuQuebec,ProprietairesIdentifies,ProprietairesDifferents,ConducteurHeurtePropreVehicule,Details,SituationVehicules,CirconstancesAccident,NumeroVehicule")] Accident accident)
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