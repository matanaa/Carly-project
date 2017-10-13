using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carly.Models
{
    public class Comment
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int DegemID { get; set; }

        //[Required]
        public string Author { get; set; }

        [Display (Name = "Content")]
        [Required]
        public string ContentInfo { get; set; }

        public virtual Degem CommentDegem { get; set; }
    }
}