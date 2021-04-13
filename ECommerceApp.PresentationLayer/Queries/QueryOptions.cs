using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceApp.PresentationLayer.Queries
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; // İlgili proprty'e defauld değer geçtik 
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private string[] _includes;
        public string Includes { set => _includes = value.Replace(" ", "").Split(','); }

        public string[] GetIncludes() => _includes ?? new string[0];

        //where
        public WhereClause<T> WhereClause { get; set; }
        public Expression<Func<T, bool>> Where
        {
            set
            {
                if (WhereClause == null)
                {
                    WhereClause = new WhereClause<T>();
                }
                WhereClause.Add(value);
            }
        }

        public bool HasWhere => WhereClause != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }

    public class WhereClause<T> : List<Expression<Func<T, bool>>> { }
}
