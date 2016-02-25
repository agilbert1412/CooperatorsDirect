using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CooperatorsDirect.Security;
using CooperatorsDirect.DAL;

namespace CooperatorsDirect.Models
{
    public class UserModel : IUserRepository
    {

        List<User> listUser;
        CooperatorsContext CooperatorsContext;

        public UserModel()
        {
            CooperatorsContext = new CooperatorsContext();
            listUser = CooperatorsContext.Clients.ToList();
        }

        public User Find(string email)
        {
            if (email == string.Empty || email == null)
            {
                return null;
            }
            try
            {
                return listUser.Where(u => u.Email.ToLower().Equals(email.ToLower())).First();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<User> GetAll()
        {
            return listUser;
        }

        public User Get(string email, string password)
        {
            if ((email == string.Empty || email == null) || (password == string.Empty || password == null))
            {
                return null;
            }
            try
            {
                User userConnected = listUser.Where(u => (u.Email.ToLower().Equals(email.ToLower()) || u.NoPolice.ToLower().Equals(email.ToLower())) && PasswordHashing.VerifyHashedPassword(u.Password, password)).First();
                if (userConnected != null)
                {
                    SessionPersiter.User = userConnected;
                }
                return userConnected;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool Delete(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                var userListdelete = listUser.Find(u => u.Email == email);
                listUser.Remove(userListdelete);
                var userDelete = CooperatorsContext.Clients.Find(email);
                CooperatorsContext.Clients.Remove(userDelete);
                CooperatorsContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Insert(User user)
        {
            try
            {
                if (user.Password != string.Empty && user.Password != null)
                {
                    user.Password = PasswordHashing.HashPassword(user.Password);
                }
                listUser.Add(user);
                CooperatorsContext.Add(user);
                CooperatorsContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Edit(User user)
        {
            try
            {
                listUser[listUser.FindIndex(u => u.Email == user.Email)] = user;
                CooperatorsContext.Entry(CooperatorsContext.Clients.First(u => u.Email == user.Email)).CurrentValues.SetValues(user);
                CooperatorsContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}