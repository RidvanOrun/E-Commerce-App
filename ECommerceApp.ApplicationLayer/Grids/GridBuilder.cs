

using ECommerceApp.ApplicationLayer.Extensions;
using ECommerceApp.ApplicationLayer.Model.DTOs;
using Microsoft.AspNetCore.Http;

namespace ECommerceApp.ApplicationLayer.Grids
{
    public class GridBuilder
    {
        private const string RouteKey = "currentroute";

        public RouteDictionary Routes { get; set; }

        public ISession Session { get; set; }


        public GridBuilder(ISession session)
        {
            Session = session;
            Routes = Session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
        }

        public GridBuilder(ISession session, SProductGridDTO values, string defaultSortedField)
        {
            Session = session;
            Routes = new RouteDictionary();
            Routes.SortField = values.SortField ?? defaultSortedField;
            Routes.SortDirection = values.SortDirection;

            SaveRouteSegment();
        }

        public void SaveRouteSegment() => Session.SetObject<RouteDictionary>(RouteKey, Routes);

        public RouteDictionary CurrentRotue => Routes;
    }
}
