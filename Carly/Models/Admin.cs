using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class Admin : IdentityDbContext<ApplicationUser>
    {

        public int id { get; set; }

        [Display(Name = "User Name")]
        public string userName { get; set; }

        [Display(Name = "E-mail Address")]
        public string email { get; set; }

        [Display(Name = "Role")]
        public string roleName { get; set; }
    }
}