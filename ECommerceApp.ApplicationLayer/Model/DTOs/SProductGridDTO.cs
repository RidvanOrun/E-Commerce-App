using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceApp.ApplicationLayer.Model.DTOs
{
    public class SProductGridDTO:SGridDTO
    {
        public const string DefaultFilter = "all";

        public string AppUser { get; set; } = "DefaultFilter";
        public string Category { get; set; } = "DefaultFilter";
        public string UnitPrice { get; set; } = "DefaultFilter";
    }
}
