﻿using System;
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
        private IUserRepository repository;



        //Constructeur par défaut utilisant le référentielle permettant l'accès aux modèles d'entity framework.
        public UserController()
        {
            repository = new UserModel();
        }

        ////Constructeur utilisant un autre type de repository (utilisation pour le mocking ) 
        public UserController(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Page par défaut du controleur. Ici c'est la page de connexion d'un client
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (SessionPersiter.User == null)
                return View();
            else
                return RedirectToAction("Home");
        }

        /// <summary>
        /// Page pour une liste des utilisateurs de l'application
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.admin)]
        public ActionResult List()
        {
            return View(repository.GetAll());
        }

        /// <summary>
        /// Page d'un client
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles.admin, Roles.reparateur, Roles.employe, Roles.client)]
        public ActionResult Details(int id = -1)
        {
            if (id == -1)
            {
                id = SessionPersiter.User.UserID;
            }
            else
            {
                if (SessionPersiter.User.Role != Roles.admin && SessionPersiter.User.UserID != id)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            User user = repository.GetUser(id);
            return View(user);
        }

        /// <summary>
        /// Page de création du contrôler. Ici c'est la page d'inscription d'un client
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(User user)
        {

            UserModel userMod = new UserModel();
            if (userMod.Find(user.Email) != null)
            {
                ViewBag.UsernameError = "L'adresse courriel est déjà utilisée doit être unique";
            }

            if (!userMod.Insert(user))
            {
                return View("Create");
            }
            return View("Index");
        }


        /// <summary>
        /// On recrée la vue Index peut importe le resultat. Par contre dans le cas d'un échec de connexion 
        /// le viewBag.Error contiendra le message d'erreur approprié
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult connexion(string email, string password)
        {
            UserModel userMod = new UserModel();
            User userConnected = userMod.GetUser(email, password);
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)
                || userConnected == null)
            {
                ViewBag.Error = "Le nom d'utilisateur ou le mot de passe est incorrect";
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        /// <summary>
        /// Retour à la page index
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            SessionPersiter.User = null;
            return RedirectToAction("Index");
        }
    }
}