using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TejInfraFollowUp.Models
{
    public class SendEmail:Common
    {
        public string Pk_Id { get; set; }
        public string Message { get; set; }
        public List<SendEmail> lstEmail { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }

        public DataSet GetEmployeeList()
        {
            SqlParameter[] para = { };
            DataSet ds = DBHelper.ExecuteQuery("GetEmployeeRegistration", para);
            return ds;
        }

     
    }
}