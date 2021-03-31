using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.DomainLayer.Entities.Concrete
{
    public class Category:BaseEntity<int>
    {
       
        public Category() 
        {
            Products = new List<Product>();
        }
        public string CategoryName { get; set; }

        public virtual List<Product> Products { get; set; }
       

    }
}
