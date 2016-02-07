using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CooperatorsDirect;
using CooperatorsDirect.Security;

namespace CooperatorsDirect_TST
{
    [TestClass]
    public class CooperatorsDirectTests
    {
        #region "Hashing"

        [TestMethod]
        public void HashingTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            string shouldBe = "bef57ec7f53a6d40beb640a780a639c83bc29ac8a9816f1fc6c5c6dcd93c4721";
            Assert.AreEqual(hashed, shouldBe);
        }

        [TestMethod]
        public void BigHashingTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            string shouldBe = "77d721c817f9d216c1fb783bcad9cdc20aaa2427402683f1f75dd6dfbe657470";
            Assert.AreEqual(hashed, shouldBe);
        }

        [TestMethod]
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
        public void HashCompareTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(hashed, pass));
        }

        [TestMethod]
        public void BigHashCompareTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsTrue(PasswordHashing.VerifyHashedPassword(hashed, pass));
        }

        [TestMethod]
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
        public void BadHashCompareTest()
        {
            string pass = "abcdef";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsFalse(PasswordHashing.VerifyHashedPassword(hashed, "abcdefg"));
        }

        [TestMethod]
        public void BigBadHashCompareTest()
        {
            string pass = "abcdefghijklmnopqrstuvwxyz1234567890";
            string hashed = PasswordHashing.HashPassword(pass);
            Assert.IsFalse(PasswordHashing.VerifyHashedPassword(hashed, "abcdefghijklmnopqrstuvwxyz1234567890a"));
        }

        [TestMethod]
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
    }
}
