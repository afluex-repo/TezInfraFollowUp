using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TejInfraFollowUp.Models;
using TejInfraFollowUp.Filter;


namespace TejInfraFollowUp.Controllers
{
    public class EmployeeController : EmployeeBaseController
    {
        // GET: Employee
        public ActionResult EmployeeDashBoard()
        {

            Lead obj = new Lead();
            List<Lead> lst1 = new List<Lead>();
            List<Lead> lstnext = new List<Lead>();
            List<Lead> lstpending = new List<Lead>();
            obj.FromDate = DateTime.Now.ToString("MM/dd/yyyy");
            obj.ToDate = DateTime.Now.ToString("MM/dd/yyyy");
            obj.EmployeeId = Session["FK_UserTypeID"].ToString();
            DataSet ds = obj.GetDashBoardDetails();
            if (ds != null && ds.Tables.Count > 0)
            {
                #region TodayFollowup
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {

                        Lead obj1 = new Lead();

                        obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                        obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                        obj1.Fk_SourceId = r["SourceName"].ToString();
                        obj1.Fk_ExecutiveId = r["Name"].ToString();
                        obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                        obj1.FollowupDate = r["FollowupDate"].ToString();
                        obj1.Description = r["Description"].ToString();

                        lst1.Add(obj1);
                    }
                    #endregion TodayFollowup
                    obj.lstLead = lst1;
                }

                #region NextFollowup
                if (ds.Tables[1].Rows.Count > 0)
                {


                    foreach (DataRow r in ds.Tables[1].Rows)

                    {

                        Lead obj1 = new Lead();

                        obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                        obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                        obj1.Fk_SourceId = r["SourceName"].ToString();
                        obj1.Fk_ExecutiveId = r["Name"].ToString();
                        obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                        obj1.FollowupDate = r["FollowupDate"].ToString();
                        obj1.Description = r["Description"].ToString();

                        lstnext.Add(obj1);
                    }
                    #endregion NextFollowup
                    obj.lstnextLead = lstnext;
                }

                #region PendingFollowup
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[2].Rows)
                    {

                        Lead obj1 = new Lead();
                        obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                        obj1.Fk_ProcpectId = r["ContactPerson"].ToString();
                        obj1.FirstInstructionDate = r["FirstInstructionDate"].ToString();
                        obj1.Fk_ExpectedProductCategoryId = r["ProductCategoryName"].ToString();
                        obj1.Fk_SourceId = r["SourceName"].ToString();
                        obj1.Fk_ExecutiveId = r["Name"].ToString();
                        obj1.Fk_ModeInterActionId = r["InterActionName"].ToString();
                        obj1.FollowupDate = r["FollowupDate"].ToString();
                        obj1.Description = r["Description"].ToString();

                        lstpending.Add(obj1);
                    }
                    obj.lstpending = lstpending;
                }
                #endregion PendingFollowup
            }
            return View(obj);
        }
        public ActionResult EmployeeDocuments(EmployeeRegistration model)
        {
            List<EmployeeRegistration> lstDocument = new List<EmployeeRegistration>();
            model.Fk_EmployeeId = Session["ExecutiveID"].ToString();
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