using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CooperatorsDirect;
using CooperatorsDirect.Models;
using CooperatorsDirect.Security;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace CooperatorsDirect_TST
{
    [TestClass]
    public class CooperatorsDirectTests
    {

        #region Images

        [TestMethod]
        [TestProperty("Category", "Images")]
        public void GetImagesTest()
        {
            var acc = new Accident();
            var values = Enum.GetValues(typeof(SituationAccident)).Cast<SituationAccident>();
            var cas0 = new List<SituationAccident>() { SituationAccident.VehiculeEnStationnement, SituationAccident.VehiculeEnStationnementIllegal, SituationAccident.NonRespectSignalisation, SituationAccident.VirageSurFlecheVerte, SituationAccident.VirageADroiteSurFeuRouge, SituationAccident.MarcheArriereDemiTour, SituationAccident.OuvertureDunePortiere, SituationAccident.CollisionParcStationnementSansSignalisation};
            var cas1 = new List<SituationAccident>() { SituationAccident.VehiculeChevauchantLigneContinue, SituationAccident.PrioriteDePassage, SituationAccident.ArretOuFeuDefectueux, SituationAccident.VehiculeQuittantChausseeLaterale, SituationAccident.CollisionEnChaine, SituationAccident.Carambolage };
            var cas2 = new List<SituationAccident>() { SituationAccident.DepassementChausseeLateraleIntersection };
            var cas3 = new List<SituationAccident>() { SituationAccident.CirculantMemeVoie, SituationAccident.VirageChausseeLaterale, SituationAccident.VehiculePrenantStationnement, SituationAccident.VehiculeQuittantStationnement, SituationAccident.CollisionLaterale, SituationAccident.ChangementVoie, SituationAccident.DepassementChausseeLaterale, SituationAccident.VehiculeChevauchantAxeMediant, SituationAccident.VehiculesPositionIndeterminee, SituationAccident.PrioriteADroite };
            var dicCas = new Dictionary<int, List<SituationAccident>>();
            dicCas.Add(0, cas0);
            dicCas.Add(1, cas1);
            dicCas.Add(2, cas2);
            dicCas.Add(3, cas3);

            foreach (var enumVal in values)
            {
                acc.CirconstancesAccident = enumVal;
                var lstImages = acc.GetExamples();
                for (int i = 0; i < 4; i++)
                {
                    //if (dicCas[i].Contains(enumVal))
                    //    Assert.AreEqual(lstImages.Count, i);
                    //else
                    //    Assert.AreNotEqual(lstImages.Count, i);
                }
            }
        }

        #endregion


        #region Hashing

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void HashingTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            string shouldBe = "bef57ec7f53a6d40beb640a780a639c83bc29ac8a9816f1fc6c5c6dcd93c4721";
            Assert.AreEqual(hashed, shouldBe);
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void BigHashingTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            string shouldBe = "77d721c817f9d216c1fb783bcad9cdc20aaa2427402683f1f75dd6dfbe657470";
            Assert.AreEqual(hashed, shouldBe);
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void HugeHashingTest()
        {
            string pass = "";
            for (int i = 0; i < 10000; i++)
            {
                pass = pass + "abcdefghijklmnopqrstuvwxyz1234567890";
            }
            string hashed = PasswordHashing.HashPassword(pass);
            string shouldBe = "832f9c9ed82ae93524bf0dece408ccc374eb669dcd28069d4dbf572d90f83473";
            Assert.AreEqual(hashed, shouldBe);
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void HashCompareTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(hashed, pass));
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void BigHashCompareTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(hashed, pass));
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void HugeHashCompareTest()
        {
            string pass = "";
            for (int i = 0; i < 10000; i++)
            {
                pass = pass + "abcdefghijklmnopqrstuvwxyz1234567890";
            }
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(hashed, pass));
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void BadHashCompareTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsFalse(PasswordHashing.VerifyHashedPassword(hashed, "abcdefg"));
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void BigBadHashCompareTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsFalse(PasswordHashing.VerifyHashedPassword(hashed, "abcdefghijklmnopqrstuvwxyz1234567890a"));
        }

        [TestMethod]
        [TestProperty("Category", "Hashing")]
        public void HugeBadHashCompareTest()
        {
            string pass = "";
            for (int i = 0; i < 10000; i++)
            {
                pass = pass + "abcdefghijklmnopqrstuvwxyz1234567890";
            }
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsFalse(PasswordHashing.VerifyHashedPassword(hashed, pass + "a"));
        }

        #endregion

        #region Classe User

        [TestMethod]
        [TestProperty("Category", "User")]
        public void CreateUser()
        {
            User u = null;
            Assert.IsNull(u);
            u = new User();
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserID);
            Assert.IsNull(u.Adresse);
            Assert.IsNull(u.Email);
            Assert.IsNull(u.Nom);
            Assert.IsNull(u.NoPolice);
            Assert.IsNull(u.Password);
            Assert.IsNull(u.Prenom);
            Assert.IsTrue(u.Role == Roles.client);
            Assert.IsNotNull(u.DateNaissance);
        }

        [TestMethod]
        [TestProperty("Category", "User")]
        public void CreateUserWithParameters()
        {
            User u = null;
            Assert.IsNull(u);
            DateTime d = DateTime.Now;
            u = new User("Wayne", "Bruce", "batsy@waynecorp.com", "12345", PasswordHashing.HashPassword("12345"), d, "Wayne Manor");
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserID);
            Assert.IsTrue(u.Adresse == "Wayne Manor");
            Assert.IsTrue(u.Email == "batsy@waynecorp.com");
            Assert.IsTrue(u.Nom == "Wayne");
            Assert.IsTrue(u.NoPolice == "12345");
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(u.Password, "12345"));
            Assert.IsTrue(u.Prenom == "Bruce");
            Assert.IsTrue(u.Role == Roles.client);
            Assert.IsTrue(u.DateNaissance == DateTime.Parse(d.ToShortDateString()));
        }

        [TestMethod]
        [TestProperty("Category", "User")]
        public void CreateUserWithParametersRole()
        {
            User u = null;
            Assert.IsNull(u);
            DateTime d = DateTime.Now;
            u = new User("Kent", "Clark", "supes@kryptonland.com", "12345", PasswordHashing.HashPassword("12345"), d, "Ice Fortress", Roles.admin);
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserID);
            Assert.IsTrue(u.Adresse == "Ice Fortress");
            Assert.IsTrue(u.Email == "supes@kryptonland.com");
            Assert.IsTrue(u.Nom == "Kent");
            Assert.IsTrue(u.NoPolice == "12345");
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(u.Password, "12345"));
            Assert.IsTrue(u.Prenom == "Clark");
            Assert.IsTrue(u.Role == Roles.admin);
            Assert.IsTrue(u.DateNaissance == DateTime.Parse(d.ToShortDateString()));
        }

        [TestMethod]
        [TestProperty("Category", "User")]
        public void CreateUserWithParametersTextDate()
        {
            User u = null;
            Assert.IsNull(u);
            DateTime d = DateTime.Now;
            u = new User("Allen", "Barry", "flashstep@light.spd", "12345", PasswordHashing.HashPassword("12345"), d.ToShortDateString(), "Her.. wait no over there", Roles.reparateur);
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.UserID);
            Assert.IsTrue(u.Adresse == "Her.. wait no over there");
            Assert.IsTrue(u.Email == "flashstep@light.spd");
            Assert.IsTrue(u.Nom == "Allen");
            Assert.IsTrue(u.NoPolice == "12345");
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(u.Password, "12345"));
            Assert.IsTrue(u.Prenom == "Barry");
            Assert.IsTrue(u.Role == Roles.reparateur);
            Assert.IsTrue(u.DateNaissance == DateTime.Parse(d.ToShortDateString()));
        }

        #endregion
    }
}
