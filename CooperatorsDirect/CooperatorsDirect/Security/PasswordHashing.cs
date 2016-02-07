using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CooperatorsDirect.Security
{
    public static class PasswordHashing
    {
        public static string HashPassword(string password)
        {
            var message = Encoding.ASCII.GetBytes(password);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            if (String.IsNullOrWhiteSpace(hashedPassword))
            {
                return false;
            }
            return hashedPassword == HashPassword(password);

        }
        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}