using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class EmployeeRegistration:Common
    {
        public string Pk_Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string UserImage { get; set; }
        public string Pk_UserTypeID { get; set; }
        public string Fk_UserTypeId { get; set; }
        public string Loginid { get; set; }
        public string Fk_EmployeeId { get; set; }
        public string CreatedBy { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string file { get; set; }
        public string PK_EmployeeDocumentId { get; set; }
        public string Profile { get; set; }
        public List<EmployeeRegistration> lstemployee { get; set; }
        public string postedFile { get; set; }
        public List<EmployeeRegistration> lstDocumentAndDate { get; set; }
        public HttpPostedFileBase profilePic { get; set; }
   
        public DataSet BindUserType()
        {
            
            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeName");
            return ds;
        }

        public DataSet SaveEmployeeRegistration()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@UserImage",UserImage),
                                      new SqlParameter("@CreatedBy",CreatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }

        public DataSet GetEmployeeList()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_Id", Pk_Id) };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeRegistration", para);
            return ds;
        } 

        public DataSet DeleteEmployee()
        {
            SqlParameter[] para = { new SqlParameter("@Pk_Id",Pk_Id),new SqlParameter("@DeletedBy",DeletedBy)};
            DataSet ds = DBHelper.ExecuteQuery("DeleteEmployeeRegistration", para);
            return ds;

        }

        public DataSet UpdateEmployeeRegistration()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Pk_Id",Pk_Id),
                                      new SqlParameter("@Fk_UserTypeId",Fk_UserTypeId),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo),
                                      new SqlParameter("@EmailId",EmailId),
                                      new SqlParameter("@Address",Address),
                                      new SqlParameter("@UserImage",UserImage),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateEmployeeRegistration", para);
            return ds;
        }

        public DataSet FilterEmployee()
        {
            SqlParameter[] para = {
               
                                      new SqlParameter("@Fk_UserTypeId",UserName),
                                      new SqlParameter("@Name",Name),
                                      new SqlParameter("@ContactNo",ContactNo)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeRegistration",para);
            return ds;
        }



        public DataSet SaveEmployeeDocuments()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UpLoadDocument",file),
                                      new SqlParameter("@UpLoadDocumentDate",Date),
                                        new SqlParameter("@FK_EmployeeId",Pk_Id),
                                                 new SqlParameter("@AddedBy",AddedBy)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("CreateEmployeeDocument", para);
            return ds;
        }


        public DataSet DocumentAndDateDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Loginid",Loginid),
                                      new SqlParameter("@Fk_EmployeeId",Fk_EmployeeId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DocumentAndDateDetails",para);
            return ds;
        }


    }
}