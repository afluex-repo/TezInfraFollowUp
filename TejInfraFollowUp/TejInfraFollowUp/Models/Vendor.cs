using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class Vendor:Common
    {
        public string VendorName { get; set; }
        public string NatureOfBusiness { get; set; }
        public string ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string GSTNO { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string PANNO { get; set; }
        public string ConcernPerson { get; set; }
        public string ConcerPersonEmail { get; set; }
        public string ConcerPersonContact { get; set; }
        public string ServiceTypeNameDisplay { get; set; }
        public string VendorId { get; set; }

        public List<Vendor> lstVendor { get; set; }

        public DataSet GetAllVendors()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@VendorCode",VendorCode),
                                      new SqlParameter("@Name",VendorName ),
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetVendorDetails", para);
            return ds;
        }

        public DataSet VendorRegistration()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@Name",VendorName),
                                      new SqlParameter("@Contact",MobileNo ),
                                      new SqlParameter("@Email",Email ),
                                      new SqlParameter("@Address",Address ),
                                      new SqlParameter("@AddedBy",AddedBy ),
                                      new SqlParameter("@PinCode",Pincode ),
                                      new SqlParameter("@State",StateName ),
                                      new SqlParameter("@City",City ),
                                      new SqlParameter("@BankName",BankName ),
                                      new SqlParameter("@AccountNumber",AccountNo ),
                                      new SqlParameter("@IFSCCode",IFSCCode ),
                                      new SqlParameter("@ConcernPersonName",ConcernPerson ),
                                      new SqlParameter("@ConcernPersonContact",ConcerPersonContact ),
                                      new SqlParameter("@ConcernPersonEmail",ConcerPersonEmail ),
                                      new SqlParameter("@GSTNo",GSTNO ),
                                      new SqlParameter("@PanNo",PANNO ),
                                      new SqlParameter("@NatureOfBusiness",NatureOfBusiness ),
                                      new SqlParameter("@ServiceTypeID", ServiceTypeID),
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("VendorRegistration", para);
            return ds;
        }

        public DataSet UpdateVendor()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@Name",VendorName),
                                      new SqlParameter("@Contact",MobileNo ),
                                       new SqlParameter("@Email",Email ),
                                        new SqlParameter("@Address",Address ),
                                         new SqlParameter("@UpdatedBy",UpdatedBy ),
                                          new SqlParameter("@PinCode",Pincode ),
                                           new SqlParameter("@State",StateName ),
                                            new SqlParameter("@City",City ),
                                           new SqlParameter("@BankName",BankName ),
                                           new SqlParameter("@AccountNumber",AccountNo ),
                                           new SqlParameter("@IFSCCode",IFSCCode ),
                                           new SqlParameter("@ConcernPersonName",ConcernPerson ),
                                           new SqlParameter("@ConcernPersonContact",ConcerPersonContact ),
                                           new SqlParameter("@ConcernPersonEmail",ConcerPersonEmail ),
                                           new SqlParameter("@GSTNo",GSTNO ),
                                           new SqlParameter("@PanNo",PANNO ),
                                            new SqlParameter("@VendorCode",VendorCode ),
                                            new SqlParameter("@NatureOfBusiness",NatureOfBusiness ),
                                          
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("UpdateVendorRegistration", para);
            return ds;
        }

        public DataSet DeleteVendor()
        {
            SqlParameter[] para ={    
                                      new SqlParameter ("@VendorCode",VendorCode),
                                      new SqlParameter("@Deletedby",DeletedBy ),
                                     
                                 };
            DataSet ds = DBHelper.ExecuteQuery("DeleteVendor", para);
            return ds;
        }
    }
}