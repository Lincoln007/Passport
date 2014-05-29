using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoowooTech.Passport.Model;

namespace LoowooTech.Passport.Dao
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> SetPage<T>(this IQueryable<T> query, Paging page)
        {
            if (page == null) return query;

            if (page.RecordCount == 0)
            {
                page.RecordCount = query.Count();
            }
            if (page.PageSize > page.RecordCount)
            {
                page.PageSize = page.RecordCount;
            }
            var skip = page.PageSize * (page.CurrentPage - 1);
            return query.Skip(skip).Take(page.PageSize);
        }
    }
}
