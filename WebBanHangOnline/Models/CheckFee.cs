using Microsoft.Owin.BuilderProperties;

namespace WebBanHangOnline.Models
{
    public class CheckFee
    {
        public string province { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string address { get; set; }
    }
}