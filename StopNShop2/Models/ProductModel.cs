using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ProductModel
    {
        private List<Product> products;

        public List<Product> findAll()
        {
            return this.products;
        }

        public Product find(int id)
        {
            return this.products.Single(p => p.ProductId.Equals(id));
        }
    }
}
