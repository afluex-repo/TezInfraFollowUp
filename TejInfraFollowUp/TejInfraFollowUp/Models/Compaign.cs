using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class Compaign:Common
    {
        public string CustomerId { get; set; }
        public string CreativeName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Decription { get; set; }
        public string CompaignId { get; set; }
        public string CampaignNo { get; set; }

        public DataSet GetCampaigns()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@CampaignNo",CampaignNo),
                                   new SqlParameter ("@CustomerCode",CustomerId),
                                  
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetCampaigns", para);
            return ds;
        }

        public DataSet CampaignEntry()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@CustomerCode",CustomerId),
                                       new SqlParameter ("@CreativeName",CreativeName),
                                        new SqlParameter ("@StartDate",StartDate),
                                         new SqlParameter ("@EndDate",EndDate),
                                          new SqlParameter ("@Description",Decription),
                                           new SqlParameter ("@AddedBy",AddedBy),
                                   
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("CampaignEntry", para);
            return ds;
        }

        public List<Compaign> lstCompaign { get; set; }



        public string CustomerName { get; set; }

        public string CompaignCode { get; set; }

        public DataSet UpdateCompaign()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@CustomerCode",CustomerId),
                                       new SqlParameter ("@CreativeName",CreativeName),
                                        new SqlParameter ("@StartDate",StartDate),
                                         new SqlParameter ("@EndDate",EndDate),
                                          new SqlParameter ("@Description",Decription),
                                           new SqlParameter ("@UpdatedBy",UpdatedBy),
                                   new SqlParameter ("@CompaignId",CompaignId),
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCompaign", para);
            return ds;
        }
    }
}