using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using TejInfraFollowUp.Models;
using TejInfraFollowUp.Filter;
using System.Net.Mail;
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Drawing;

namespace TejInfraFollowUp.Controllers
{
    public class EmployeeRegistrationController : Controller
    {
        //
        // GET: /EmployeeRegistration/

        public ActionResult EmployeeRegistration(string Pk_Id)
        {
            if(Session["UserID"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            EmployeeRegistration model = new EmployeeRegistration();
            #region BindUsertype
            int count = 0;
            List<SelectListItem> ddlUserName = new List<SelectListItem>();
            DataSet ds = model.BindUserType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserName.Add(new SelectListItem { Text = "Select User Type", Value = "0" });
                    }
                    ddlUserName.Add(new SelectListItem { Text = r["UserName"].ToString(), Value = r["Pk_UserTypeID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserName = ddlUserName;

            #endregion BindUsertype
            if (Pk_Id != null)
            {
                EmployeeRegistration obj = new EmployeeRegistration();
                try
                {
                    obj.Pk_Id = Pk_Id;
                    DataSet ds1 = obj.GetEmployeeList();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_Id = ds1.Tables[0].Rows[0]["Pk_Id"].ToString();
                        obj.Fk_UserTypeId = ds1.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        obj.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                        obj.ContactNo = ds1.Tables[0].Rows[0]["ContactNo"].ToString();
                        obj.EmailId = ds1.Tables[0].Rows[0]["EmailId"].ToString();
                        obj.Address = ds1.Tables[0].Rows[0]["Address"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    TempData["ServiceError"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEmployeeRegistration(EmployeeRegistration obj, HttpPostedFileBase postedFile)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["ServiceError"] == null)
            {
                ViewBag.errormsg = "none";

            }
            string FormName = "";
            string Controller = "";
            try 
            {
                if (postedFile != null)
                {
                    obj.UserImage = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.UserImage)));
                }

                string mailbody = "";
                obj.CreatedBy = Session["UserID"].ToString();
                DataSet ds = obj.SaveEmployeeRegistration();
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                               string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                                BLSMS.SendSMS(obj.ContactNo, str2);
                           
                            try
                            {
                               
                                mailbody =  obj.EmailId+";"+  BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), ds.Tables[0].Rows[0]["Password"].ToString());
                                var fromAddress = new MailAddress("contact.afluex@gmail.com");
                                var toAddress = new MailAddress(obj.EmailId);
                              
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
                                    Subject = "Registration",
                                    Body = mailbody,
                                })
                                    smtp.Send(message);
                            }
                            catch (Exception ex)
                            {

                            }
                            finally
                            {

                            }
                        }
                        catch { }

                        TempData["ServiceError"] = "Employee Registration Successfully!\nName : " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Login ID : " + ds.Tables[0].Rows[0]["LoginId"].ToString() + ", Password : " + ds.Tables[0].Rows[0]["Password"].ToString();
                        FormName = "EmployeeRegistration";
                        Controller = "EmployeeRegistration";
                    }
                    else 
                    {
                        TempData["ServiceError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EmployeeRegistration";
                        Controller = "EmployeeRegistration";
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["ServiceError"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GetEmpolyeeRegistrationList()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult DeleteEmployeeRegistration(string Pk_Id)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            EmployeeRegistration obj = new EmployeeRegistration();
            try
            {

                obj.DeletedBy = Session["UserID"].ToString();
                obj.Pk_Id = Pk_Id;
                DataSet ds = new DataSet();


                ds = obj.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["EmployeeDelete"] = "Employee Deleted Successfully";


                    }
                    else
                    {
                        TempData["EmployeeDelete"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EmployeeDelete"] = ex.Message;

            }
            ViewBag.saverrormsg = "";
            return RedirectToAction("GetEmpolyeeRegistrationList");
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateEmployeeRegistration(EmployeeRegistration obj, string Pk_Id, HttpPostedFileBase postedFile)
        {
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (postedFile != null)
                {
                    obj.UserImage = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.UserImage)));
                }

                obj.UpdatedBy = Session["UserID"].ToString();
                obj.Pk_Id = obj.Pk_Id;
                DataSet ds = new DataSet();
                ds = obj.UpdateEmployeeRegistration();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["ServiceError"] = "Employee Registration Updated Successfully";
                    }
                    else
                    {
                        TempData["ServiceError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceError"] = ex.Message;
            }
            return RedirectToAction("EmployeeRegistration");
        }
        [HttpPost]
        [ActionName("GetEmpolyeeRegistrationList")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult FilterEmployee(EmployeeRegistration model)
        {
            List<EmployeeRegistration> lst = new List<EmployeeRegistration>();
            try
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                DataSet ds = model.FilterEmployee();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                       
                        EmployeeRegistration obj = new EmployeeRegistration();
                        obj.Pk_Id = r["Pk_Id"].ToString();
                        obj.Fk_UserTypeId = r["UserName"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.ContactNo = r["ContactNo"].ToString();
                        obj.EmailId = r["EmailId"].ToString();
                        obj.Address = r["Address"].ToString();
                        obj.UserImage = string.IsNullOrEmpty(r["UserImage"].ToString()) ? " ../SoftwareImages/d2.jpg" : r["UserImage"].ToString();
                        lst.Add(obj);
                    }
                    model.lstemployee = lst;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }
        public ActionResult VisitorForm()
        {
            Master model = new Master();
            int count1 = 0;
            if(Session["ExecutiveID"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<SelectListItem> ddlsite = new List<SelectListItem>();
            DataSet ds = model.GetSiteName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //if (count1 == 0)
                    //{
                    //    ddlsite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    //}
                    ddlsite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["SiteName"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlsite = ddlsite;

            int count2 = 0;
            List<SelectListItem> ddlCategoryName = new List<SelectListItem>();
            DataSet ds1 = model.GetCategoryName();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlCategoryName.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategoryName.Add(new SelectListItem { Text = dr["CategoryName"].ToString(), Value = dr["Pk_CategoryId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlCategoryName = ddlCategoryName;
            return View();
        }
        [HttpPost]
        public JsonResult AddProfile(Master userDetail)
        {
            
            var profile = Request.Files;
            bool status = false;
            var datavalue = Request["dataValue"];
            //string imgname = string.Empty;
            //string ImageName = string.Empty;
            //HttpPostedFileBase postedFile = Request.Files["Image"];
            //if (postedFile != null)
            //{
            //    userDetail.Image = "../VisitorImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
            //    postedFile.SaveAs(Path.Combine(Server.MapPath(userDetail.Image)));
            //}
            //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            //    var logo = System.Web.HttpContext.Current.Request.Files["Image"];
            //    if (logo.ContentLength > 0)
            //    {
            //        var profileName = Path.GetFileName(logo.FileName);
            //        var ext = Path.GetExtension(logo.FileName);

            //        ImageName = profileName;

            //        var comPath = Server.MapPath("../VisitorImage/") + ImageName;

            //        logo.SaveAs(comPath);
            //        userDetail.Image = comPath;
            //    }

            //}
            //else
            //userDetail.Image = Server.MapPath("../VisitorImage/") + "profile.jpg";
            userDetail.VisitDate = string.IsNullOrEmpty(userDetail.VisitDate) ? null : Common.ConvertToSystemDate(userDetail.VisitDate, "dd/MM/yyyy");
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(Request["dataValue"]);
            DataTable VisitorDetails = new DataTable();
            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            userDetail.dtVisitorDetails = VisitorDetails;
            //userDetail.AddedBy = Session["ExecutiveID"].ToString();
            userDetail.CreatedBy = Session["ExecutiveID"].ToString();
            DataSet ds = new DataSet();
            ds = userDetail.SaveVisitorDetails();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Visitor"] = "Visitor Details saved successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult PrintVisitor(string Id)
        {
            if (Session["ExecutiveID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Master newdata = new Master();
            List<Master> lstvisitor = new List<Master>();
            newdata.VisitorId = Crypto.Decrypt(Id);
            DataSet ds = newdata.VisitorListById();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.PK_VisitorID = ds.Tables[0].Rows[0]["PK_VisitorMasterID"].ToString();
                ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                //ViewBag.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                ViewBag.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();
                //ViewBag.Image = ds.Tables[0].Rows[0]["VisitorImage"].ToString();
                ViewBag.VehicleDetails = ds.Tables[0].Rows[0]["VehicleDetails"].ToString();
                ViewBag.PickUpLocation = ds.Tables[0].Rows[0]["PickUpLocation"].ToString();
                ViewBag.DropLocation = ds.Tables[0].Rows[0]["DropLocation"].ToString();
                ViewBag.PickUpTime = ds.Tables[0].Rows[0]["PickUpTime"].ToString();
                ViewBag.DropTime = ds.Tables[0].Rows[0]["DropTime"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Master obj = new Master();
                        obj.Name = r["Name"].ToString();
                        obj.MobileNo = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        lstvisitor.Add(obj);
                    }
                }
                newdata.VisitorList = lstvisitor;
            }
            return View(newdata);
        }
        public ActionResult GetVisitorDetails()
        {
            if (Session["ExecutiveID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Master model = new Master();
            //List<Master> lst = new List<Master>();
            //DataSet dss = model.GetVisitorDetails();
            //if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dss.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.VisitorId = r["PK_VisitorDetailsID"].ToString();
            //obj.SiteName = r["SiteName"].ToString();
            //obj.AssociateID = r["LoginId"].ToString();
            //obj.AssociateName = r["AssociateName"].ToString();
            //obj.Amount = r["Amount"].ToString();
            //obj.VisitDate = r["VisitDate"].ToString();
            //obj.VisitorImage = r["VisitorImage"].ToString();
            //        obj.Name = r["Name"].ToString();
            //        obj.MobileNo = r["Mobile"].ToString();
            //        obj.Address = r["Address"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.VisitorList = lst;
            //}
            return View();
        }
        [HttpPost]
        [ActionName("GetVisitorDetails")]
        public ActionResult SearchVisitorDetails(Master model)
        {
            if (Session["ExecutiveID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Master> lst = new List<Master>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.CreatedBy = Session["ExecutiveID"].ToString();
            DataSet ds = model.GetVisitorDetailsforEmployee();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_VisitorMasterID"].ToString());
                    //obj.VisitorId = r["PK_VisitorMasterID"].ToString();
                    // obj.VisitorImage = r["VisitorImage"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.VehicleDetails = r["VehicleDetails"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    // obj.VisitDate = r["VisitDate"].ToString();
                    obj.VisitDate = r["VisitDate"].ToString();
                    //obj.VisitorImage = r["VisitorImage"].ToString();
                    obj.VehicleNumber = r["VehicleDetails"].ToString();
                    //obj.Name = r["Name"].ToString();
                    obj.PickUpLocation = r["PickUpLocation"].ToString();
                    obj.DropLocation = r["DropLocation"].ToString();
                    obj.PickUpTime = r["PickUpTime"].ToString();
                    obj.DropTime = r["DropTime"].ToString();
                    lst.Add(obj);
                }
                model.VisitorList = lst;
            }
            return View(model);
        }
        public ActionResult GetAssociateName(string AssociateID)
        {
            try
            {
                if (Session["ExecutiveID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                Master model = new Master();
                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult AddDocuments(EmployeeRegistration formData)
        {
            var profile = Request.Files;
            bool status = false;
            var datavalue = Request["dataValue"];
            string imgname = string.Empty;
            string ImageName = string.Empty;
            HttpPostedFileBase postedFile = Request.Files["Image"];
            if (postedFile != null)
            {
                formData.Image = "../UploadImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Path.Combine(Server.MapPath(formData.Image)));
            }
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var logo = System.Web.HttpContext.Current.Request.Files["Image"];
                if (logo.ContentLength > 0)
                {
                    var profileName = Path.GetFileName(logo.FileName);
                    var ext = Path.GetExtension(logo.FileName);

                    ImageName = profileName;

                    var comPath = Server.MapPath("../UploadImage/") + ImageName;

                    logo.SaveAs(comPath);
                    formData.Image = comPath;
                }

            }
            else
                formData.Image = Server.MapPath("../UploadImage/") + "profile.jpg";

            formData.Date = string.IsNullOrEmpty(formData.Date) ? null : Common.ConvertToSystemDate(formData.Date, "dd/MM/yyyy");
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(Request["dataValue"]);
            DataTable VisitorDetails = new DataTable();
            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            // userDetail.dtVisitorDetails = VisitorDetails;
            formData.AddedBy = Session["ExecutiveID"].ToString();
            DataSet ds = new DataSet();
            ds = formData.SaveEmployeeDocuments();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["Employee"] = "saved Details  successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return new JsonResult { Data = new { status = status } };
        }
        public ActionResult index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddProfileee(HttpPostedFileBase file,EmployeeRegistration userDetail)
        {
            try
            { 
            if (file != null)
            {
                userDetail.file = "../UploadImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(userDetail.file)));
            }
            userDetail.Pk_Id = Request["Pk_Id"];
            userDetail.Date = string.IsNullOrEmpty(userDetail.Date) ? null : Common.ConvertToSystemDate(userDetail.Date, "dd/MM/yyyy");
            userDetail.AddedBy = Session["UserID"].ToString();
            DataSet ds = userDetail.SaveEmployeeDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    userDetail.Result ="1";
                    TempData["Image"] = "Document Save  Successfully";
                }
                else
                {
                        userDetail.Result = "0";
                        TempData["Image"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            }
            catch (Exception ex)
            {
                TempData["Image"] = ex.Message;
            }
            return Json(userDetail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DocumentAndDateDetails(EmployeeRegistration model)
        {
            List<EmployeeRegistration> lstDocument = new List<EmployeeRegistration>();
            DataSet ds = model.DocumentAndDateDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EmployeeRegistration obj = new EmployeeRegistration();
                    obj.PK_EmployeeDocumentId = r["PK_EmployeeDocumentId"].ToString();
                    obj.Loginid = r["LoginId"].ToString();
                    obj.file = r["UpLoadDocument"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Date = r["UpLoadDocumentDate"].ToString();
                    lstDocument.Add(obj);
                }
                model.lstDocumentAndDate = lstDocument;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("DocumentAndDateDetails")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DocumentAndDateDetailsbyId(EmployeeRegistration model)
        {
            List<EmployeeRegistration> lstDocument = new List<EmployeeRegistration>();
            DataSet ds = model.DocumentAndDateDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EmployeeRegistration obj = new EmployeeRegistration();
                    obj.Loginid = r["LoginId"].ToString();
                    obj.PK_EmployeeDocumentId = r["PK_EmployeeDocumentId"].ToString();
                    obj.file = r["UpLoadDocument"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Date = r["UpLoadDocumentDate"].ToString();
                    lstDocument.Add(obj);
                }
                model.lstDocumentAndDate = lstDocument;
            }
            return View(model);
        }
    }
 }
