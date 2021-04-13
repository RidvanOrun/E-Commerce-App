using ECommerceApp.ApplicationLayer.Extensions;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using ECommerceApp.DomainLayer.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Grids
{
    public class ProductGridBuilder : GridBuilder
    {
        public ProductGridBuilder(ISession session) : base(session) { }

        public ProductGridBuilder(ISession session,
                               SProductGridDTO values,
                               string defaultSortFilter) : base(session, values, defaultSortFilter)
        {
            bool isInitial = values.Category.IndexOf(FilterPrefix.Category) == -1;
            Routes.SellerFilter = (isInitial) ? FilterPrefix.AppUser + values.AppUser : values.AppUser;
            Routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Category : values.Category;
            Routes.UnitPriceFilter = (isInitial) ? FilterPrefix.UnitPrice + values.UnitPrice : values.UnitPrice;

            SaveRouteSegment();
        }

        public void LoadFilterSegment(string[] filter, AppUser appUser)
        {
            if (appUser == null) Routes.SellerFilter = FilterPrefix.AppUser + filter[0];
            else Routes.SellerFilter = FilterPrefix.AppUser + filter[0] + "-" + appUser.FullName.Slug();

            Routes.CategoryFilter = FilterPrefix.Category + filter[1];
            Routes.UnitPriceFilter = FilterPrefix.UnitPrice + filter[2];
        }

        public void ClearFilterSegments() => Routes.ClearFilters();

        //filter flag
        string def = SProductGridDTO.DefaultFilter;
        public bool IsFilterBySeller => Routes.SellerFilter != def;
        public bool IsFilterByCategory => Routes.CategoryFilter != def;
        public bool IsFilterByUnitPrice => Routes.UnitPriceFilter != def;

        //sort flag
        public bool IsSortByGenre => Routes.SortField.EqualsNoCase(nameof(Category));
        public bool IsSortByPrice => Routes.SortField.EqualsNoCase(nameof(Product.UnitPrice));
    }
}
