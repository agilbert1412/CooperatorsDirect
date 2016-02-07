using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CooperatorsDirect.Models
{
    public enum Roles
    {
        [Display(Name = "Admin")]
        admin,
        [Display(Name = "Employé")]
        employe,
        [Display(Name = "Reparateur")]
        reparateur,
        [Display(Name = "Client")]
        client
    }
}