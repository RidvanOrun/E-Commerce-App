using ECommerceApp.ApplicationLayer.Grids;
using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Models.VMs
{
    public class ProductFilterViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPage { get; set; }

        //Dropdown menüler için
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<AppUser> AppUsers { get; set; }
        public Dictionary<string, string> UnitPrices =>
            new Dictionary<string, string>
            {
                { "under50", "Under $50"},
                { "50to500", "Between $50 to $500"},
                { "over500", "Over $500" }
            };

   
    }
}
