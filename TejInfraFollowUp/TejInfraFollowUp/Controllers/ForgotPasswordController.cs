using TejInfraFollowUp.Filter;
using TejInfraFollowUp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;



namespace TejInfraFollowUp.Controllers
{
    public class ForgotPasswordController : Controller
    {
        //
        // GET: /ForgotPassword/

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        [OnAction(ButtonName = "btnReset")]
        public ActionResult ValidatePassword(ForgotPassword model)
        {
            DataSet ds = model.PasswordForget();
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    string mailbody = "";
                    try
                    {
                        mailbody = "Dear Member, your Passoword is : " + ds.Tables[0].Rows[0]["Password"].ToString();

                        var fromAddress = new MailAddress("prakher.afluex@gmail.com");
                        var toAddress = new MailAddress(model.EmailId);

                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, "Baby8542816119")

                        };

                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            IsBodyHtml = true,
                            Subject = "Recover Password",
                            Body = mailbody
                        })
                            smtp.Send(message);
                        TempData["Login"] = "Your Password Has Been Send On your EmailId";
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                    }
                }
                else
                {
                    TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            return RedirectToAction("ForgotPassword");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword(ForgotPassword model)
        {
            try
            {
                if (Session["ExecutiveID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                model.UpdatedBy = Session["ExecutiveID"].ToString();
                //model.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Password Changed  Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ChangePassword", "ForgotPassword");

           
        }




    }
}
