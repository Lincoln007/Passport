using Newtonsoft.Json;
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
            CurrentPage = 1;
            PageSize = limit;

        }

        [JsonProperty("total")]
        public int RecordCount { get; set; }

        [JsonProperty("page")]
        public int CurrentPage { get; set; }

        [JsonProperty("pageRows")]
        public int PageSize { get; set; }

        [JsonProperty("pageCount")]
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
        public PagingResult(Paging page, IEnumerable<T> data)
        {
            PageSize = page.PageSize;
            CurrentPage = page.CurrentPage;
            RecordCount = page.RecordCount;
            List = data;
        }

        [JsonProperty("rows")]
        public IEnumerable<T> List { get; set; }
    }
}
