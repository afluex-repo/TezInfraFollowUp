using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TejInfraFollowUp.Filter;
using TejInfraFollowUp.Models;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace TejInfraFollowUp.Controllers
{
    public class EmployeeProspectController : EmployeeBaseController
    {
        // GET: EmployeeProspect
        public ActionResult EmployeeProspect( string Pk_ProcpectId)
        {

            EmployeeProspect model = new EmployeeProspect();

            #region BindCategory
            int count = 0;
            List<SelectListItem> ddlcategory = new List<SelectListItem>();
            DataSet ds = model.BingCategory();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlcategory.Add(new SelectListItem { Text = "Select Category Type", Value = "0" });
                    }
                    ddlcategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["Pk_CategoryId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlcategory = ddlcategory;
            #endregion BindCategory

            #region BindIntraction
            int count1 = 0;
            List<SelectListItem> ddlInteraction = new List<SelectListItem>();
            DataSet ds1 = model.BindInteraction();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlInteraction.Add(new SelectListItem { Text = "Select InterAction Type", Value = "0" });
                    }
                    ddlInteraction.Add(new SelectListItem { Text = r["InterActionName"].ToString(), Value = r["PK_InterActionId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlInteraction = ddlInteraction;
            #endregion BindIntraction

            #region BindExpected
            int count2 = 0;
            List<SelectListItem> ddlExpected = new List<SelectListItem>();
            DataSet ds2 = model.BindExpected();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlExpected.Add(new SelectListItem { Text = "Select Product Type", Value = "0" });
                    }
                    ddlExpected.Add(new SelectListItem { Text = r["ProductCategoryName"].ToString(), Value = r["Pk_ProductCategoryId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlExpected = ddlExpected;
            #endregion BindExpected

            #region BindSource
            int count3 = 0;
            List<SelectListItem> ddlSource = new List<SelectListItem>();
            DataSet ds3 = model.BindSource();
            if (ds3
                != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlSource.Add(new SelectListItem { Text = "Select Source Type", Value = "0" });
                    }
                    ddlSource.Add(new SelectListItem { Text = r["SourceName"].ToString(), Value = r["Pk_SourceId"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlSource = ddlSource;
            #endregion BindSource

            #region BindExecutive
            int count4 = 0;
            List<SelectListItem> ddlExecutive = new List<SelectListItem>();
            DataSet ds4 = model.BindExecutive();
            if (ds4
                != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count4 == 0)
                    {
                        ddlExecutive.Add(new SelectListItem { Text = "Select Executive Type", Value = "0" });
                    }
                    ddlExecutive.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                    count4 = count4 + 1;
                }
            }
            ViewBag.ddlExecutive = ddlExecutive;
            #endregion BindExecutive

            if (Pk_ProcpectId != null)
            {
                List<EmployeeProspect> lst = new List<EmployeeProspect>();
                EmployeeProspect obj = new EmployeeProspect();
                try
                {
                    obj.Pk_ProcpectId = Pk_ProcpectId;
                    DataSet ds6 = obj.ProspectList();
                    if (ds6 != null && ds6.Tables.Count > 0 && ds6.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ProcpectId = ds6.Tables[0].Rows[0]["Pk_ProcpectId"].ToString();
                        obj.ContactPerson = ds6.Tables[0].Rows[0]["ContactPerson"].ToString();
                        obj.ContactEmailId = ds6.Tables[0].Rows[0]["ContactEmailId"].ToString();
                        obj.ContactNo = ds6.Tables[0].Rows[0]["ContactNo"].ToString();
                        obj.Fk_IndustryCategoryId = ds6.Tables[0].Rows[0]["Fk_IndustryCategoryId"].ToString();
                        obj.CompanyName = ds6.Tables[0].Rows[0]["CompanyName"].ToString();
                        obj.CompanyContactNo = ds6.Tables[0].Rows[0]["CompanyContactNo"].ToString();
                        obj.Address = ds6.Tables[0].Rows[0]["Address"].ToString();
                        obj.Pincode = ds6.Tables[0].Rows[0]["Pincode"].ToString();
                        obj.State = ds6.Tables[0].Rows[0]["statename"].ToString();
                        obj.City = ds6.Tables[0].Rows[0]["Districtname"].ToString();
                        obj.Designation = ds6.Tables[0].Rows[0]["Designation"].ToString();
                        obj.WebSite = ds6.Tables[0].Rows[0]["WebSite"].ToString();
                        obj.SkypeId = ds6.Tables[0].Rows[0]["SkypeId"].ToString();
                        obj.FacebookId = ds6.Tables[0].Rows[0]["FacebookId"].ToString();
                        obj.TwitterId = ds6.Tables[0].Rows[0]["TwitterId"].ToString();
                        obj.LinkedInId = ds6.Tables[0].Rows[0]["LinkedInId"].ToString();
                        obj.ApproximateEmployee = ds6.Tables[0].Rows[0]["ApproximateEmployee"].ToString();
                        obj.ApproximateCompanyTurnOver = ds6.Tables[0].Rows[0]["ApproximateCompanyTurnOver"].ToString();
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

        public ActionResult GetStateCity(string PinCode)
        {
            EmployeeProspect obj = new EmployeeProspect();
            obj.Pincode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["StateName"].ToString();
                obj.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("EmployeeProspect")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveProspect(EmployeeProspect obj)
        {

            if (TempData["ProcpectError"] == null)
            {
                ViewBag.errormsg = "none";
            }
            obj.FirstInstructionDate = string.IsNullOrEmpty(obj.FirstInstructionDate) ? null : Common.ConvertToSystemDate(obj.FirstInstructionDate, "dd/MM/yyyy");
            obj.FollowupDate = string.IsNullOrEmpty(obj.FollowupDate) ? null : Common.ConvertToSystemDate(obj.FollowupDate, "dd/MM/yyyy");
            try
            {
                obj.AddedBy = Session["ExecutiveID"].ToString();
                DataSet ds = obj.SaveProspect();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds != null && ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ProcpectError"] = "Prospect Save  Successfully";

                    }
                    else
                    {
                        TempData["ProcpectError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ProcpectError"] = ex.Message;
            }
            return RedirectToAction("EmployeeProspect");
        }

        public ActionResult GetEmployeeProspectList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("GetEmployeeProspectList")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetEmployeeProspectList(EmployeeProspect model)
        {

            List<EmployeeProspect> lst = new List<EmployeeProspect>();
            try
            {
                model.EmployeeId = Session["ExecutiveID"].ToString();
                model.Pk_ProcpectId = model.Pk_ProcpectId;
                DataSet ds = model.ProspectList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        EmployeeProspect obj = new EmployeeProspect();
                        obj.Pk_ProcpectId = r["Pk_ProcpectId"].ToString();
                        obj.ContactPerson = r["ContactPerson"].ToString();
                        obj.ContactEmailId = r["ContactEmailId"].ToString();
                        obj.ContactNo = r["ContactNo"].ToString();
                        obj.Fk_IndustryCategoryId = r["CategoryName"].ToString();
                        obj.CompanyName = r["CompanyName"].ToString();
                        obj.CompanyContactNo = r["CompanyContactNo"].ToString();
                        obj.Address = r["Address"].ToString();
                        lst.Add(obj);
                    }
                    model.lstProcpect = lst;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);

        }

        public ActionResult DeleteProspect(string Pk_ProcpectId)
        {
            EmployeeProspect obj = new EmployeeProspect();
            try
            {

                obj.DeletedBy = Session["ExecutiveID"].ToString();
                obj.Pk_ProcpectId = Pk_ProcpectId;
                DataSet ds = new DataSet();


                ds = obj.DeleteProspect();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["ProcpectDelete"] = "ProsPect Deleted Successfully";


                    }
                    else
                    {
                        TempData["ProcpectDelete"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ProcpectDelete"] = ex.Message;

            }
            ViewBag.saverrormsg = "";
            return RedirectToAction("GetEmployeeProspectList");
        }

        [HttpPost]
        [ActionName("EmployeeProspect")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProspect(EmployeeProspect obj, string Pk_ProcpectId)
        {
            try
            {
                obj.UpdatedBy = Session["ExecutiveID"].ToString();
                obj.Pk_ProcpectId = obj.Pk_ProcpectId;
                DataSet ds = new DataSet();
                ds = obj.UpdateProspect();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["ProcpectError"] = "Prospect Updated Successfully";
                    }
                    else
                    {
                        TempData["ProcpectError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ProcpectError"] = ex.Message;
            }
            return RedirectToAction("EmployeeProspect");
        }

    }
}