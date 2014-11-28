using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace Wizardsgroup.Utilities.Helpers
{
    public static class SMSHelper
    {
        public static void Send(IList<string> mobileNos, string message)
        {
            if (mobileNos.Count > 0)
            {
                foreach (var mobileNo in mobileNos)
                {
                    Send(mobileNo, message);
                }
            }
        }

        public static void Send(string mobileNo, string message)
        {
            var isSmsNotificationEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["SmsNotificationEnabled"].ToString());

            if (isSmsNotificationEnabled)
            {
                var smsGatewayServerBaseAddress = ConfigurationManager.AppSettings["SmsGatewayBaseAddress"].ToString();
                var smsGatewayRequestUri = ConfigurationManager.AppSettings["SmsGatewayRequestUri"].ToString();
                var smsGatewayUsername = ConfigurationManager.AppSettings["SmsGatewayUsername"].ToString();
                var smsGatewayPassword = ConfigurationManager.AppSettings["SmsGatewayPassword"].ToString();

                if (smsGatewayServerBaseAddress.Trim() != string.Empty &
                    smsGatewayRequestUri.Trim() != string.Empty &
                    smsGatewayUsername.Trim() != string.Empty &
                    smsGatewayPassword.Trim() != string.Empty &
                    (mobileNo != null && mobileNo.Trim().Length > 0))
                {
                    try
                    {
                        var client = new HttpClient();
                        //client.BaseAddress = new Uri("http://192.168.10.241");

                        client.BaseAddress = new Uri(smsGatewayServerBaseAddress);
                        string uriString = string.Format("{0}?action=send&username={1}&password={2}&mobile={3}&message={4}",
                            smsGatewayRequestUri.StartsWith("/") ? smsGatewayRequestUri.Substring(1) : smsGatewayRequestUri,
                            smsGatewayUsername,
                            smsGatewayPassword,
                            mobileNo,
                            message);
                        //HttpResponseMessage response = client.GetAsync("/api/index.php?action=send&username=medicard&password=P@$$w0rd&mobile=" + mobileNo + "&message=" + message).Result;
                        HttpResponseMessage response = client.GetAsync(uriString).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("[SMSHelper.Send]:Error: " + ex.InnerException);
                    }
                }
                else
                {
                    if (smsGatewayServerBaseAddress.Trim() == string.Empty ||
                        smsGatewayRequestUri.Trim() == string.Empty ||
                        smsGatewayUsername.Trim() == string.Empty ||
                        smsGatewayPassword.Trim() == string.Empty)
                    {
                        Logger.Log("[SMSHelper.Send]:Configuration: Please check your web.config SMS configuration settings.");
                    }
                }
            }
        }
    }
}