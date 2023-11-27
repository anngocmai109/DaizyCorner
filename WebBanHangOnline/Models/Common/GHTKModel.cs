using System.Collections.Generic;
using System.Configuration;

namespace WebBanHangOnline.Models.Common
{
    public class GHTKFeeRequest
    {
        public string address { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string pick_province { get; set; } = ConfigurationManager.AppSettings["pick_province"];
        public string pick_district { get; set; } = ConfigurationManager.AppSettings["pick_district"];
        public int weight { get; set; } = 0;
        public string deliver_option { get; set; } = ConfigurationManager.AppSettings["deliver_option"];
    }

    public class GHTKFeeReponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public GHTKFee fee { get; set; }
    }
    public class GHTKFee
    {
        public string name { get; set; }
        public decimal fee { get; set; }
        public decimal insurance_fee { get; set; }
        public string include_vat { get; set; }
        public string cost_id { get; set; }
        public string delivery_type { get; set; }
        public string a { get; set; }
        public string dt { get; set; }
        public decimal ship_fee_only { get; set; }
        public string promotion_key { get; set; }
        public bool delivery { get; set; }
        public List<GHTKFeeExtra> extFees { get; set; }
    }

    public class GHTKFeeExtra
    {
        public string display { get; set; }
        public string title { get; set; }
        public int amount { get; set; }
        public string type { get; set; }
    }
}