using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ProductReview
    {
        [Key]
        public int ProductReviewID { get; set; }

        public string ReviewDetail { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductFK { get; set; }

        
    }
}
