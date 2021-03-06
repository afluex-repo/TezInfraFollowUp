using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TejInfraFollowUp.Models
{
    public class Master : Common
    {
        public string Side { get; set; }
        public string SiteImage { get; set; }
        public string SiteOwner { get; set; }
        public string Comments { get; set; }
        public string CartRate { get; set; }
        public string Quantity { get; set; }
        public string Facing { get; set; }
        public string SiteID { get; set; }
        public string Hidden { get; set; }
        public string VendorID { get; set; }
        public string Rational { get; set; }
        public string Location { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string ActivityName { get; set; }
        public string Pk_ActivityId { get; set; }
        public string ProductCategoryName { get; set; }
        public string Pk_ProductCategoryId { get; set; }
        public string Area { get; set; }
        public string SiteName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }

        public string Pk_CategoryId { get; set; }
        public string ChanceName { get; set; }
        public string Pk_BusinessChanceId { get; set; }
        public string SourceName { get; set; }
        public string Pk_SourceId { get; set; }
        public string PK_InterActionId { get; set; }
        public List<Master> lstChance { get; set; }
        public List<Master> lstProductCategory { get; set; }
        public List<Master> lstActivity { get; set; }
        public List<Master> lstSource { get; set; }
        public List<Master> lstCategory { get; set; }
        public List<Master> lstSites { get; set; }
        public List<Master> lstInterAction { get; set; }
        public DataTable dtSiteMaster { get; set; }
        public string MediaTypeID { get; set; }
        public string MediaTypeName { get; set; }
        public string MediaVehicleName { get; set; }
        public string InterActionName { get; set; }
        public string MediaVehicleID { get; set; }
        public string ServiceId { get; set; }
        public string HSNCode { get; set; }
        public string ServiceName { get; set; }
        public string CGST { get; set; }
        public string CategoryName { get; set; }
        public string IGST { get; set; }
        public string SGST { get; set; }
        public string DateFormat { get; set; }

        public string VisitorId { get; set; }
        public string VisitorImage { get; set; }
        public string AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string Amount { get; set; }
        public string VisitDate { get; set; }
        public DataTable dtVisitorDetails { get; set; }
        public string Address { get; set; }
        public string UserID { get; set; }
        public string LoginId { get; set; }
        public HttpPostedFileBase files { get; set; }
        public string Image { get; set; }
        public List<SelectListItem> ddlsite { get; set; }
        public List<Master> VisitorList { get; set; }
        public string postedFile1 { get; set; }
        public string EncryptKey { get; set; }
        public string EncryptedId { get; set; }
        public string VisitorDetailId { get; set; }
        public string VehicleNumber { get; set; }

        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string VehicleDetails { get; set; }
        public string PickUpLocation { get; set; }
        public string DropLocation { get; set; }


        public string PickUpTime { get; set; }
        public string DropTime { get; set; }
        

        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> ddlCategoryName { get; set; }

        public List<Master> lstUserType { get; set; }
        public string CreatedBy { get; set; }

        public DataSet VisitorListById()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_VisitorId",VisitorId),
                new SqlParameter("@PK_VisitorDetailId",VisitorDetailId),
            };
            DataSet ds = DBHelper.ExecuteQuery("VisitorListById", para);
            return ds;
        }
        public DataSet SaveSite()
        {
            SqlParameter[] para ={new SqlParameter ("@SiteMaster",dtSiteMaster),
                                new SqlParameter("@AddedBy",AddedBy),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("SiteMaster", para);
            return ds;
        }

        public DataSet UpdateSite()
        {
            SqlParameter[] para ={
                                       new SqlParameter ("@SiteID",SiteID),
                                     new SqlParameter ("@VendorID",VendorID),
                                     new SqlParameter ("@SiteName",SiteName),
                                     new SqlParameter ("@Pincode",Pincode),
                                     new SqlParameter ("@StateName",StateName),
                                     new SqlParameter ("@City",City),
                                     new SqlParameter ("@Facing",Facing),
                                     new SqlParameter ("@Rational",Rational),
                                     new SqlParameter ("@Side",Side),
                                     new SqlParameter ("@Quantity",Quantity),
                                     new SqlParameter ("@Width",Width),
                                     new SqlParameter ("@Height",Height),
                                     new SqlParameter("@Area",Area),
                                      new SqlParameter("@MediaVehicleID",MediaVehicleID),
                                       new SqlParameter("@MediaTypeID",MediaTypeID),
                                        new SqlParameter("@CartRate",CartRate),
                                         new SqlParameter("@Comments",Comments),
                                           new SqlParameter("@SiteOwner",SiteOwner),
                                             new SqlParameter("@SiteImage",SiteImage),
                                             new SqlParameter("@UpdatedBy",UpdatedBy),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSite", para);
            return ds;
        }

        public DataSet GetSiteList()
        {
            SqlParameter[] para ={new SqlParameter ("@SiteName",SiteName),
                                new SqlParameter("@MediaTypeID",MediaTypeID),
                                 new SqlParameter("@MediaVehicleId",MediaVehicleID),
                                  new SqlParameter("@VendorID",VendorID),
                                   new SqlParameter("@SiteID",SiteID),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetAllSiteList", para);
            return ds;
        }

        public DataSet DeleteSite()
        {
            SqlParameter[] para ={new SqlParameter ("@Fk_SiteId",SiteID),
                                new SqlParameter("@DeletedBy",DeletedBy),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("DeleteSite", para);
            return ds;
        }

        public DataSet GetServiceList()
        {
            SqlParameter[] para ={new SqlParameter ("@Fk_ServiceId",ServiceId),
                                new SqlParameter("@HSNCode",HSNCode),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetAllServices", para);
            return ds;
        }

        public DataSet SaveService()
        {
            SqlParameter[] para ={new SqlParameter ("@ServiceName",ServiceName),
                                    new SqlParameter("@HSNCode",HSNCode),
                                     new SqlParameter("@GST",CGST),
                                      new SqlParameter("@IGST",IGST),
                                       new SqlParameter("@SGST",SGST),
                                       new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@DateFormat",DateFormat),

                                 };
            DataSet ds = DBHelper.ExecuteQuery("InsertService", para);
            return ds;
        }

        public DataSet UpdateService()
        {
            SqlParameter[] para ={new SqlParameter ("@ServiceName",ServiceName),
                                    new SqlParameter("@HSNCode",HSNCode),
                                     new SqlParameter("@GST",CGST),
                                      new SqlParameter("@IGST",IGST),
                                       new SqlParameter("@SGST",SGST),
                                       new SqlParameter("@UpdatedBy",UpdatedBy),
                                        new SqlParameter("@DateFormat",DateFormat),
                                         new SqlParameter("@ServiceID",ServiceId),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("UpdateService", para);
            return ds;
        }

        public DataSet DeleteService()
        {
            SqlParameter[] para ={new SqlParameter ("@ServiceID",ServiceId),
                                     new SqlParameter("@DeletedBy",DeletedBy),

                                 };
            DataSet ds = DBHelper.ExecuteQuery("DeleteService", para);
            return ds;
        }

        public DataSet InsertInterAction()
        {
            SqlParameter[] param = {new SqlParameter("@InterActionName",InterActionName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertInterAction", param);
            return ds;
        }

        public DataSet ListInterAction()
        {
            SqlParameter[] param = { new SqlParameter("@PK_InterActionId", PK_InterActionId) };
            DataSet ds = DBHelper.ExecuteQuery("ListInterAction", param);
            return ds;
        }

        public DataSet UpdateInterAction()
        {
            SqlParameter[] param = { new SqlParameter("@PK_InterActionId", PK_InterActionId),
                                   new SqlParameter("@InterActionName",InterActionName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateInterAction", param);
            return ds;
        }


        public DataSet InsertCategory()
        {
            SqlParameter[] param = {new SqlParameter("@CategoryName",CategoryName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertCategory", param);
            return ds;
        }

        public DataSet ListCategory()
        {
            SqlParameter[] param = { new SqlParameter("@PK_CategoryId", Pk_CategoryId) };
            DataSet ds = DBHelper.ExecuteQuery("ListCategory", param);
            return ds;
        }

        public DataSet UpdateCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_CategoryId", Pk_CategoryId),
                                   new SqlParameter("@CategoryName",CategoryName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateCategory", param);
            return ds;
        }

        public DataSet InsertDataSource()
        {
            SqlParameter[] param = {new SqlParameter("@SourceName",SourceName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertDataSource", param);
            return ds;
        }

        public DataSet ListDataSource()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_SourceId", Pk_SourceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListDataSource", param);
            return ds;
        }

        public DataSet UpdateDataSource()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_SourceId", Pk_SourceId),
                                   new SqlParameter("@SourceName",SourceName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateDataSource", param);
            return ds;
        }

        public DataSet InsertProspectActivity()
        {
            SqlParameter[] param = {new SqlParameter("@ActivityName",ActivityName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertProspectActivity", param);
            return ds;
        }
        public DataSet ListActivity()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ActivityId", Pk_ActivityId) };
            DataSet ds = DBHelper.ExecuteQuery("ListActivity", param);
            return ds;
        }

        public DataSet UpdateActivity()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ActivityId", Pk_ActivityId),
                                   new SqlParameter("@ActivityName",ActivityName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateProspectActivity", param);
            return ds;
        }

        public DataSet InsertBusinessChance()
        {
            SqlParameter[] param = {new SqlParameter("@ChanceName",ChanceName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("InsertBusinessChance", param);
            return ds;
        }
        public DataSet ListBusinessChance()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId) };
            DataSet ds = DBHelper.ExecuteQuery("ListBusinessChance", param);
            return ds;
        }

        public DataSet UpdateBusinessChance()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_BusinessChanceId", Pk_BusinessChanceId),
                                   new SqlParameter("@ChanceName",ChanceName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusiness", param);
            return ds;
        }

        public DataSet ExpectedProductCategoryInsert()
        {
            SqlParameter[] param = {new SqlParameter("@ProductCategoryName",ProductCategoryName),
                                   new SqlParameter("@AddedBy",AddedBy)};
            DataSet ds = DBHelper.ExecuteQuery("ExpectedProductCategoryInsert", param);
            return ds;
        }

        public DataSet ListProductCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", param);
            return ds;
        }

        public DataSet UpdateProductCategory()
        {
            SqlParameter[] param = { new SqlParameter("@Pk_ProductCategoryId", Pk_ProductCategoryId),
                                   new SqlParameter("@ProductCategoryName",ProductCategoryName),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateExpectedProductCategory", param);
            return ds;
        }


        public DataSet GetSiteName()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetSiteName");
            return ds;
        }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }

        public DataSet SaveVisitorDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SiteName",SiteID),
                                       new SqlParameter("@FK_AssociateId", AssociateID),
                                       new SqlParameter("@Fk_CategoryId", Pk_CategoryId),
                                       new SqlParameter("@VisiteDate", VisitDate),
                                       new SqlParameter("@VisitorImage", Image),
                                       new SqlParameter("@AddedBy", CreatedBy),
                                       new SqlParameter("@AssociateName",AssociateName),
                                       new SqlParameter("@VehicleDetails",VehicleNumber),
                                       new SqlParameter("@PickUpLocation",PickUpLocation),
                                       new SqlParameter("@DropLocation",DropLocation),
                                        new SqlParameter("@PickUpTime",PickUpTime),
                                         new SqlParameter("@DropTime",DropTime),
                                      new SqlParameter("@DtVisitorDetail",dtVisitorDetails)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveVisitor", para);
            return ds;
        }

        public DataSet GetVisitorDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_VisitorId",VisitorId),
                                      new SqlParameter("@PK_VisitorDetailId",VisitorDetailId),
                                       new SqlParameter("@Associateid",AssociateID),
                                       new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetVisitorDetails", para);
            return ds;
        }

        public DataSet GetVisitorDetailsforEmployee()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_VisitorId",VisitorId),
                                       new SqlParameter("@Associateid",AssociateID),
                                       new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@AddedBy",CreatedBy)


                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetVisitorDetailsforEmployee", para);
            return ds;
        }




        public DataSet GetCategoryName()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetCategoryName");
            return ds;
        }


        public DataSet SaveUserType()
        {
            SqlParameter[] param = {new SqlParameter("@UserType",UserType),
                                   //new SqlParameter("@Description",Description),
                                   new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("CreateUserType", param);
            return ds;
        }

        public DataSet UpdateUserType()
        {
            SqlParameter[] param = {
                                    new SqlParameter("@UserTypeId",UserTypeId),
                                    new SqlParameter("@UserType",UserType),
                                   //new SqlParameter("@Description",Description),
                                   new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateUserType", param);
            return ds;
        }



        public DataSet GetUserTypeDeatils()
        {
            SqlParameter[] param = {new SqlParameter("@UserTypeId",UserTypeId),
                                   new SqlParameter("@UserType",UserType),
                                   new SqlParameter("@Description",Description)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeDetails", param);
            return ds;
        }



        public DataSet DeleteUserType()
        {
            SqlParameter[] param = {new SqlParameter("@UserTypeId",UserTypeId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteUserType", param);
            return ds;
        }

        public DataSet DeleteCategory()
        {
            SqlParameter[] param = {new SqlParameter("@CategoryId",Pk_CategoryId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteCategory", param);
            return ds;
        }

        public DataSet DeleteInterAction()
        {
            SqlParameter[] param = {new SqlParameter("@InsertActionId",PK_InterActionId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteInsertAction", param);
            return ds;
        }

        public DataSet DeleteDataSource()
        {
            SqlParameter[] param = {new SqlParameter("@Pk_SourceId",Pk_SourceId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteDataSource", param);
            return ds;
        }

        public DataSet DeleteProspectActivity()
        {
            SqlParameter[] param = {new SqlParameter("@Pk_ActivityId",Pk_ActivityId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProspectActivity", param);
            return ds;
        }


        public DataSet DeleteBusinessChance()
        {
            SqlParameter[] param = {new SqlParameter("@Pk_BusinessChanceId",Pk_BusinessChanceId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteBusinessChance", param);
            return ds;
        }

        public DataSet DeleteProductCategory()
        {
            SqlParameter[] param = {new SqlParameter("@Pk_ProductCategoryId",Pk_ProductCategoryId),
                                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteProductCategory", param);
            return ds;
        }

        public DataSet DeleteVisitor()
        {
            SqlParameter[] param = {new SqlParameter("@VisitorId",VisitorId),
                new SqlParameter("@VisitorDetailId",VisitorDetailId),
                                 new SqlParameter("@AddedBy",AddedBy),

            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteVisitor", param);
            return ds;
        }


        public DataSet UpdateVisitorDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@VisitorID",VisitorId),
                                       new SqlParameter("@VisitorDetailId",VisitorDetailId),
                                      new SqlParameter("@SiteName",SiteID),
                                       new SqlParameter("@FK_AssociateId", AssociateID),
                                       new SqlParameter("@Fk_CategoryId", Pk_CategoryId),
                                       new SqlParameter("@VisiteDate", VisitDate),
                                       new SqlParameter("@VisitorImage", Image),
                                       new SqlParameter("@AddedBy", CreatedBy),
                                       new SqlParameter("@AssociateName",AssociateName),
                                       new SqlParameter("@VehicleDetails",VehicleNumber),
                                       new SqlParameter("@PickUpLocation",PickUpLocation),
                                       new SqlParameter("@DropLocation",DropLocation),
                                        new SqlParameter("@PickUpTime",PickUpTime),
                                         new SqlParameter("@DropTime",DropTime),
                                      new SqlParameter("@DtVisitorDetail",dtVisitorDetails)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateVisitor", para);
            return ds;
        }

    }
}
