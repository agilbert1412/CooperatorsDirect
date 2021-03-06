﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CooperatorsDirect.Models;

namespace CooperatorsDirect.Security
{
    public static class SessionPersiter
    {
        static string usernameSessionVar = "user";
        public static User User
        {
            get
            {
                if (HttpContext.Current == null) { return null; }
                var sessionVar = HttpContext.Current.Session[usernameSessionVar];
                if (sessionVar != null)
                {
                    return sessionVar as User;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionVar] = value;
            }
        }
    }
}