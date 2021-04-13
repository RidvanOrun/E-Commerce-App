using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class SGridDTO
    {
        public string SortField { get; set; }
        public string SortDirection { get; set; } = "asc";
    }
}
