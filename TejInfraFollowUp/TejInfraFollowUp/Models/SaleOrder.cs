using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TejInfraFollowUp.Models
{
    public class SaleOrder:Common
    {
        #region Properties
        public string PK_SalesOrderID { get; set; }
        public string SaleOrderDetailsID { get; set; }
        public string SaleOrderNoEncrypt { get; set; }
        public string SaleOrderIDEncrypt { get; set; }
        public string SalesOrderNo { get; set; }
        public string SalesOrderNoID { get; set; }
        public string CustomerID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceDate { get; set; }
        public string CampaignID { get; set; }
        public string CampaignNumber { get; set; }
        public string POReceived { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string TotalAmount { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string TDS { get; set; }
        public string Discount { get; set; }
        public string FinalAmount { get; set; }
        public string PONumber { get; set; }
        public string POImagePath { get; set; }
        public string PaymentTermsID { get; set; }
        public string SalesPersonID { get; set; }
        public string OperationExecutiveID { get; set; }
        public string BillingSnapsID { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        
        public string Side { get; set; }
        public string Unit { get; set; }
        public string Area { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string Description { get; set; }
        //public string SaleOrderNo { get; set; }
        public string OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerInfo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerMobile { get; set; }
        public string PK_SalesOrderNoID { get; set; }
        public string CustomerAddress { get; set; }
        public string ServiceType { get; set; }
        public string isEditable { get; set; }
        public DataTable dtSaleOrderDetails { get; set; }
        public List<SaleOrder> lstsaleorder { get; set; }
        public List<SaleOrder> lstSites { get; set; }
        public List<SaleOrder> lstSalesPerson { get; set; }
        public List<SaleOrder> lstOperationExecutive { get; set; }
        #endregion

        public DataSet GetAllVendors()
        {
            SqlParameter[] para ={ new SqlParameter ("@VendorCode",VendorCode),
                                      new SqlParameter("@Name",VendorName ),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetVendorDetails", para);
            return ds;
        }

        public DataSet SalesOrderNoList()
        {
            SqlParameter[] para ={ new SqlParameter ("@SaleOrderNo",SalesOrderNo) };
            DataSet ds = DBHelper.ExecuteQuery("GetSaleOrderNoList", para);
            return ds;
        }

        public DataSet GenerateSalesOrderNo()
        {
            SqlParameter[] para ={ new SqlParameter ("@AddedBy",AddedBy) };
            DataSet ds = DBHelper.ExecuteQuery("GenerateSalesOrderNo", para);
            return ds;
        }

        public DataSet GetSiteList()
        {
            SqlParameter[] para ={new SqlParameter ("@SiteName",SiteName),
                                   new SqlParameter("@SiteID",SiteID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAllSiteList", para);
            return ds;
        }

        public DataSet GetSalesPersonList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetSalesPersonList");
            return ds;
        }

        public DataSet GetOperationExecutiveList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetOperationExecutiveList");
            return ds;
        }

        public DataSet GetServiceList()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_ServiceId", ServiceID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAllServices", para);
            return ds;
        }

        public DataSet SaleOrderInsert()
        {
            SqlParameter[] para = { new SqlParameter("@SalesOrderDetails", dtSaleOrderDetails),
                                  new SqlParameter("@SalesOrderNoID", SalesOrderNoID),
                                  new SqlParameter("@OrderDate", OrderDate),
                                  new SqlParameter("@CustomerCode", CustomerCode),
                                  new SqlParameter("@CampaignID", CampaignID),
                                  new SqlParameter("@POReceived", POReceived),
                                  new SqlParameter("@PONumber", PONumber),
                                  new SqlParameter("@POImagePath", POImagePath),
                                  new SqlParameter("@PaymentTermsID", PaymentTermsID),
                                  new SqlParameter("@SalesPersonID", SalesPersonID),
                                  new SqlParameter("@OperationExecutiveID", OperationExecutiveID),
                                  new SqlParameter("@BillingSnapsID", BillingSnapsID),
                                  new SqlParameter("@AddedBy", AddedBy) 
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SalesOrderInsert", para);
            return ds;
        }

        public DataSet SaleOrderUpdate()
        {
            SqlParameter[] para = { new SqlParameter("@SalesOrderDetails", dtSaleOrderDetails),
                                  new SqlParameter("@PK_SalesOrderID", PK_SalesOrderID),
                                  new SqlParameter("@SalesOrderNoID", SalesOrderNoID),
                                  new SqlParameter("@OrderDate", OrderDate),
                                  new SqlParameter("@CustomerCode", CustomerCode),
                                  new SqlParameter("@CampaignID", CampaignID),
                                  new SqlParameter("@POReceived", POReceived),
                                  new SqlParameter("@PONumber", PONumber),
                                  new SqlParameter("@POImagePath", POImagePath),
                                  new SqlParameter("@PaymentTermsID", PaymentTermsID),
                                  new SqlParameter("@SalesPersonID", SalesPersonID),
                                  new SqlParameter("@OperationExecutiveID", OperationExecutiveID),
                                  new SqlParameter("@BillingSnapsID", BillingSnapsID),
                                  new SqlParameter("@AddedBy", AddedBy) 
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSaleOrder", para);
            return ds;
        }

        public DataSet GetSaleOrderDetails()
        {
            SqlParameter[] para = { new SqlParameter("@SalesOrderNo", SalesOrderNo),
                                  new SqlParameter("@CustomerID", CustomerID) };
            DataSet ds = DBHelper.ExecuteQuery("SalesOrderList", para);
            return ds;
        }

        public DataSet DeleteSaleOrderLine()
        {
            SqlParameter[] para = { new SqlParameter("@PK_SaleOrderDetailsID", SaleOrderDetailsID),
                                  new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = DBHelper.ExecuteQuery("DeleteSaleOrderLine", para);
            return ds;
        }

    }
}