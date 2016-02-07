using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CooperatorsDirect.Models;
using CooperatorsDirect.Security;


namespace LevelUp.Controllers
{
    public class UserController : Controller
    {
        //private IUserRepository repository;



        //Constructeur par défaut utilisant le référentielle permettant l'accès aux modèles d'entity framework.
        public UserController()
        {
            //repository = new UserModel();
        }

        ////Constructeur utilisant un autre type de repository (utilisation pour le mocking ) 
        //public UserController(IUserRepository repository)
        //{
        //    this.repository = repository;
        //}

        /// <summary>
        /// Page par défaut du controleur. Ici c'est la page de connexion d'un utilisateur
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }


        ///// <summary>
        ///// On recrée la vue Index peut importe le resultat. Par contre dans le cas d'un échec de connexion 
        ///// le viewBag.Error contiendra le message d'erreur approprié
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult connexion(string username, string password)
        //{
        //    UserModel userMod = new UserModel();
        //    User userConnected = userMod.Get(username, password);
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)
        //        || userConnected == null)
        //    {
        //        ViewBag.Error = "Le nom d'utilisateur ou le mot de passe est incorrect";
        //        return View("Index");
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //}

        ///// <summary>
        ///// Retour à la page index
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult Logout()
        //{
        //    SessionPersiter.User = null;
        //    return RedirectToAction("Index");
        //}
    }
}