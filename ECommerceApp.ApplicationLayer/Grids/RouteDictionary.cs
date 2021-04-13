using ECommerceApp.ApplicationLayer.Extensions;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Grids
{
    public class RouteDictionary : Dictionary<string, string>
    {
        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public string SortField
        {
            get => Get(nameof(SGridDTO.SortField));
            set => this[nameof(SGridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(SGridDTO.SortDirection));
            set => this[nameof(SGridDTO.SortDirection)] = value;
        }

        public string CategoryFilter
        {
            get => Get(nameof(SProductGridDTO.Category))?.Replace(FilterPrefix.Category, "");
            set => this[nameof(SProductGridDTO.Category)] = value;
        }

        public string UnitPriceFilter
        {
            get => Get(nameof(SProductDTO.UnitPrice))?.Replace(FilterPrefix.UnitPrice, "");
            set => this[nameof(SProductDTO.UnitPrice)] = value;
        }

        public string SellerFilter
        {
            get
            {
                //author-8-around-the-world
                string s = Get(nameof(SProductDTO.AppUsers))?.Replace(FilterPrefix.AppUser, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }


            set => this[nameof(SProductDTO.AppUsers)] = value;
        }

        public void ClearFilters() => CategoryFilter = UnitPriceFilter = SellerFilter = SProductGridDTO.DefaultFilter;

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(SGridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) && current.SortDirection == "asc")
                this[nameof(SGridDTO.SortDirection)] = "desc";
            else
                this[nameof(SGridDTO.SortDirection)] = "asc";
        }

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }

            return clone;
        }
    }
}
