using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }

        public int Quantity { get; set; }

        public bool Submitted { get; set; } = false;

        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("User")]
        public string UserFK { get; set; }

        public virtual Product Product { get; set; }
        [ForeignKey("Product")]
        public int ProductFK { get; set; }
    }
}
