using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class BLSMS
    {
        static public void SendSMS(string Mobile, string Message)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);


                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }

        static public string ForgetPassword(string MemberName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }

        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);
            Message = Message.Replace("[TPassword]", Password);
            return Message;
        }
        static public string SendSmsAdmin(string Mobile, string ctrMessage)
        {
            string Message = ConfigurationSettings.AppSettings["AdminSMS"].ToString();

            Message = Message.Replace("[Message]", ctrMessage);
            return Message;
        }
        static public string ChangePassword(string MemberName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ChangePassword"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }

    }
}