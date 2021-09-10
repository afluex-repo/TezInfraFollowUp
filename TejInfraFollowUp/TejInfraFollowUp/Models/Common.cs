using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TejInfraFollowUp.Models
{
    public class Common
    {
        public string DeletedBy { get; set; }
        public string StateName { get; set; }
        public string VendorCode { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string AddedBy { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string UpdatedBy { get; set; }
        public DataSet BindVendor()
        {
            SqlParameter[] para ={new SqlParameter ("@VendorCode",VendorCode),
                                new SqlParameter("@Name",Name),
                                  new SqlParameter("@MobileNo",MobileNo)};
            DataSet ds = DBHelper.ExecuteQuery("GetVendorDetails", para);
            return ds;
        }

        public DataSet GetMediaVehicle()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetMediaVehicle");
            return ds;
        }

        public DataSet GetMediaType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetMediaType");
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para ={new SqlParameter ("@PinCode",Pincode),
                               };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }

        public string Result { get; set; }

        public static List<SelectListItem> BindDateFormat()
        {
            List<SelectListItem> ddldateformat = new List<SelectListItem>();
            ddldateformat.Add(new SelectListItem { Text = "Single", Value = "Single" });
            ddldateformat.Add(new SelectListItem { Text = "Double", Value = "Double" });
         
            return ddldateformat;
        }
        public static List<SelectListItem> BindPaymentTerms()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text="Please Select ",Value="0" });
            PaymentMode.Add(new SelectListItem { Text="Monthly",Value="1" });
            PaymentMode.Add(new SelectListItem { Text="Quarterly",Value="2" });
            PaymentMode.Add(new SelectListItem { Text="Yearly",Value="3" });
            PaymentMode.Add(new SelectListItem { Text="Immediate",Value="4" });
            return PaymentMode;
        }
        public static List<SelectListItem> BillingSnaps()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Please Select ", Value = "0" });
            PaymentMode.Add(new SelectListItem { Text="Start Date-End Date",Value="1" });
            PaymentMode.Add(new SelectListItem { Text="Start Date-Mid Date-End Date",Value="2" });
            return PaymentMode;
        }
        public static List<SelectListItem> POReceived()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Please Select ", Value = "0" });
            PaymentMode.Add(new SelectListItem { Text="Yes",Value="1" });
            PaymentMode.Add(new SelectListItem { Text="Awaited",Value="2" });
            PaymentMode.Add(new SelectListItem { Text = "Billing on Mail Confirmation", Value = "3" });
            return PaymentMode;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }
    }
}