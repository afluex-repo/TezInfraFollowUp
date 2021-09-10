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
    public class EmployeeDWRController : EmployeeBaseController 
    {
        // GET: EmployeeDWR
        public ActionResult EmployeeDWR()
        {

            #region ddlInterAction
            try
            {
                EmployeeDWR obj1 = new EmployeeDWR();
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
                EmployeeDWR obj1 = new EmployeeDWR();
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

            #region ddlChance
            try
            {
                EmployeeDWR obj1 = new EmployeeDWR();
                int count = 0;
                List<SelectListItem> ddlChance = new List<SelectListItem>();
                DataSet ds1 = obj1.ListBusinessChance();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds1.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlChance.Add(new SelectListItem { Text = "Select Business Chance", Value = "0" });
                        }
                        ddlChance.Add(new SelectListItem { Text = r["ChanceName"].ToString(), Value = r["Pk_BusinessChanceId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlChance = ddlChance;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion

            #region ddlName
            try
            {
                EmployeeDWR obj1 = new EmployeeDWR();
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
            #region ddlProspect
            try
            {
                EmployeeDWR obj1 = new EmployeeDWR();
                int count = 0;
                List<SelectListItem> ddlProspect = new List<SelectListItem>();
                obj1.AddedBy = Session["ExecutiveID"].ToString();
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
            List<SelectListItem> ddlLead = new List<SelectListItem>();
            ddlLead.Add(new SelectListItem { Text = "Select Lead", Value = "0" });
            ViewBag.ddlLead = ddlLead;
            return View();
        }
        public ActionResult GetLeadByProspect(string Fk_ProcpectId)
        {
            EmployeeDWR model = new EmployeeDWR();
            try
            {

                model.Fk_ProcpectId = Fk_ProcpectId;

                List<SelectListItem> ddlLead = new List<SelectListItem>();

                DataSet ds = model.GetLeadByProspect();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        ddlLead.Add(new SelectListItem { Text = r["ProductCategoryName"].ToString(), Value = r["Pk_LeadId"].ToString() });

                    }
                }

                model.ddlLead = ddlLead;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeadDetailsByID(string Pk_LeadeId)
        {
            EmployeeDWR model = new EmployeeDWR();
            try
            {

                model.Pk_LeadeId = Pk_LeadeId;

                List<EmployeeDWR> list = new List<EmployeeDWR>();

                DataSet ds = model.LeadList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Fk_ExecutiveId = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.Fk_SourceId = ds.Tables[0].Rows[0]["SourceName"].ToString();
                    model.FirstInstructionDate = ds.Tables[0].Rows[0]["FirstInstructionDate"].ToString();
                    model.FollowupDate = ds.Tables[0].Rows[0]["FollowupDate"].ToString();
                    model.Fk_ModeInterActionId = ds.Tables[0].Rows[0]["InterActionName"].ToString();

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
        [ActionName("EmployeeDWR")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult DWRMaster(EmployeeDWR model)
        {

            try
            {
                model.MeetingDate = Common.ConvertToSystemDate(model.MeetingDate, "dd/MM/yyyy");
                model.NextFollowupDate = Common.ConvertToSystemDate(model.NextFollowupDate, "dd/MM/yyyy");
                model.AddedBy = Session["ExecutiveID"].ToString();
                DataSet ds = model.InsertDWR();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Daily Work Report is Successfully Added";
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
            return RedirectToAction("EmployeeDWR");
        }

        public ActionResult GetEmployeeDWRList(string Pk_DwrId)
        {
            EmployeeDWR model = new EmployeeDWR();
            List<EmployeeDWR> lst1 = new List<EmployeeDWR>();
            model.Fk_LeadId = Pk_DwrId;
            DataSet ds = model.GetDWRList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EmployeeDWR obj = new EmployeeDWR();
                    obj.Pk_DwrId = r["Pk_DwrId"].ToString();
                    obj.Fk_ProcpectId = r["ContactPerson"].ToString();
                    obj.Fk_LeadId = r["ProductCategoryName"].ToString();
                    obj.NextFollowupDate = r["NextFollowupDate"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.Fk_BusinessChanceId = r["ChanceName"].ToString();
                    obj.Fk_ModeInterActionId = r["InterActionName"].ToString();
                    obj.Fk_ExecutiveId = r["Name"].ToString();
                    obj.MeetingDate = r["MeetingDate"].ToString();
                    obj.MeetingTime = r["MeetingTime"].ToString();

                    lst1.Add(obj);
                }
                model.lstDWR = lst1;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

    }
}