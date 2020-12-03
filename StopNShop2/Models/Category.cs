using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }
    }
}
