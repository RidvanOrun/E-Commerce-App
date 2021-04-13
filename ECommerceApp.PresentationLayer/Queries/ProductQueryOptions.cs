using ECommerceApp.ApplicationLayer.Extensions;
using ECommerceApp.ApplicationLayer.Grids;
using ECommerceApp.DomainLayer.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Queries
{
    public class ProductQueryOptions : QueryOptions<Product>
    {
        public void SortFilter(ProductGridBuilder builder)
        {
            //filter

            //if (builder.IsFilterByCategory) Where = b => b.CategoryId == builder.CurrentRotue.CategoryFilter;

            if (builder.IsFilterByUnitPrice)
            {
                if (builder.CurrentRotue.UnitPriceFilter == "under50") Where = b => b.UnitPrice < 50;
                else if (builder.CurrentRotue.UnitPriceFilter == "50to500") Where = b => b.UnitPrice >= 50 && b.UnitPrice <= 500;
                else Where = b => b.UnitPrice > 500;
            }

            if (builder.IsFilterBySeller)
            {
                int id = builder.CurrentRotue.SellerFilter.ToInt();

                if (id > 0) Where = b => b.AppUserToProducts.Any(ba => ba.AppUser.Id == id);
            }

            //sort
            if (builder.IsSortByGenre) OrderBy = b => b.Category.CategoryName;
            else if (builder.IsSortByPrice) OrderBy = b => b.UnitPrice;
            else OrderBy = b => b.ProductName;
        }
    }
}
