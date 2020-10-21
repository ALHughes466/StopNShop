using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class WishList
    {
        [Key]
        public int WishListID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Product Product { get; set; }
    }
}
