using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace TejInfraFollowUp.Models
{
    public class PO : Common
    {
        #region Properties
        public string EncryptKey { get; set; }
        public string PONumberID { get; set; }
        public string PONumber { get; set; }
        public string PODate { get; set; }
        public string PaymentTerms { get; set; }
        public string SaleOrderNumber { get; set; }
        public string SaleOrderNumberID { get; set; }
        public string CampaignNoID { get; set; }
        public string CampaignNumber { get; set; }
        public string OperationExecutiveID { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceImage { get; set; }
        public string SiteName { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Area { get; set; }
        public string MediaTypeName { get; set; }
        public string MediaVehicleName { get; set; }

        public List<PO> lstPO { get; set; }
        #endregion

        public DataSet GetOperationExecutiveList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetOperationExecutiveList");
            return ds;
        }

        public DataSet InsertPurchaseOrder()
        {
            SqlParameter[] para = { new SqlParameter("@FK_SaleOrderNoID", SaleOrderNumberID),
                                  new SqlParameter("@PurchaseOrderNo", PONumber), 
                                  new SqlParameter("@PurchaseOrderDate", PODate),
                                  new SqlParameter("@PaymentTermsID", PaymentTerms),
                                  new SqlParameter("@InvoiceNo", InvoiceNo),
                                  new SqlParameter("@InvoiceImage", InvoiceImage),
                                  new SqlParameter("@AddedBy", AddedBy) };

            DataSet ds = DBHelper.ExecuteQuery("PurchaseOrderInsert", para);
            return ds;
        }

        public DataSet POList()
        {
            SqlParameter[] para = { new SqlParameter("@PONumber", PONumber) };
            DataSet ds = DBHelper.ExecuteQuery("POList", para);
            return ds;
        }

    }
}