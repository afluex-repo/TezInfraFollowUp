using TejInfraFollowUp.Models;
using TejInfraFollowUp.Filter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace TejInfraFollowUp.Controllers
{
    public class AdminController : AdminBaseController
    {

        public ActionResult DashBoard()
        {
            Lead obj = new Lead(); 
            List<Lead> lst1 = new List<Lead>();
            List<Lead> lstnext = new List<Lead>();
            List<Lead> lstpending = new List<Lead>();
            obj.FromDate = DateTime.Now.ToString("MM/dd/yyyy");
            obj.ToDate = DateTime.Now.ToString("MM/dd/yyyy");
            DataSet ds = obj.GetDashBoardDetails();
            if(ds!=null && ds.Tables[0].Rows.Count>0)
            {
                #region TodayFollowup
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    Lead obj1 = new Lead();

                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lst1.Add(obj1);
                }
                #endregion TodayFollowup
                obj.lstLead = lst1;

                
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                #region NextFollowup
                foreach (DataRow r in ds.Tables[1].Rows)
                {

                    Lead obj1 = new Lead();

                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lstnext.Add(obj1);
                }
                #endregion NextFollowup
                obj.lstnextLead = lstnext;
            }

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                #region PendingFollowup
                foreach (DataRow r in ds.Tables[2].Rows)
                {

                    Lead obj1 = new Lead();
                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.ContactNo = r["ContactNo"].ToString();
                    obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj1.Fk_SourceId = r["SourceName"].ToString();
                    obj1.Fk_ExecutiveId = r["Name"].ToString();
                    obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj1.FollowupDate = r["FollowupDate"].ToString();
                    obj1.Description = r["Description"].ToString();
                    obj1.PrevioussDate = r["Previoussdate"].ToString();
                    lstpending.Add(obj1);
                }
                obj.lstpending = lstpending;
                #endregion PendingFollowup
            }
            return View(obj);
        }

        public ActionResult GetSenderEmails()
        {
            Admin model = new Models.Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetSenderEmail();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.SenderEmail = dr["SenderEmail"].ToString();
                    obj.SenderPassword = dr["Password"].ToString();

                    lst.Add(obj);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        #region SaveEmailIDs

        public ActionResult EmailMaster()
        {
            Admin model = new Admin();
            try
            {
                List<Admin> lst = new List<Admin>();
                DataSet ds = model.GetEmailData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.EncryptKey = Crypto.Encrypt(dr["PK_EmailID"].ToString());
                        obj.PK_EmailID = dr["PK_EmailID"].ToString();
                        obj.Name = dr["Name"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.Description = dr["Description"].ToString();

                        lst.Add(obj);
                    }
                    model.lstVendor = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ActionName("EmailMaster")]
        [OnAction(ButtonName = "btnSaveEmail")]
        public ActionResult SaveEmails(Admin model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.SaveEmails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmailData"] = "Email data saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["EmailData"] = "ERROR : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("EmailMaster");
        }

        public ActionResult DeleteEmail(string id)
        {
            Admin model = new Admin();
            try
            {
                model.PK_EmailID = Crypto.Decrypt(id);
                model.AddedBy = Session["UserID"].ToString();

                DataSet ds = model.DeletEmail();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["EmailData"] = "Email deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["EmailData"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EmailData"] = ex.Message;
            }
            return RedirectToAction("EmailMaster");
        }

        #endregion

        #region EmailTemplate

        public ActionResult EmailTemplate()
        {
            return View();
        }

        [HttpPost]
        [ActionName("EmailTemplate")]
        [OnAction(ButtonName = "btnSaveTemplate")]
        public ActionResult SaveEmailTemplate(Admin model, HttpPostedFileBase postedfile)
        {
            try
            {
                if (postedfile != null)
                {
                    model.SelectedFilePath = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedfile.FileName);
                    postedfile.SaveAs(Path.Combine(Server.MapPath(model.SelectedFilePath)));
                }
                model.AddedBy = Session["UserID"].ToString();

                DataSet ds = model.SaveEmailTemplate();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["EmailTemplate"] = "Template saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["EmailTemplate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("EmailTemplate");
        }

        public ActionResult TemplateList()
        {
            Admin model = new Admin();
            try
            {
                DataSet ds = model.GetAllTemplates();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<Admin> lst = new List<Admin>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.EncryptKey = Crypto.Decrypt(r["PK_TemplateID"].ToString());
                        obj.PK_TemplateID = r["PK_TemplateID"].ToString();
                        obj.TemplateSubject = r["TemplateSubject"].ToString();
                        obj.TempalteBody = r["TemplateBody"].ToString();
                        lst.Add(obj);
                    }
                    model.lstTemplates = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        #endregion

        #region SendEmail
        public ActionResult TestEditor()
        {
            Admin model = new Admin();
            try
            {
                model.PK_TemplateID = "3";
                DataSet ds1 = model.GetAllTemplates();

                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    model.Result = "1";
                    model.SelectedFilePath = ds1.Tables[0].Rows[0]["FilePath"].ToString();
                    model.Subject = ds1.Tables[0].Rows[0]["TemplateSubject"].ToString();
                    model.Body = ds1.Tables[0].Rows[0]["TemplateBody"].ToString();
                    model.EmailBodyHTML = ds1.Tables[0].Rows[0]["TemplateBody"].ToString();
                }

                model.SenderEmailDisplay = "contact.afluex@gmail.com";
                //model.SenderEmail = "prakher.afluex@gmail.com";
                //model.SenderPassword = "Baby8542816119";

                List<Admin> lst = new List<Admin>();
                DataSet ds = model.GetEmailData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.Name = dr["Name"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.Description = dr["Description"].ToString();

                        lst.Add(obj);
                    }
                    model.lstVendor = lst;
                }

                int count4 = 0;
                List<SelectListItem> ddlTemplates = new List<SelectListItem>();
                DataSet dsTemplate = model.GetAllTemplates();
                if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTemplate.Tables[0].Rows)
                    {
                        if (count4 == 0)
                        {
                            ddlTemplates.Add(new SelectListItem { Text = "Select Template", Value = "0" });
                        }
                        ddlTemplates.Add(new SelectListItem { Text = r["TemplateSubject"].ToString(), Value = r["PK_TemplateID"].ToString() });
                        count4 = count4 + 1;
                    }
                }
                ViewBag.ddlTemplates = ddlTemplates;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult SendEmail()
        {
            Admin model = new Admin();
            try
            {
                model.SenderEmailDisplay = "contact.afluex@gmail.com";
                //model.SenderEmail = "prakher.afluex@gmail.com";
                //model.SenderPassword = "Baby8542816119";

                List<Admin> lst = new List<Admin>();
                DataSet ds = model.GetEmailData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Admin obj = new Admin();
                        obj.Name = dr["Name"].ToString();
                        obj.Email = dr["Email"].ToString();
                        obj.Description = dr["Description"].ToString();

                        lst.Add(obj);
                    }
                    model.lstVendor = lst;
                }

                int count4 = 0;
                List<SelectListItem> ddlTemplates = new List<SelectListItem>();
                DataSet dsTemplate = model.GetAllTemplates();
                if (dsTemplate != null && dsTemplate.Tables.Count > 0 && dsTemplate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsTemplate.Tables[0].Rows)
                    {
                        if (count4 == 0)
                        {
                            ddlTemplates.Add(new SelectListItem { Text = "Select Template", Value = "0" });
                        }
                        ddlTemplates.Add(new SelectListItem { Text = r["TemplateSubject"].ToString(), Value = r["PK_TemplateID"].ToString() });
                        count4 = count4 + 1;
                    }
                }
                ViewBag.ddlTemplates = ddlTemplates;
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult TemplateChange(string tid)
        {
            Admin model = new Admin();
            try
            {
                model.PK_TemplateID = tid;
                DataSet ds = model.GetAllTemplates();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "1";
                    model.SelectedFilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                    model.Subject = ds.Tables[0].Rows[0]["TemplateSubject"].ToString();
                    model.Body = ds.Tables[0].Rows[0]["TemplateBody"].ToString();
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SendEmail")]
        [OnAction(ButtonName = "btnSendEmail")]
        public ActionResult SendEmailAction(Admin model)
        {
            try
            {
                int ctrCheck = 0;
                string ctrRowCount = Request["hdRows"].ToString();
                string recipientEmail = "";
                string chk = "";

                string signature = "<br/><br/><br/><br/><br/><table><tr><td><img src='https://www.bazarprofit.com/js/afluex-logo.png' /></td><td><b>A.K. Chauhan | 7310000414 / 413 / 412</b><br>Afluex Multiservices LLP<br>supportow@afluex.com<br>www.afluex.com<br>D-54, 2<sup>nd</sup>Floor, Vibhuti Khand, Near OLA Office, Lucknow, UP - 226010<br>We also provide IT Development Services</td></tr></table><br/><u>Terms & Conditions:</u>Read Before Reply</br><ul><li>All Proposed Sites are Subject to availability at the time of final confirmation.</li><li>If the site is booked for less than one month, extension for the same will depend upon availability of the site.</li><li>Tax will be charged extra as per gst applicable.</li><li>Send scanned P.O before start campaign along with signed & stamped on your letterhead.</li><li>Payment 50% Advance before campaign and rest 50% after 15 days or mid of the campaign.</li><li>Project/ Campaign will execute after advance Payment.</li><li>In case of any damage or theft of the flex/media the client shall bear the cost of the flex/media or provide the new flex/media.</li><li>Approval of the proposal or design along with the attachment otherwise the company will not accept the approval.</li><li>Please check everything before approval because once any project have approved other company will not responsible for any loss or mistake.</li><li>All disputes are Subject to Lucknow jurisdiction</li></ul>";

                for (int i = 0; i < int.Parse(ctrRowCount); i++)
                {
                    chk = Request["chkEmail_" + i];
                    if(chk == "on")
                    {
                        recipientEmail += Request["txtEmail_" + i].ToString() + ";";
                    }
                }
                SmtpClient mailServer = new SmtpClient("smtp.gmail.com", 587);
                mailServer.EnableSsl = true;
                mailServer.Credentials = new System.Net.NetworkCredential("afluex.outdoor@gmail.com", "krishna@9919");
                
                MailMessage myMail = new MailMessage();
                myMail.Subject = model.Subject;
                myMail.Body = model.EmailBodyHTML + signature;
                myMail.From = new MailAddress("afluex.outdoor@gmail.com", "Afluex Multiservices LLP");
                myMail.To.Add("supportnow@afluex.com");
                
                myMail.IsBodyHtml = true;
                foreach (var emailid in recipientEmail.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    myMail.Bcc.Add(emailid);
                }

                HttpPostedFileBase file = Request.Files["postedfile"];
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentLength < 12288000)
                    {
                        myMail.Body = "<html><body>" + model.EmailBodyHTML + signature + "</html></body>";
                        string filename = Path.GetFileName(file.FileName);
                        var attachment = new Attachment(file.InputStream, filename);
                        myMail.Attachments.Add(attachment);
                    }
                    else
                    {
                        string uploadFilename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        model.SelectedFilePath = "../EmailAttachments/" + uploadFilename;
                        file.SaveAs(Path.Combine(Server.MapPath(model.SelectedFilePath)));
                        myMail.Body = "<html><body>" + model.EmailBodyHTML;
                        myMail.Body += "<br/> Attachment Link : http://TejInfraFollowUp.afluex.com/EmailAttachments/" + uploadFilename;
                        myMail.Body += signature + "</html></body>";
                    }
                }
                mailServer.Send(myMail);
                ctrCheck++;
                TempData["Class"] = "alert alert-success";
                TempData["SendEmail"] = "Email sent successfully";
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["SendEmail"] = "ERROR : " + ex.Message;
            }
            return RedirectToAction("SendEmail");
        }
        #endregion

    }
}
