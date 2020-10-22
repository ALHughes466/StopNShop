﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Previous Price")]
        public decimal PreviousPrice { get; set; }

        public int Rating { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        [Display(Name = "Free Shipping")]
        public bool FreeShipping { get; set; }

        //Foreign Keys
        public virtual Category Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryFK { get; set; }
    }
}
