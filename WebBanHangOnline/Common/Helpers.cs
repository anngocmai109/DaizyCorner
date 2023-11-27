using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Web.Helpers;
using WebBanHangOnline.Models.Common;

namespace WebBanHangOnline.Common
{
    public class Helpers
    {
        public static GHTKFeeReponse GetFreeFromGHTK(GHTKFeeRequest request)
        {
            GHTKFeeReponse responseObj = null;
            var ghtkBaseUrl = ConfigurationManager.AppSettings["ghtk_Api"];
            var ghtkToken = ConfigurationManager.AppSettings["ghtk_Token"];

            string getGHTKFeeUri = $"{ghtkBaseUrl}/services/shipment/fee?";

            string query = $"address={request.address}&province={request.province}&district={request.district}&pick_province={request.pick_province}" +
                           $"&pick_district={request.pick_district}&weight={request.weight}&deliver_option={request.deliver_option}";
            try
            {
                var client = new HttpClient();
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, getGHTKFeeUri + query);
                requestMessage.Headers.Add("Token", ghtkToken);
                var response = client.SendAsync(requestMessage).Result;
                responseObj = JsonConvert.DeserializeObject<GHTKFeeReponse>(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                responseObj = null;
            }
            return responseObj;
        }

    }
}