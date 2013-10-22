using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoowooTech.Passport.Model
{
    public class Paging
    {
        public Paging()
        {
            PageSize = 20;
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
        public IEnumerable<T> List { get; set; }
    }
}
