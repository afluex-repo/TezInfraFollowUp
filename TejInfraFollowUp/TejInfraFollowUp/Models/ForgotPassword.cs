using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace TejInfraFollowUp.Models
{
    public class ForgotPassword:Common
    {
        public string Pk_Id { get; set; }
        public string LoginId { get; set; }
        public string EmailId { get; set; }


        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }




        public DataSet PasswordForget()
        {
            SqlParameter[] para = {new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@EmailId",EmailId)};
            DataSet ds = DBHelper.ExecuteQuery("ForgotPassword", para);
            return ds;

        }


        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ChangePasswordNew", para);
            return ds;

        }


    }
}