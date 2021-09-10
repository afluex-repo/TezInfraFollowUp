using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TejInfraFollowUp.Models
{
    public class EmployeeDWR:Comman
    {
        public string Fk_ProcpectId { get; set; }
        public string Pk_ProcpectId { get; set; }
        public string Fk_ExpectedProductCategoryId { get; set; }
        public string Pk_LeadeId { get; set; }
        public string Fk_LeadId { get; set; }
        public string Fk_ExecutiveId { get; set; }
        public string Fk_SourceId { get; set; }
        public string Fk_ModeInterActionId { get; set; }
        public string FirstInstructionDate { get; set; }
        public string Pk_DwrId { get; set; }
        public string Pk_SourceId { get; set; }
        public string PK_InterActionId { get; set; }
        public string FollowupDate { get; set; }
        public string NextFollowupDate { get; set; }
        public string Remark { get; set; }
        public string Fk_BusinessChanceId { get; set; }
        public string MeetingTime { get; set; }
        public string MeetingDate { get; set; }
        public string Pk_BusinessChanceId { get; set; }
        public List<EmployeeDWR> ddlProspect { get; set; }
        public List<EmployeeDWR> ddlProductCategory { get; set; }
        public List<EmployeeDWR> ddlDataSource { get; set; }
        public List<EmployeeDWR> ddlName { get; set; }
        public List<EmployeeDWR> ddlInterAction { get; set; }
        public List<SelectListItem> ddlLead { get; set; }
        public List<EmployeeDWR> lstDWR { get; set; }
        public string AddedBy { get; set; }
        public string Result { get; internal set; }

        public DataSet GetProspectList()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProcpectId",Pk_ProcpectId ),
                new SqlParameter("@EmployeeId",AddedBy ),
                
                                   };
            DataSet ds = DBHelper.ExecuteQuery("GetProspectList", param);
            return ds;
        }

        public DataSet LeadList()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_LeadeId", Pk_LeadeId) };
            DataSet ds = DBHelper.ExecuteQuery("LeadList", param);
            return ds;
        }

        public DataSet GetLeadByProspect()
        {
            SqlParameter[] param = { new SqlParameter("@Fk_ProcpectId", Fk_ProcpectId) };
            DataSet ds = DBHelper.ExecuteQuery("GetLeadByProspect", param);
            return ds;
        }

        public DataSet ListBusinessChance()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListBusinessChance", param);
            return ds;
        }

        public DataSet ListDataSource()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_SourceId", Pk_SourceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListDataSource", param);
            return ds;
        }

        public DataSet ListInterAction()
        {
            SqlParameter[] param = { new SqlParameter("@PK_InterActionId", PK_InterActionId) };
            DataSet ds = DBHelper.ExecuteQuery("ListInterAction", param);
            return ds;
        }
        public DataSet ListExecutive()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetExecutiveName");
            return ds;
        }
        public DataSet InsertDWR()
        {
            SqlParameter[] param = { new SqlParameter("@Fk_ProcpectId", Fk_ProcpectId),
                                    new SqlParameter("@Fk_LeadId", Fk_LeadId),
                                    new SqlParameter("@NextFollowupDate", NextFollowupDate),
                                    new SqlParameter("@Remark", Remark),
                                    new SqlParameter("@Fk_BusinessChanceId", Fk_BusinessChanceId),
                                    new SqlParameter("@Fk_ModeInterActionId", Fk_ModeInterActionId),
                                    new SqlParameter("@Fk_ExecutiveId", Fk_ExecutiveId),
                                    new SqlParameter("@MeetingDate", MeetingDate),
                                    new SqlParameter("@MeetingTime", MeetingTime),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertDWR", param);
            return ds;

        }

        public DataSet GetDWRList()
        {
            SqlParameter[] param = { new SqlParameter("@Fk_LeadId", Fk_LeadId) };
            DataSet ds = DBHelper.ExecuteQuery("GetDWRList", param);
            return ds;
        }


    }
}