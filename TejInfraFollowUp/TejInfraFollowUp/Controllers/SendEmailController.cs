using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TejInfraFollowUp.Models;
using TejInfraFollowUp.Filter;
using System.Net.Mail;
using System.Net;

namespace TejInfraFollowUp.Controllers
{
    public class SendEmailController : AdminBaseController
    {
        //
        // GET: /SendEmail/

        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        [ActionName("SendEmail")]
        [OnAction(ButtonName = "btnProceed")]
        public ActionResult Proceed(SendEmail model)
        {
            SendEmail obj = new SendEmail();

            List<SendEmail> list = new List<SendEmail>();
            try
            {
                DataSet ds = model.GetEmployeeList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SendEmail objroll = new SendEmail();
                        objroll.Pk_Id = r["Pk_Id"].ToString();
                        objroll.Name = r["Name"].ToString();
                        objroll.ContactNo = r["ContactNo"].ToString();
                        objroll.EmailId = r["EmailId"].ToString();

                        list.Add(objroll);
                    }
                    model.lstEmail = list;
                }


            }
            catch (Exception ex)
            {
                TempData["Student"] = ex.Message;
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("SendEmail")]
        [OnAction(ButtonName = "btnSendSms")]
        public ActionResult SendSMS(SendEmail model)
        {
            try
            {
                string check, email, mailbody = "";

                check = Request["chksms"];
                email = Request["chkemail"];

                if (check == "on")
                {
                    try
                    {
                        string noofrows = Request["hdRows"].ToString();
                        string chk = "";
                        for (int i = 1; i < int.Parse(noofrows); i++)
                        {
                            chk = Request["chkSelect_ " + i];
                            if (chk == "on")
                            {
                                try
                                {
                                    string str2 = BLSMS.SendSmsAdmin(Request["hdMobile_ " + i], model.Message);
                                    BLSMS.SendSMS(Request["hdMobile_ " + i], str2);
                                    TempData["Message"] = "Message Send Successfully";
                                }
                                catch (Exception)
                                {

                                }

                            }
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
                if (email == "on")
                {
                    try
                    {
                        string noofrows = Request["hdRows"].ToString();
                        string chk = "";
                        for (int i = 1; i < int.Parse(noofrows); i++)
                        {
                            chk = Request["chkSelect_ " + i];
                            if (chk == "on")
                            {
                                try
                                {
                                    mailbody = "Dear sir, " + model.Message;
                                    var fromAddress = new MailAddress("Contact.afluex@gmail.com");
                                    var toAddress = new MailAddress(Request["hdEmail_ " + i]);
                                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                    {
                                        Host = "smtp.gmail.com",
                                        Port = 587,
                                        EnableSsl = true,
                                        DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                        UseDefaultCredentials = false,
                                        Credentials = new NetworkCredential(fromAddress.Address, "krishna@9919")
                                    };
                                    using (var message = new MailMessage(fromAddress, toAddress)
                                    {
                                        IsBodyHtml = true,
                                        Subject = "Welcome",
                                        Body = mailbody

                                    })
                                        smtp.Send(message);
                                }
                                catch (Exception)
                                {

                                }
                                TempData["Message"] = "Message Send Successfully";
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }

            }
            catch (Exception)
            { }
            return View();
        }
    }
}

