using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TejInfraFollowUp.Models;
using TejInfraFollowUp.Filter;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace TejInfraFollowUp.Models
{
    public class Admin : Common
    {
        public string SenderEmailDisplay { get; set; }
        [AllowHtml]
        public string EmailBodyHTML { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string EncryptKey { get; set; }
        public string TemplateSubject { get; set; }
        public string TempalteBody { get; set; }
        public List<Admin> lstTemplates { get; set; }
        public List<Admin> lstVendor { get; set; }
        public List<Admin> lstSaleOrders { get; set; }
        public string SelectedFilePath { get; set; }
        public string VendorName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string NatureOfBusiness { get; set; }
        public string ConcernPerson { get; set; }
        public string ConcerPersonContact { get; set; }
        public string Address { get; set; }
        public string VendorId { get; set; }
        public string Subject { get; set; }
        public string SaleOrderNo { get; set; }
        public string SaleOrderNoID { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public string OrderDate { get; set; }
        public string CssClass { get; set; }
        public string Body { get; set; }
        public string PK_TemplateID { get; set; }
        public string PK_EmailID { get; set; }
        
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        

        public DataSet GetDashboardDetails()
        {
            DataSet ds = DBHelper.ExecuteQuery("AdminDashboard");
            return ds;
        }

        public DataSet GetEmailData()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetEmail");
            return ds;
        }

        public DataSet GetSenderEmail()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetSenderEmails");
            return ds;
        }

        #region SaveEmails

        public DataSet SaveEmails()
        {
            SqlParameter[] para ={   new SqlParameter ("@Name", Name),
                                     new SqlParameter ("@Email", Email),
                                     new SqlParameter ("@Description", Description),
                                     new SqlParameter("@AddedBy", AddedBy), };

            DataSet ds = DBHelper.ExecuteQuery("SaveEmailData", para);
            return ds;
        }

        public DataSet DeletEmail()
        {
            SqlParameter[] para ={   new SqlParameter ("@PK_EmailID", PK_EmailID),
                                     new SqlParameter ("@DeletedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("DeleteEmail", para);
            return ds;
        }

        #endregion

        #region EmailTemplate

        public DataSet GetAllTemplates()
        {
            SqlParameter[] para = { new SqlParameter("@PK_TemplateID", PK_TemplateID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAllTemplates", para);
            return ds;
        }

        public DataSet SaveEmailTemplate()
        {
            SqlParameter[] para ={   new SqlParameter ("@TemplateSubject", Subject),
                                     new SqlParameter ("@TemplateBody", EmailBodyHTML),
                                     new SqlParameter ("@FilePath", SelectedFilePath),
                                     new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = DBHelper.ExecuteQuery("SaveEmailTemplate", para);
            return ds;
        }

        #endregion


        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AdminChangePassword", para);
            return ds;

        }


    }
}