using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class Lead:Common
    {
        public List<Lead> lstnextLead { get; set; }

        public string Pk_Id { get; set; }
        public string Fk_ExecutiveId { get; set; }
        public string PK_InterActionId { get; set; }
        public string Fk_ModeInterActionId { get; set; }
        public string Pk_SourceId { get; set; }
        public string Fk_SourceId { get; set; }
        public string Pk_ProductCategoryId { get; set; }
        public string Fk_ExpectedProductCategoryId { get; set; }
        public string FirstInstructionDate { get; set; }
        public string CompanyContactNo { get; set; }
        public string Pk_LeadeId { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string FollowupDate { get; set; }
        public string PrevioussDate { get; set; }
        public string Description { get; set; }
        public string ContactNo { get; set; }
        public string ContactEmailId { get; set; }
        public string ContactPerson { get; set; }
        public string Prospect { get; set; }
        public string Pk_ProcpectId { get; set; }
        public string Fk_ProcpectId { get; set; }
        public List<Lead> ddlProspect { get; set; }
         public List<Lead> ddlProductCategory { get; set; }
        public List<Lead> ddlDataSource { get; set; }
        public List<Lead> ddlName { get; set; }
        public List<Lead> ddlInterAction { get; set; }
        public List<Lead> lstLead { get; set; }
        public List<Lead> lstpending { get; set; }
        public string EmployeeId { get; set; }

        public DataSet GetProspectList()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProcpectId",Pk_ProcpectId )
                                   };
            DataSet ds = DBHelper.ExecuteQuery("GetProspectList", param);
            return ds;
        }

        public DataSet ListProductCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", param);
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

        public DataSet InsertLead()
        {
            SqlParameter[] param = { new SqlParameter("@Fk_ProcpectId",Fk_ProcpectId),
                                   new SqlParameter("@Fk_ExpectedProductCategoryId",Fk_ExpectedProductCategoryId),
                                   new SqlParameter("@Fk_SourceId",Fk_SourceId),
                                   new SqlParameter("@Fk_ExecutiveId",Fk_ExecutiveId),
                                   new SqlParameter("@Fk_ModeInterActionId",Fk_ModeInterActionId),
                                   new SqlParameter("@Description",Description),
                                   new SqlParameter("@FollowupDate",FollowupDate),
                                   new SqlParameter("@FirstInstructionDate",FirstInstructionDate),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertLead", param);
            return ds;
        }


        public DataSet LeadList()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_LeadeId", Pk_LeadeId) };
            DataSet ds = DBHelper.ExecuteQuery("LeadList", param);
            return ds;
        }

        public DataSet DeleteLead()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_LeadeId", Pk_LeadeId),
                                   new SqlParameter("@DeletedBy",DeletedBy)};
            DataSet ds = DBHelper.ExecuteQuery("DeleteLead", param);
            return ds;
        }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet GetDashBoardDetails()
        {
            SqlParameter[] para ={
                                      new SqlParameter ("@FromDate",FromDate),
                                   new SqlParameter ("@ToDate",ToDate),
                                    new SqlParameter ("@EmployeeId",EmployeeId),


                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails", para);
            return ds;
        }

    }
}