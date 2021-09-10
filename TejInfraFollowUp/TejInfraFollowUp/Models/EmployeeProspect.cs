using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class EmployeeProspect:Comman
    {
        public string Pk_ProcpectId { get; set; }
        public string ContactPerson { get; set; }
        public string ContactEmailId { get; set; }
        public string ContactNo { get; set; }
        public string Fk_IndustryCategoryId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactNo { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Designation { get; set; }
        public string WebSite { get; set; }
        public string SkypeId { get; set; }
        public string FacebookId { get; set; }
        public string LinkedInId { get; set; }
        public string ApproximateEmployee { get; set; }
        public string ApproximateCompanyTurnOver { get; set; }
        public string Fk_ProcPectId { get; set; }
        public string FirstInstructionDate { get; set; }
        public string Fk_ExpectedProductCategoryId { get; set; }
        public string Fk_SourceId { get; set; }
        public string Fk_ExecutiveId { get; set; }
        public string Fk_ModeInterActionId { get; set; }
        public string FollowupDate { get; set; }
        public string Description { get; set; }
        public string TwitterId { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PK_InterActionId { get; set; }
        public List<EmployeeProspect> lstProcpect { get; set; }
        public string Pk_Id { get; set; }
        public string EmployeeId { get; set; }
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string UserImage { get; internal set; }
        public string EmailId { get; internal set; }
        public string Name { get; internal set; }
        public string Fk_UserTypeId { get; internal set; }
        public string CreatedBy { get; internal set; }
        public string Result { get; internal set; }

        public DataSet BingCategory()
        {

            DataSet ds = DBHelper.ExecuteQuery("ListCategory");
            return ds;
        }
        public DataSet BindInteraction()
        {
            DataSet ds = DBHelper.ExecuteQuery("ListInterAction");
            return ds;
        }
        public DataSet BindExpected()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductList");
            return ds;
        }

        public DataSet BindSource()
        {
            DataSet ds = DBHelper.ExecuteQuery("ListDataSource");
            return ds;
        }

        public DataSet BindExecutive()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetExecutiveName");
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para ={new SqlParameter ("@PinCode",Pincode),
                               };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }

   
        public DataSet SaveProspect()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@ContactPerson",ContactPerson),
                                      new SqlParameter("@ContactEmailId",ContactEmailId),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@Fk_IndustryCategoryId",Fk_IndustryCategoryId),
                                      new SqlParameter("@CompanyName",CompanyName),
                                      new SqlParameter("@CompanyContactNo",CompanyContactNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@Designation",Designation),
                                      new SqlParameter("@WebSite",WebSite),
                                      new SqlParameter("@SkypeId",SkypeId),
                                      new SqlParameter("@FacebookId",FacebookId),
                                      new SqlParameter("@TwitterId",TwitterId),
                                      new SqlParameter("@LinkedInId",LinkedInId),
                                      new SqlParameter("@ApproximateEmployee",ApproximateEmployee),
                                      new SqlParameter("@ApproximateCompanyTurnOver",ApproximateCompanyTurnOver),
                                      new SqlParameter("@FirstInstructionDate",FirstInstructionDate),
                                      new SqlParameter("@Fk_ExpectedProductCategoryId",Fk_ExpectedProductCategoryId),
                                      new SqlParameter("@Fk_SourceId",Fk_SourceId),
                                      new SqlParameter("@Fk_ExecutiveId",Fk_ExecutiveId),
                                      new SqlParameter("@Fk_ModeInterActionId",Fk_ModeInterActionId),
                                      new SqlParameter("@FollowupDate",FollowupDate),
                                      new SqlParameter("@Description",Description),
                                      new SqlParameter("@AddedBy",AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("InsertProcpectLeads", para);
            return ds;
        }

        public DataSet ProspectList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_ProcpectId", Pk_ProcpectId),
                                      new SqlParameter("@ContactPerson", ContactPerson),
                                      new SqlParameter("@ContactNo", ContactNo),
                                      new SqlParameter("@CompanyName", CompanyName),
                                     new SqlParameter("@EmployeeId",EmployeeId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetProspectList", para);
            return ds;
        }

        public DataSet DeleteProspect()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_ProcpectId", Pk_ProcpectId), new SqlParameter("@DeletedBy", DeletedBy) };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProspect", para);
            return ds;
        }

        public DataSet UpdateProspect()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@Pk_ProcpectId",Pk_ProcpectId),
                                     new SqlParameter("@ContactPerson",ContactPerson),
                                      new SqlParameter("@ContactEmailId",ContactEmailId),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@Fk_IndustryCategoryId",Fk_IndustryCategoryId),
                                      new SqlParameter("@CompanyName",CompanyName),
                                      new SqlParameter("@CompanyContactNo",CompanyContactNo),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@Pincode",Pincode),
                                      new SqlParameter("@Designation",Designation),
                                      new SqlParameter("@WebSite",WebSite),
                                      new SqlParameter("@SkypeId",SkypeId),
                                      new SqlParameter("@FacebookId",FacebookId),
                                      new SqlParameter("@TwitterId",TwitterId),
                                      new SqlParameter("@LinkedInId",LinkedInId),
                                      new SqlParameter("@ApproximateEmployee",ApproximateEmployee),
                                      new SqlParameter("@ApproximateCompanyTurnOver",ApproximateCompanyTurnOver),
                                        new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProspect", para);
            return ds;
        }

     

       

      
    }
}