using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public interface IUserRepository
    {

        User Find(string email);

        List<User> GetAllUsers();

        List<User> GetAllCustomers();

        User GetUser(int id);

        User GetUser(string email, string password);

        bool Delete(string email);

        bool Insert(User user);

        bool Edit(User user);

    }
}