using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TejInfraFollowUp.Filter;
using TejInfraFollowUp.Models;
namespace TejInfraFollowUp.Controllers
{
    public class EmployeeLeadController : EmployeeBaseController
    {
        // GET: EmployeeLead
        public ActionResult EmployeeLead()
        {

            #region ddlProspect
            try
            {
                EmployeeLead obj1 = new EmployeeLead();
                int count = 0;
                List<SelectListItem> ddlProspect = new List<SelectListItem>();
                obj1.Fk_ExecutiveId = Session["ExecutiveID"].ToString();
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
                EmployeeLead obj1 = new EmployeeLead();
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
                EmployeeLead obj1 = new EmployeeLead();
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
                EmployeeLead obj1 = new EmployeeLead();
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
                EmployeeLead obj1 = new EmployeeLead();
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
            EmployeeLead model = new EmployeeLead();
            try
            {

                model.Pk_ProcpectId = Pk_ProcpectId;

                List<EmployeeLead> listTeacher = new List<EmployeeLead>();

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
        [ActionName("EmployeeLead")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult LeadMaster(EmployeeLead model)
        {

            try
            {
                model.FirstInstructionDate = Common.ConvertToSystemDate(model.FirstInstructionDate, "dd/MM/yyyy");
                model.FollowupDate = Common.ConvertToSystemDate(model.FollowupDate, "dd/MM/yyyy");
                model.AddedBy = Session["ExecutiveID"].ToString();
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
            return RedirectToAction("EmployeeLead");
        }

        public ActionResult GetEmployeeLeadList()
        {
            EmployeeLead model = new EmployeeLead();
            List<EmployeeLead> lst1 = new List<EmployeeLead>();
            model.AddedBy = Session["ExecutiveID"].ToString();
            DataSet ds = model.LeadList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EmployeeLead obj = new EmployeeLead();
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

                EmployeeLead model = new EmployeeLead();
                model.Pk_LeadeId = Pk_LeadeId;
                model.DeletedBy = Session["ExecutiveID"].ToString();
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
            return RedirectToAction("GetEmployeeLeadList");
        }

    }
}