using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SetPage<T>(this IOrderedQueryable<T> query, Paging page)
        {
            if (page != null)
            {
                if (page.RecordCount == 0)
                {
                    page.RecordCount = query.Count();

                    return query.Skip(page.PageSize * (page.CurrentPage - 1)).Take(page.PageSize);
                }
            }
            return query;
        }
    }
}
