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

namespace TejInfraFollowUp.Controllers
{
    public class EmployeeRegistrationController : Controller
    {
        //
        // GET: /EmployeeRegistration/

        public ActionResult EmployeeRegistration(string Pk_Id)
        {
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
            return View();
        }

        public ActionResult DeleteEmployeeRegistration(string Pk_Id)
        {
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
                model.Pk_Id = Session["UserID"].ToString();
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
    }
 }
