using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class Admin : IdentityDbContext<ApplicationUser>
    {

        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string roleName { get; set; }
    }
}