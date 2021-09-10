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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            Session.Abandon();

            return View();
        }

        public ActionResult LoginAction(Home obj)
        {
            if (obj.LoginId == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Index");

            }
            if (obj.Password == null)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Index");
            }
            if (obj.LoginId.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter LoginId";
                return RedirectToAction("Index");

            }
            if (obj.Password.Trim() == "")
            {
                ViewBag.errormsg = "";
                TempData["Login"] = "Please Enter Password";
                return RedirectToAction("Index");

            }

            try
            {

                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() == "1")
                    {
                        ViewBag.errormsg = "";
                        Session["UserID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();

                        return RedirectToAction("DashBoard", "Admin");

                    }

                    if (ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString() != "1")
                    {
                        ViewBag.errormsg = "";
                        Session["ExecutiveID"] = ds.Tables[0].Rows[0]["Pk_Id"].ToString();
                        Session["LoginID"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Username"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["FK_UserTypeID"] = ds.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        return RedirectToAction("EmployeeDashBoard", "Employee");

                    }

                    else
                    {
                        ViewBag.errormsg = "";
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        return RedirectToAction("Index");

                    }

                }
                else
                {
                    ViewBag.errormsg = "";
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.errormsg = "";
                TempData["Login"] = ex.Message;
                return RedirectToAction("Index");

            }





        }
        public ActionResult SendFollowupMail()
        {
            Lead obj = new Lead();
            List<Lead> lst1 = new List<Lead>();
            List<Lead> lstnext = new List<Lead>();
            obj.FromDate = DateTime.Now.ToString("MM/dd/yyyy");
            obj.ToDate = DateTime.Now.ToString("MM/dd/yyyy");
            int i = 0;

            DataSet ds = obj.GetDashBoardDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                string mailbody = "<div class='table-responsive'><table id='file_export' style='border-collapse: collapse;width: 100%;margin-bottom: 1rem;background-color:transparent;padding: 0.75rem; vertical-align: top; border-top: 1px solid #dee2e6; vertical-align: bottom;border-bottom: 2px solid #dee2e6;border-top: 2px solid #dee2e6;background-color: #fff;padding: 0.3rem; border: 1px solid #dee2e6; border: 1px solid #dee2e6;border-bottom-width: 2px;border: 0;background-color: rgba(0, 0, 0, 0.05); background-color: rgba(0, 0, 0, 0.075); background-color: #b8daff; background-color: #9fcdff;background-color: #9fcdff;background-color: #d6d8db;background-color: #c8cbcf;background-color: rgba(255, 255, 255, 0.05); border: 0;border: 1px solid #dee2e6 !important;'><thead style='border:1px solid'>";
                mailbody += "<tr ><th style='width:11%;text-align: center;border: 1px solid;'> Contact Person </th><th style='width:11%;text-align: center;border: 1px solid;'> Contact No. </th><th style='width:11%;text-align: center;border: 1px solid;'> Expected Product</th>";
                mailbody += "<th style='width:11%;text-align: center;border: 1px solid;'>Executive Name</th><th style='width:11%;text-align: center;border: 1px solid;'>Mode Of Interaction</th><th style='width:11%;text-align: center;border: 1px solid;'>Description</th></tr></thead><tbody>";

                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    mailbody += "<tr style='border:1px solid'>";
                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["ContactPerson"].ToString() + "</td>";
                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["ContactNo"].ToString() + "</td>";

                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["ProductCategoryName"].ToString() + "</td>";

                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["Name"].ToString() + "</td>";
                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["InterActionName"].ToString() + "</td>";

                    mailbody += "<td style='width:11%;text-align: center;border: 1px solid;'>" + r["Description"].ToString() + "</td>";
                    mailbody += "</tr>";
                    i++;
                }
                mailbody += "</tbody></table>";

                var fromAddress = new MailAddress("Contact.afluex@gmail.com");
                var toAddress = new MailAddress("Contact.afluex@gmail.com");
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
                    Subject = "Today's TejInfraFollowUp" + DateTime.Now.ToString("dd/MM/yyyy"),
                    Body = mailbody

                })
                    smtp.Send(message);

                //try
                //{
                //    string str2 = "Dear Krishna Murari,you have to follow "+ i +" Leads today.Please check the mail i.e,contact.afluex@gmail.com";
                //    BLSMS.SendSMS("7310000413", str2);
                //    string str = "Dear Alok Chauhan,you have to follow " + i + " Leads today.Please check the mail i.e,contact.afluex@gmail.com";
                //    BLSMS.SendSMS("7310000414", str);
                //    string str1 = "Dear Manish Srivastava,you have to follow " + i + " Leads today.Please check the mail i.e,contact.afluex@gmail.com";
                //    BLSMS.SendSMS("7310000412", str1);

                //}
                //catch { }


            }

            return View();
        }



    }
}
