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

    public class CarDetails
    {
        [Display (Name = "Brand Name")]
        public string brandName { get; set; }

        [Display(Name = "Origin Country")]
        public string originCountry { get; set; }

        [Display(Name = "Model Name")]
        public string DegemName { get; set; }

        [Display(Name = "Color")]
        public string color { get; set; }

        [Display(Name = "Quantity")]
        public int quantity { get; set; }
    }
}