using CooperatorsDirect.Models;
using CooperatorsDirect.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CooperatorsDirect.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [CustomAuthorize(Roles.admin, Roles.reparateur, Roles.employe, Roles.client)]
        public ActionResult Index()
        {
            //TODO
            //If connecté --> go to home
            return View();
            //Else go to connexion
        }
    }
}