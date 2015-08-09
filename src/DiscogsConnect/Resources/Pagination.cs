using System.Collections.Generic;

namespace DiscogsConnect
{
    public class Pagination
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Items { get; set; }
        public int PerPage { get; set; }
        public PaginationUrls Urls { get; set; }

        public class PaginationUrls
        {
            public string First { get; set; }
            public string Prev { get; set; }
            public string Next { get; set; }
            public string Last { get; set; }
        }
    }

    public class PaginationResponse
    {
        public Pagination Pagination { get; set; }
    }

    public class PaginationResponse<TResult> : PaginationResponse
    {
        public List<TResult> Items { get; set; }
    }
}