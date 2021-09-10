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
    public class DWRController : AdminBaseController
    {
        //
        // GET: /DWR/

        public ActionResult DWRMaster()
        {
           
            #region ddlInterAction
            try
            {
                DWR obj1 = new DWR();
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
                DWR obj1 = new DWR();
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
                DWR obj1 = new DWR();
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
                DWR obj1 = new DWR();
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
                DWR obj1 = new DWR();
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
            List<SelectListItem> ddlLead = new List<SelectListItem>();
            ddlLead.Add(new SelectListItem { Text = "Select Lead", Value = "0" });
            ViewBag.ddlLead = ddlLead;
            return View();
        }

        public ActionResult GetLeadByProspect(string Fk_ProcpectId)
        {
            DWR model = new DWR();
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
        public ActionResult GetLeadDetailsByID(string Pk_LeadeId,string ProspectId)
        {
            DWR model = new DWR();
            try
            {

                model.Pk_LeadeId = Pk_LeadeId;
                model.Fk_ProcpectId = ProspectId;
                List<Lead> list = new List<Lead>();

                DataSet ds = model.LeadList();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                   // model.Fk_ExpectedProductCategoryId = ds.Tables[0].Rows[0]["ProductCategoryName"].ToString();
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
        [ActionName("DWRMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult DWRMaster(DWR model)
        {

            try
            {
                model.MeetingDate = Common.ConvertToSystemDate(model.MeetingDate, "dd/MM/yyyy");
               // model.MeetingTime = Common.ConvertToSystemDate(model.MeetingTime, "dd/MM/yyyy");
                model.NextFollowupDate = Common.ConvertToSystemDate(model.NextFollowupDate, "dd/MM/yyyy");
                model.AddedBy = Session["UserID"].ToString();
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
            return RedirectToAction("DWRMaster");
        }

        public ActionResult ListDWR(string Fk_LeadId)
        {
            DWR model = new DWR();
            List<DWR> lst1 = new List<DWR>();
            model.Fk_LeadId = Fk_LeadId;
            DataSet ds = model.GetDWRList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DWR obj = new DWR();
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
