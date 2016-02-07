using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using CooperatorsDirect.Models;

namespace CooperatorsDirect.Models
{
    public class CustomPrincipal
    {
        private Client User;

        public IIdentity Identity { get; set; }


        public CustomPrincipal(Client user)
        {
            User = user;
            Identity = new GenericIdentity(User.Email);
        }

        public bool IsInRole(Roles[] userRoles)
        {
            return userRoles.Any(ur => ur.ToString().Contains(User.Role.ToString()));
        }
    }
}