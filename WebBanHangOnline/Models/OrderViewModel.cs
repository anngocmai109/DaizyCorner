using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "Địa chỉ khổng để trống")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành khổng để trống")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Quận/Huyện khổng để trống")]
        public string District { get; set; }

        [Required(ErrorMessage = "Phường/Xã khổng để trống")]
        public string Ward { get; set; }

        public string Email { get; set; }
        public string CustomerId { get; set; }
        public int TypePayment { get; set; }
        public int TypePaymentVN { get; set; }
    }
}