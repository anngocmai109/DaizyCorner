using System;

namespace WebBanHangOnline.Common
{
    public class Pagination
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = "";
        public string OrderByStr { get; set; } = "";
        public Func<int,int, string> Url { get; set; }
    }
}
