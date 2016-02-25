using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public interface IUserRepository
    {

        User Find(string email);

        List<User> GetAll();

        User Get(string email, string password);

        bool Delete(string email);

        bool Insert(User user);

        bool Edit(User user);

    }
}