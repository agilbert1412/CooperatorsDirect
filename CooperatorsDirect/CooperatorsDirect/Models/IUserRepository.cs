using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public interface IUserRepository
    {

        Client Find(string email);

        List<Client> GetAll();

        Client Get(string email, string password);

        bool Delete(string email);

        bool Insert(Client user);

        bool Edit(Client user);

    }
}