using TejInfraFollowUp.Filter;
using TejInfraFollowUp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TejInfraFollowUp.Controllers
{
    public class LeadController : AdminBaseController
    {
        

        public ActionResult LeadMaster()
        {
            #region ddlProspect
            try
            {
                Lead obj1 = new Lead();
                int count = 0;
                List<SelectListItem> ddlProspect = new List<SelectListItem>();
                DataSet ds1 = obj1.GetProspectList();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlProspect.Add(new SelectListItem { Text = "Select Prospect", Value = "0" });
                        }
                        ddlProspect.Add(new SelectListItem { Text = r["ContactPerson"].ToString(), Value = r["Pk_ProcpectId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlProspect = ddlProspect;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion 

            #region ddlProductCategory
            try
            {
                Lead obj1 = new Lead();
                int count = 0;
                List<SelectListItem> ddlProductCategory = new List<SelectListItem>();
                DataSet ds1 = obj1.ListProductCategory();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlProductCategory.Add(new SelectListItem { Text = "Select Product Category", Value = "0" });
                        }
                        ddlProductCategory.Add(new SelectListItem { Text = r["ProductCategoryName"].ToString(), Value = r["Pk_ProductCategoryId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlProductCategory = ddlProductCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion 

            #region ddlInterAction
            try
            {
                Lead obj1 = new Lead();
                int count = 0;
                List<SelectListItem> ddlInterAction = new List<SelectListItem>();
                DataSet ds1 = obj1.ListInterAction();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlInterAction.Add(new SelectListItem { Text = "Select InterAction", Value = "0" });
                        }
                        ddlInterAction.Add(new SelectListItem { Text = r["InterActionName"].ToString(), Value = r["PK_InterActionId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlInterAction = ddlInterAction;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion 

            #region ddlDataSource
            try
            {
                Lead obj1 = new Lead();
                int count = 0;
                List<SelectListItem> ddlDataSource = new List<SelectListItem>();
                DataSet ds1 = obj1.ListDataSource();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlDataSource.Add(new SelectListItem { Text = "SelectData Source", Value = "0" });
                        }
                        ddlDataSource.Add(new SelectListItem { Text = r["SourceName"].ToString(), Value = r["Pk_SourceId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlDataSource = ddlDataSource;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion 

            #region ddlName
            try
            {
                Lead obj1 = new Lead();
                int count = 0;
                List<SelectListItem> ddlName = new List<SelectListItem>();
                DataSet ds1 = obj1.ListExecutive();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlName.Add(new SelectListItem { Text = "Select Executive Source", Value = "0" });
                        }
                        ddlName.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_Id"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlName = ddlName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
            return View();
        }

        public ActionResult GetProspectDetailsByID(string Pk_ProcpectId)
        {
            Lead model = new Lead();
            try
            {

                model.Pk_ProcpectId = Pk_ProcpectId;

                List<Lead> listTeacher = new List<Lead>();

                DataSet ds = model.GetProspectList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    model.ContactEmailId = ds.Tables[0].Rows[0]["ContactEmailId"].ToString();
                    model.ContactNo = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                    model.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.CompanyContactNo = ds.Tables[0].Rows[0]["CompanyContactNo"].ToString();
                   
                    model.Result = "Yes";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("LeadMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult LeadMaster(Lead model)
        {

            try
            {
                model.FirstInstructionDate = Common.ConvertToSystemDate(model.FirstInstructionDate, "dd/MM/yyyy");
                model.FollowupDate = Common.ConvertToSystemDate(model.FollowupDate, "dd/MM/yyyy");
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertLead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Lead is Successfully Added";
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
            return RedirectToAction("LeadMaster");
        }

        public ActionResult ListLead()
        {
            Lead model = new Lead();
            List<Lead> lst1 = new List<Lead>();

            DataSet ds = model.LeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Lead obj = new Lead();
                    obj.Pk_LeadeId = r["Pk_LeadId"].ToString();
                    obj.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                    obj.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                    obj.Fk_SourceId = r["SourceName"].ToString();
                    obj.Fk_ExecutiveId = r["Name"].ToString();
                    obj.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj.FollowupDate = r["FollowupDate"].ToString();
                    obj.Description = r["Description"].ToString();

                    lst1.Add(obj);
                }
                model.lstLead = lst1;
            }
            return View(model);
        }

        public ActionResult DeleteLead(string Pk_LeadeId)
        { 
        
            try
            {

                Lead model = new Lead();
                model.Pk_LeadeId = Pk_LeadeId;
                model.DeletedBy = Session["UserID"].ToString();
                DataSet ds = model.DeleteLead();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Lead is Successfully Deleted";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListLead");
        }
    

    }
}
