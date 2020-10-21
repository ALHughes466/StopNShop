using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }

        public int Quantity { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Product Product { get; set; }
    }
}
