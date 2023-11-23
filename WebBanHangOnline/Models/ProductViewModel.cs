using System.Collections.Generic;
using WebBanHangOnline.Common;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models
{
    public class ProductViewModel
    {
        public Pagination Pagination { get; set; }
        public List<Product> Products { get; set; }
    }
}
