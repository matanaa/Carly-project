using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class Degem
    {
        public int DegemId { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string DegemName { get; set; }

        [Required]
        public string Color { get; set; }

        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Brand")]
        public int BrandID { get; set; }

        public virtual Brand Brand { get; set; }

        [Display(Name = "Images")]
        public string ImagesLinks { get; set; }

        [Display(Name = "Video")]
        public string VideoLink { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}