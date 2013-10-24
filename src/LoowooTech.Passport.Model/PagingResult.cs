using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class Paging
    {
        public Paging() : this(0) { }

        public Paging(int start = 0, int limit = 20)
        {
            if (limit == 0)
            {
                limit = int.MaxValue;
            }

            CurrentPage = (int)(start / limit) + start % limit == 0 ? 0 : 1;

        }

        public int RecordCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount
        {
            get
            {
                return (int)(RecordCount / PageSize) + (RecordCount % PageSize) > 0 ? 1 : 0;
            }
        }
    }

    public class PagingResult<T> : Paging
    {
        public PagingResult(Paging page,IEnumerable<T> data)
        {
            PageSize = page.PageSize;
            CurrentPage = page.CurrentPage;
            RecordCount = page.RecordCount;
            List = data;
        }
        public IEnumerable<T> List { get; set; }
    }
}
