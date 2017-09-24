using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class Brand
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Origin Country")]
        public string OriginCountry { get; set; }

        public virtual ICollection<Degem> Degems { get; set; }

    }
}