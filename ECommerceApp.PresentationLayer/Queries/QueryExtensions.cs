using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Queries
{
    public static class QueryExtensions { public static IQueryable<T> PageBy<T>(this IQueryable<T> items, int pageNumber, int pageSize) => items.Skip((pageNumber - 1) * pageSize).Take(pageSize); }
}
