using TejInfraFollowUp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TejInfraFollowUp.Filter;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TejInfraFollowUp.Controllers
{
    public class MasterController : AdminBaseController
    {
        #region SiteMaster Start
        public ActionResult SiteMaster(string SiteId)
        {
            Master obj = new Master();
            if (TempData["SiteError"] == null)
            {
                ViewBag.errormsg = "none";
                ViewBag.saverrormsg = "none";
            }
            Session["dt"] = null;
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType
            if ((SiteId) != "" && SiteId != null)
            {
                ViewBag.Isvisible = "none";
                Master objmaster = new Master();
                objmaster.SiteID = Crypto.Decrypt(SiteId);
                DataSet ds = objmaster.GetSiteList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        obj.SiteID = (dr["PK_SiteID"].ToString());
                        obj.VendorID = dr["FK_VendorID"].ToString();
                        obj.SiteName = dr["SiteName"].ToString();
                        obj.Pincode = dr["Pincode"].ToString();
                        obj.StateName = dr["State"].ToString();
                        obj.City = dr["City"].ToString();
                        obj.Location = dr["Location"].ToString();
                        obj.Facing = dr["Facing"].ToString();
                        obj.Rational = dr["Rational"].ToString();
                        obj.Height = dr["Height"].ToString();
                        obj.Width = dr["Width"].ToString();
                        obj.Quantity = dr["Quantity"].ToString();
                        obj.Side = dr["Side"].ToString();
                        obj.Area = dr["Area"].ToString();
                        obj.MediaVehicleID = dr["PK_MediaVehicleID"].ToString();
                        obj.MediaTypeID = dr["PK_MediaTypeID"].ToString();
                        obj.CartRate = dr["CartRate"].ToString();
                        obj.SiteOwner = dr["ActualSiteOwner"].ToString();
                        obj.SiteImage = dr["SiteImage"].ToString();
                    }
                }
            }
            else
            {
                ViewBag.Isvisible = "";
            }


            return View(obj);
        }

        [HttpPost]
        [ActionName("SiteMaster")]
        [OnAction(ButtonName = "addSite")]
        //string VendorID, string SiteName, string Pincode, string StateName, string City, string Facing, string Rational, string Height, string Width, string Quantity, string Side, string Area, string MediaVehicleID, string MediaTypeID, string CartRate, string SiteOwner,
        public ActionResult SaveSiteTemp(Master model, HttpPostedFileBase postedFile)
        {
            if (TempData["SiteError"] == null)
            {
                ViewBag.errormsg = "none";
                ViewBag.saverrormsg = "none";
            }
            if (postedFile != null)
            {
                model.SiteImage = "../SoftwareImages/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Path.Combine(Server.MapPath(model.SiteImage)));
            }
            DataTable dt = new DataTable();

            if (Session["dt"] != null)
            {
                dt = (DataTable)Session["dt"];

                DataRow dr = null;
                dr = dt.NewRow();
                dr["FK_VendorID"] = model.VendorID;
                dr["SiteName"] = model.SiteName;
                dr["Pincode"] = model.Pincode;
                dr["State"] = model.StateName;
                dr["City"] = model.City;
                dr["Location"] = model.Location;
                dr["Facing"] = model.Facing;
                dr["Rational"] = model.Rational;
                dr["Height"] = model.Height;
                dr["Width"] = model.Width;
                dr["Quantity"] = model.Quantity;
                dr["Side"] = model.Side;
                dr["Area"] = model.Area;
                dr["MediaVehicle"] = model.MediaVehicleID;
                dr["MediaType"] = model.MediaTypeID;
                dr["CartRate"] = model.CartRate;
                dr["ActualSiteOwner"] = model.SiteOwner;
                dr["SiteImage"] = model.SiteImage == null ? null : model.SiteImage;
                dr["Comments"] = model.Comments;
                dt.Rows.Add(dr);

                Session["dt"] = dt;
            }
            else
            {
                dt.Columns.Add("FK_VendorID", typeof(string));
                dt.Columns.Add("SiteName", typeof(string));
                dt.Columns.Add("Pincode", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Facing", typeof(string));
                dt.Columns.Add("Rational", typeof(string));
                dt.Columns.Add("Height", typeof(string));
                dt.Columns.Add("Width", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
                dt.Columns.Add("Side", typeof(string));
                dt.Columns.Add("Area", typeof(string));
                dt.Columns.Add("MediaVehicle", typeof(string));
                dt.Columns.Add("MediaType", typeof(string));
                dt.Columns.Add("CartRate", typeof(string));
                dt.Columns.Add("ActualSiteOwner", typeof(string));
                dt.Columns.Add("SiteImage", typeof(string));
                dt.Columns.Add("Comments", typeof(string));

                DataRow dr = null;
                dr = dt.NewRow();
                dr["FK_VendorID"] = model.VendorID;
                dr["SiteName"] = model.SiteName;
                dr["Pincode"] = model.Pincode;
                dr["State"] = model.StateName;
                dr["City"] = model.City;
                dr["Location"] = model.Location;
                dr["Facing"] = model.Facing;
                dr["Rational"] = model.Rational;
                dr["Height"] = model.Height;
                dr["Width"] = model.Width;
                dr["Quantity"] = model.Quantity;
                dr["Side"] = model.Side;
                dr["Area"] = model.Area;
                dr["MediaVehicle"] = model.MediaVehicleID;
                dr["MediaType"] = model.MediaTypeID;
                dr["CartRate"] = model.CartRate;
                dr["ActualSiteOwner"] = model.SiteOwner;
                dr["SiteImage"] = model.SiteImage == null ? null : model.SiteImage;
                dr["Comments"] = model.Comments;
                dt.Rows.Add(dr);
                Session["dt"] = dt;
            }

            List<Master> lst = new List<Master>();
            dt = (DataTable)Session["dt"];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Master obj = new Master();
                    obj.VendorID = dr["FK_VendorID"].ToString();
                    obj.SiteName = dr["SiteName"].ToString();
                    obj.Pincode = dr["Pincode"].ToString();
                    obj.StateName = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Location = dr["Location"].ToString();
                    obj.Facing = dr["Facing"].ToString();
                    obj.Rational = dr["Rational"].ToString();
                    obj.Height = dr["Height"].ToString();
                    obj.Width = dr["Width"].ToString();
                    obj.Quantity = dr["Quantity"].ToString();
                    obj.Side = dr["Side"].ToString();
                    obj.Area = dr["Area"].ToString();
                    obj.MediaVehicleID = dr["MediaVehicle"].ToString();
                    obj.MediaTypeID = dr["MediaType"].ToString();
                    obj.CartRate = dr["CartRate"].ToString();
                    obj.SiteOwner = dr["ActualSiteOwner"].ToString();
                    obj.SiteImage = dr["SiteImage"].ToString();

                    lst.Add(obj);
                }
                model.lstSites = lst;


            }
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType

            return View(model);
        }

        public ActionResult GetStateCity(string PinCode)
        {
            Common obj = new Common();
            obj.Pincode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.StateName = ds.Tables[0].Rows[0]["StateName"].ToString();
                obj.City = ds.Tables[0].Rows[0]["CityName"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("SiteMaster")]
        [OnAction(ButtonName = "SaveSite")]
        public ActionResult SaveSite(Master obj)
        {
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType
            if (TempData["SiteError"] == null)
            {
                ViewBag.errormsg = "none";
                ViewBag.saverrormsg = "none";
            }
            if (Session["dt"] == null)
            {
                TempData["SiteError"] = "Please Add Atleast One Site.";
                ViewBag.errormsg = "";
                ViewBag.saverrormsg = "none";
                return View();
            }
            try
            {
                obj.dtSiteMaster = (DataTable)Session["dt"];

                obj.AddedBy = Session["UserID"].ToString();

                DataSet ds = new DataSet();


                ds = obj.SaveSite();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["SiteError"] = "Site Save Successfully";
                        ViewBag.saverrormsg = "";
                        ViewBag.errormsg = "none";
                    }
                    else
                    {
                        TempData["SiteError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        ViewBag.errormsg = "";
                        ViewBag.saverrormsg = "none";

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SiteError"] = ex.Message;
                ViewBag.errormsg = "";
                ViewBag.saverrormsg = "none";
            }


            return RedirectToAction("SiteMaster");
        }

        public ActionResult SiteList()
        {
            if (TempData["SiteDelete"] == null)
            {
                ViewBag.saverrormsg = "none";
            }
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType
            return View();
        }
        [HttpPost]
        [ActionName("SiteList")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetSiteList(Master objmaster)
        {
            if (TempData["SiteDelete"] == null)
            {
                ViewBag.saverrormsg = "none";
            }
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "All", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType

            List<Master> lst = new List<Master>();
            objmaster.MediaTypeID = objmaster.MediaTypeID == "0" ? null : objmaster.MediaTypeID;
            objmaster.MediaVehicleID = objmaster.MediaVehicleID == "0" ? null : objmaster.MediaVehicleID;
            objmaster.VendorID = objmaster.VendorID == "0" ? null : objmaster.VendorID;
            DataSet ds = objmaster.GetSiteList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.SiteID = Crypto.Encrypt(dr["PK_SiteID"].ToString());
                    obj.VendorID = dr["FK_VendorID"].ToString();
                    obj.SiteName = dr["SiteName"].ToString();
                    obj.Pincode = dr["Pincode"].ToString();
                    obj.StateName = dr["State"].ToString();
                    obj.City = dr["City"].ToString();
                    obj.Location = dr["Location"].ToString();
                    obj.Facing = dr["Facing"].ToString();
                    obj.Rational = dr["Rational"].ToString();
                    obj.Height = dr["Height"].ToString();
                    obj.Width = dr["Width"].ToString();
                    obj.Quantity = dr["Quantity"].ToString();
                    obj.Side = dr["Side"].ToString();
                    obj.Area = dr["Area"].ToString();
                    obj.MediaVehicleName = dr["MediaVehicleName"].ToString();
                    obj.MediaTypeName = dr["MediaTypeName"].ToString();
                    obj.CartRate = dr["CartRate"].ToString();
                    obj.SiteOwner = dr["ActualSiteOwner"].ToString();
                    obj.SiteImage = dr["SiteImage"].ToString();
                    lst.Add(obj);
                }
                objmaster.lstSites = lst;
            }
            return View(objmaster);
        }

        [HttpPost]
        [ActionName("SiteMaster")]
        [OnAction(ButtonName = "UpdateSite")]
        public ActionResult UpdateSite(Master obj, HttpPostedFileBase postedFile)
        {
            ViewBag.saverrormsg = "none";
            ViewBag.errormsg = "none";
            if (postedFile != null)
            {
                obj.SiteImage = "../images/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(Path.Combine(Server.MapPath(obj.SiteImage)));
            }
            #region BindVendor
            Common objcomm = new Common();
            List<SelectListItem> ddlVendors = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindVendor();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendors.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlVendors.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["Pk_VendorId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlVendors = ddlVendors;
            #endregion BindVendor

            #region BindMediaVechile

            List<SelectListItem> ddlMediaVehicle = new List<SelectListItem>();
            ds1 = objcomm.GetMediaVehicle();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaVehicle.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaVehicle.Add(new SelectListItem { Text = r["MediaVehicleName"].ToString(), Value = r["PK_MediaVehicleID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaVehicle = ddlMediaVehicle;
            #endregion BindMediaVechile

            #region BindMediaType

            List<SelectListItem> ddlMediaType = new List<SelectListItem>();
            ds1 = objcomm.GetMediaType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlMediaType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlMediaType.Add(new SelectListItem { Text = r["MediaTypeName"].ToString(), Value = r["PK_MediaTypeID"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlMediaType = ddlMediaType;
            #endregion BindMediaType

            if (TempData["SiteError"] == null)
            {
                ViewBag.errormsg = "none";
                ViewBag.saverrormsg = "none";
            }

            try
            {

                obj.UpdatedBy = Session["UserID"].ToString();
                obj.SiteID = Crypto.Decrypt(obj.SiteID);
                DataSet ds = new DataSet();


                ds = obj.UpdateSite();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["SiteError"] = "Site Updated Successfully";
                        ViewBag.saverrormsg = "";
                        ViewBag.errormsg = "none";
                    }
                    else
                    {
                        TempData["SiteError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        ViewBag.errormsg = "";
                        ViewBag.saverrormsg = "none";

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SiteError"] = ex.Message;
                ViewBag.errormsg = "";
                ViewBag.saverrormsg = "none";
            }


            return RedirectToAction("SiteMaster");
        }

        public ActionResult DeleteSite(string SiteId)
        {
            Master obj = new Master();
            try
            {

                obj.DeletedBy = Session["UserID"].ToString();
                obj.SiteID = Crypto.Decrypt(SiteId);
                DataSet ds = new DataSet();


                ds = obj.DeleteSite();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["SiteDelete"] = "Site Deleted Successfully";


                    }
                    else
                    {
                        TempData["SiteDelete"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SiteDelete"] = ex.Message;

            }
            ViewBag.saverrormsg = "";
            return RedirectToAction("SiteList");
        }
        #endregion SiteMaster Start

        #region ServiceMaster Start
        public ActionResult ServiceMaster(string ServiceId)
        {
            #region ddldateformat
            List<SelectListItem> ddldateformat = Common.BindDateFormat();
            ViewBag.ddldateformat = ddldateformat;
            #endregion ddldateformat
            Master obj = new Master();
            if (TempData["ServiceError"] == null)
            {
                ViewBag.errormsg = "none";

            }


            if ((ServiceId) != "" && ServiceId != null)
            {
                ViewBag.Isvisible = "none";
                Master objmaster = new Master();
                objmaster.ServiceId = Crypto.Decrypt(ServiceId);
                DataSet ds = objmaster.GetServiceList();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        obj.ServiceId = (dr["PK_ServiceId"].ToString());
                        obj.ServiceName = dr["ServiceName"].ToString();
                        obj.HSNCode = dr["HSNCode"].ToString();
                        obj.CGST = dr["GST"].ToString();
                        obj.IGST = dr["IGST"].ToString();
                        obj.SGST = dr["SGST"].ToString();
                        obj.DateFormat = dr["DateFormat"].ToString();
                    }
                }
            }
            else
            {
                ViewBag.Isvisible = "";
            }


            return View(obj);
        }

        [HttpPost]
        [ActionName("ServiceMaster")]
        [OnAction(ButtonName = "SaveService")]
        public ActionResult SaveService(Master obj)
        {

            if (TempData["ServiceError"] == null)
            {
                ViewBag.errormsg = "none";

            }

            try
            {


                obj.AddedBy = Session["UserID"].ToString();

                DataSet ds = new DataSet();


                ds = obj.SaveService();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["ServiceError"] = "Service Save Successfully";

                    }
                    else
                    {
                        TempData["ServiceError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceError"] = ex.Message;

            }
            ViewBag.errormsg = "";

            return RedirectToAction("ServiceMaster");
        }

        public ActionResult ServiceList()
        {
            if (TempData["ServiceDelete"] == null)
            {
                ViewBag.saverrormsg = "none";
            }

            return View();
        }
        [HttpPost]
        [ActionName("ServiceList")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetServiceList(Master objmaster)
        {
            if (TempData["SiteDelete"] == null)
            {
                ViewBag.saverrormsg = "none";
            }


            List<Master> lst = new List<Master>();

            DataSet ds = objmaster.GetServiceList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.ServiceId = Crypto.Encrypt(dr["PK_ServiceId"].ToString());
                    obj.ServiceName = dr["ServiceName"].ToString();
                    obj.HSNCode = dr["HSNCode"].ToString();
                    obj.CGST = dr["GST"].ToString();
                    obj.IGST = dr["IGST"].ToString();
                    obj.SGST = dr["SGST"].ToString();
                    obj.DateFormat = dr["DateFormat"].ToString();
                    lst.Add(obj);
                }
                objmaster.lstSites = lst;
            }
            return View(objmaster);
        }

        [HttpPost]
        [ActionName("ServiceMaster")]
        [OnAction(ButtonName = "UpdateService")]
        public ActionResult UpdateService(Master obj)
        {
            ViewBag.errormsg = "none";

            if (TempData["ServiceError"] == null)
            {
                ViewBag.errormsg = "none";

            }

            try
            {

                obj.UpdatedBy = Session["UserID"].ToString();
                obj.ServiceId = Crypto.Decrypt(obj.ServiceId);
                DataSet ds = new DataSet();


                ds = obj.UpdateService();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["dt"] = null;
                        TempData["ServiceError"] = "Service Updated Successfully";


                    }
                    else
                    {
                        TempData["ServiceError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceError"] = ex.Message;

            }

            ViewBag.errormsg = "";
            return RedirectToAction("ServiceMaster");
        }

        public ActionResult DeleteService(string ServiceId)
        {
            Master obj = new Master();
            try
            {

                obj.DeletedBy = Session["UserID"].ToString();
                obj.ServiceId = Crypto.Decrypt(ServiceId);
                DataSet ds = new DataSet();


                ds = obj.DeleteService();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["ServiceDelete"] = "Service Deleted Successfully";


                    }
                    else
                    {
                        TempData["ServiceDelete"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceDelete"] = ex.Message;

            }
            ViewBag.saverrormsg = "";
            return RedirectToAction("ServiceList");
        }
        #endregion ServiceMaster End  

        public ActionResult InterActionMaster(string PK_InterActionId)
        {
            Master model = new Master();

            if (PK_InterActionId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.PK_InterActionId = PK_InterActionId;
                    DataSet ds = obj.ListInterAction();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.PK_InterActionId = ds.Tables[0].Rows[0]["PK_InterActionId"].ToString();
                        obj.InterActionName = ds.Tables[0].Rows[0]["InterActionName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("InterActionMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult InterActionMaster(Master model)
        {

            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertInterAction();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "InterAction is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("InterActionMaster");
        }

        public ActionResult InterActionList(string PK_InterActionId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListInterAction();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_InterActionId = r["PK_InterActionId"].ToString();
                    obj.InterActionName = r["InterActionName"].ToString();

                    lst1.Add(obj);
                }
                model.lstInterAction = lst1;
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("InterActionMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateInterActionList(string PK_InterActionId, string InterActionName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.PK_InterActionId = PK_InterActionId;
                obj.InterActionName = InterActionName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateInterAction();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "InterActionList";
                        Controller = "Master";

                        TempData["Delete"] = "InterAction is Successfully Updated!";
                    }
                    else
                    {
                        //Session["PK_InterActionId"] = PK_InterActionId;
                        FormName = "InterActionMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult CategoryMaster(string Pk_CategoryId)
        {
            Master model = new Master();

            if (Pk_CategoryId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_CategoryId = Pk_CategoryId;
                    DataSet ds = obj.ListCategory();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_CategoryId = ds.Tables[0].Rows[0]["Pk_CategoryId"].ToString();
                        obj.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("CategoryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult CategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Category is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("CategoryMaster");
        }

        public ActionResult CategoryList(string Pk_CategoryId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListCategory();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_CategoryId = r["Pk_CategoryId"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();

                    lst1.Add(obj);
                }
                model.lstCategory = lst1;
            }
            return View(model);

        }


        [HttpPost]
        [ActionName("CategoryMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateCategoryList(string Pk_CategoryId, string CategoryName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_CategoryId = Pk_CategoryId;
                obj.CategoryName = CategoryName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "CategoryList";
                        Controller = "Master";

                        TempData["Delete"] = "Category is Successfully Updated!";
                    }
                    else
                    {
                        Session["Pk_CategoryId"] = Pk_CategoryId;
                        FormName = "CategoryMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }


        public ActionResult DataSourceMaster(string Pk_SourceId)
        {
            Master model = new Master();

            if (Pk_SourceId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_SourceId = Pk_SourceId;
                    DataSet ds = obj.ListDataSource();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_SourceId = ds.Tables[0].Rows[0]["Pk_SourceId"].ToString();
                        obj.SourceName = ds.Tables[0].Rows[0]["SourceName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("DataSourceMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult DataSourceMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertDataSource();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Source Of Data is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("DataSourceMaster");
        }

        public ActionResult DataSourceList(string Pk_SourceId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListDataSource();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_SourceId = r["Pk_SourceId"].ToString();
                    obj.SourceName = r["SourceName"].ToString();

                    lst1.Add(obj);
                }
                model.lstSource = lst1;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("DataSourceMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateDataSourceList(string Pk_SourceId, string SourceName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_SourceId = Pk_SourceId;
                obj.SourceName = SourceName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateDataSource();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "DataSourceList";
                        Controller = "Master";

                        TempData["Delete"] = "Data Source is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_SourceId"] = Pk_SourceId;
                        FormName = "DataSourceMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }
        public ActionResult ProspectActivityMaster(string Pk_ActivityId)
        {
            Master model = new Master();

            if (Pk_ActivityId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_ActivityId = Pk_ActivityId;
                    DataSet ds = obj.ListActivity();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ActivityId = ds.Tables[0].Rows[0]["Pk_ActivityId"].ToString();
                        obj.ActivityName = ds.Tables[0].Rows[0]["ActivityName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("ProspectActivityMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult ProspectActivityMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertProspectActivity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Prospect Activity is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ProspectActivityMaster");
        }

        public ActionResult ProspectActivityList(string Pk_ActivityId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListActivity();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ActivityId = r["Pk_ActivityId"].ToString();
                    obj.ActivityName = r["ActivityName"].ToString();

                    lst1.Add(obj);
                }
                model.lstActivity = lst1;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ProspectActivityMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProspectActivityList(string Pk_ActivityId, string ActivityName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_ActivityId = Pk_ActivityId;
                obj.ActivityName = ActivityName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateActivity();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "ProspectActivityList";
                        Controller = "Master";

                        TempData["Delete"] = "Prospect Activity is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_ActivityId"] = Pk_ActivityId;
                        FormName = "ProspectActivityMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult BusinessChanceMaster(string Pk_BusinessChanceId)
        {
            Master model = new Master();

            if (Pk_BusinessChanceId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_BusinessChanceId = Pk_BusinessChanceId;
                    DataSet ds = obj.ListBusinessChance();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_BusinessChanceId = ds.Tables[0].Rows[0]["Pk_BusinessChanceId"].ToString();
                        obj.ChanceName = ds.Tables[0].Rows[0]["ChanceName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        [ActionName("BusinessChanceMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult BusinessChanceMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.InsertBusinessChance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Business Chance is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("BusinessChanceMaster");
        }

        public ActionResult BusinessChanceList(string Pk_BusinessChanceId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListBusinessChance();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_BusinessChanceId = r["Pk_BusinessChanceId"].ToString();
                    obj.ChanceName = r["ChanceName"].ToString();

                    lst1.Add(obj);
                }
                model.lstChance = lst1;
            }
            return View(model);


        }

        [HttpPost]
        [ActionName("BusinessChanceMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBusinessChanceList(string Pk_BusinessChanceId, string ChanceName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_BusinessChanceId = Pk_BusinessChanceId;
                obj.ChanceName = ChanceName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateBusinessChance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "BusinessChanceList";
                        Controller = "Master";

                        TempData["Delete"] = "Business Chance is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_BusinessChanceId"] = Pk_BusinessChanceId;
                        FormName = "BusinessChanceMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }

        public ActionResult ProductCategoryMaster(string Pk_ProductCategoryId)
        {
            Master model = new Master();

            if (Pk_ProductCategoryId != null)
            {
                Master obj = new Master();
                try
                {
                    obj.Pk_ProductCategoryId = Pk_ProductCategoryId;
                    DataSet ds = obj.ListProductCategory();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.Pk_ProductCategoryId = ds.Tables[0].Rows[0]["Pk_ProductCategoryId"].ToString();
                        obj.ProductCategoryName = ds.Tables[0].Rows[0]["ProductCategoryName"].ToString();

                    }
                }
                catch (Exception ex)
                {
                    TempData["Master"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        [ActionName("ProductCategoryMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult ProductCategoryMaster(Master model)
        {
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.ExpectedProductCategoryInsert();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Product Category is Successfully Added";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ProductCategoryMaster");
        }

        public ActionResult ProductCategoryList(string Pk_ProductCategoryId)
        {
            Master model = new Master();
            List<Master> lst1 = new List<Master>();

            DataSet ds = model.ListProductCategory();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ProductCategoryId = r["Pk_ProductCategoryId"].ToString();
                    obj.ProductCategoryName = r["ProductCategoryName"].ToString();

                    lst1.Add(obj);
                }
                model.lstProductCategory = lst1;
            }
            return View(model);

        }

        [HttpPost]
        [ActionName("ProductCategoryMaster")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProductCategoryList(string Pk_ProductCategoryId, string ProductCategoryName)
        {
            string FormName = "";
            string Controller = "";
            Master obj = new Master();
            try
            {
                obj.Pk_ProductCategoryId = Pk_ProductCategoryId;
                obj.ProductCategoryName = ProductCategoryName.Trim();
                obj.UpdatedBy = Session["UserID"].ToString();
                DataSet ds = obj.UpdateProductCategory();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        FormName = "ProductCategoryList";
                        Controller = "Master";

                        TempData["Delete"] = "ProductCategory is Successfully Updated!";
                    }
                    else
                    {
                        //Session["Pk_ProductCategoryId"] = Pk_ProductCategoryId;
                        FormName = "ProductCategoryMaster";
                        Controller = "Master";
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(FormName, Controller);

        }



        public ActionResult VisitorForm(string Id, string VId)
        {
            Master model = new Master();
            DataSet ds = new DataSet();
            if (Id != null)
            {
                model.VisitorId = Crypto.Decrypt(Id);
                if (VId != null)
                {
                    model.VisitorDetailId = Crypto.Decrypt(VId);
                }
                List<Master> lst = new List<Master>();
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                ds = model.GetVisitorDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.SiteID = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    model.AssociateID = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["CustomerMobile"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.VehicleDetails = ds.Tables[0].Rows[0]["VehicleDetails"].ToString();
                    model.Pk_CategoryId = ds.Tables[0].Rows[0]["Pk_CategoryId"].ToString();
                    model.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();
                    model.VehicleNumber = ds.Tables[0].Rows[0]["VehicleDetails"].ToString();
                    model.PickUpLocation = ds.Tables[0].Rows[0]["PickUpLocation"].ToString();
                    model.DropLocation = ds.Tables[0].Rows[0]["DropLocation"].ToString();
                    model.PickUpTime = ds.Tables[0].Rows[0]["PickUpTime"].ToString();
                    model.DropTime = ds.Tables[0].Rows[0]["DropTime"].ToString();
                }
            }

            int count1 = 0;
            List<SelectListItem> ddlsite = new List<SelectListItem>();
            ds = model.GetSiteName();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //if (count1 == 0)
                    //{
                    //    ddlsite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    //}
                    ddlsite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["SiteName"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlsite = ddlsite;

            int count2 = 0;
            List<SelectListItem> ddlCategoryName = new List<SelectListItem>();
            DataSet ds1 = model.GetCategoryName();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlCategoryName.Add(new SelectListItem { Text = "Select Category", Value = "0" });
                    }
                    ddlCategoryName.Add(new SelectListItem { Text = dr["CategoryName"].ToString(), Value = dr["Pk_CategoryId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlCategoryName = ddlCategoryName;
            return View(model);
        }


        public ActionResult GetAssociateName(string AssociateID)
        {
            try
            {
                Master model = new Master();
                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public JsonResult AddProfile(Master userDetail)
        {
            var profile = Request.Files;
            bool status = false;
            var datavalue = Request["dataValue"];
            userDetail.VisitDate = DateTime.Now.ToString();
            //string imgname = string.Empty;
            //string ImageName = string.Empty;
            //HttpPostedFileBase postedFile = Request.Files["Image"];
            //if (postedFile != null)
            //{
            //    userDetail.Image = "../VisitorImage/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
            //    postedFile.SaveAs(Path.Combine(Server.MapPath(userDetail.Image)));
            //}
            //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            //    var logo = System.Web.HttpContext.Current.Request.Files["Image"];
            //    if (logo.ContentLength > 0)
            //    {
            //        var profileName = Path.GetFileName(logo.FileName);
            //        var ext = Path.GetExtension(logo.FileName);

            //        ImageName = profileName;

            //        var comPath = Server.MapPath("../VisitorImage/") + ImageName;

            //        logo.SaveAs(comPath);
            //        userDetail.Image = comPath;
            //    }

            //}
            //else
            //userDetail.Image = Server.MapPath("../VisitorImage/") + "profile.jpg";
            userDetail.VisitDate = string.IsNullOrEmpty(userDetail.VisitDate) ? null : Common.ConvertToSystemDate(userDetail.VisitDate, "dd/MM/yyyy");
            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(Request["dataValue"]);
            DataTable VisitorDetails = new DataTable();
            VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            userDetail.dtVisitorDetails = VisitorDetails;
            userDetail.CreatedBy = Session["UserID"].ToString();
            DataSet ds = new DataSet();

            if (userDetail.VisitorId == null)
            {
                ds = userDetail.SaveVisitorDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Visitor"] = "Visitor Details saved successfully";
                        status = true;
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                userDetail.CreatedBy = Session["UserID"].ToString();
                ds = userDetail.UpdateVisitorDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Visitor"] = "Visitor Details Update successfully";
                        status = true;
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return new JsonResult {Data = new { status = status } };
            //return Json(userDetail, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult save(Master obj, string dataValue, string SiteID, string AssociateID, string Amount, string VisiteDate, string postedFile1)// string AssociateName,
        //{


        //    obj.VisitDate = string.IsNullOrEmpty(VisiteDate) ? null : Common.ConvertToSystemDate(VisiteDate, "dd/MM/yyyy");
        //    bool status = false;
        //    var isValidModel = TryUpdateModel(obj);
        //    var jss = new JavaScriptSerializer();
        //    var jdv = jss.Deserialize<dynamic>(dataValue);

        //    //var serializeData = JsonConvert.DeserializeObject<List<Customer>>(empdata);
        //    //System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
        //    DataTable VisitorDetails = new DataTable();

        //    VisitorDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
        //    obj.dtVisitorDetails = VisitorDetails;
        //    obj.AddedBy = Session["UserID"].ToString();


        //    //if (Picfile != null)
        //    //{
        //    //    var randomValue = DateTime.Now.ToString("yyyyMMddHHmmssfff");

        //    //    string path = Server.MapPath("~/UploadFiles/");
        //    //    if (!Directory.Exists(path))
        //    //    {
        //    //        Directory.CreateDirectory(path);
        //    //    }
        //    //    var fileName = randomValue + Path.GetFileName(Picfile.FileName);
        //    //    Picfile.SaveAs(path + fileName);
        //    //    obj.Image = fileName;


        //    DataSet ds = new DataSet();
        //    ds = obj.SaveVisitorDetails();
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //        {
        //            TempData["Visitor"] = "Visitor Details saved successfully";
        //            status = true;
        //        }
        //        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        TempData["Visitor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //    }
        //    //}
        //    return new JsonResult { Data = new { status = status } };
        //}
        public ActionResult GetVisitorDetails()
        {
            //Master model = new Master();
            //List<Master> lst = new List<Master>();
            //DataSet dss = model.GetVisitorDetails();
            //if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in dss.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.VisitorId = r["PK_VisitorDetailsID"].ToString();
            //obj.SiteName = r["SiteName"].ToString();
            //obj.AssociateID = r["LoginId"].ToString();
            //obj.AssociateName = r["AssociateName"].ToString();
            //obj.Amount = r["Amount"].ToString();
            //obj.VisitDate = r["VisitDate"].ToString();
            //obj.VisitorImage = r["VisitorImage"].ToString();
            //        obj.Name = r["Name"].ToString();
            //        obj.MobileNo = r["Mobile"].ToString();
            //        obj.Address = r["Address"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.VisitorList = lst;
            //}
            return View();
        }
        [HttpPost]
        [ActionName("GetVisitorDetails")]
        public ActionResult SearchVisitorDetails(Master model)
        {
            List<Master> lst = new List<Master>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetVisitorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    
                    Master obj = new Master();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_VisitorMasterID"].ToString());
                    obj.EncryptedId = Crypto.Encrypt(r["PK_VisitorDetailsID"].ToString());
                    //obj.VisitorId = r["PK_VisitorMasterID"].ToString();
                    // obj.VisitorImage = r["VisitorImage"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.CustomerMobile = r["CustomerMobile"].ToString();
                    obj.VehicleDetails = r["VehicleDetails"].ToString();
                    obj.CategoryName = r["CategoryName"].ToString();
                    // obj.VisitDate = r["VisitDate"].ToString();
                    obj.VisitDate = r["VisitDate"].ToString();
                    //obj.VisitorImage = r["VisitorImage"].ToString();
                    obj.VehicleNumber = r["VehicleDetails"].ToString();
                    obj.PickUpLocation = r["PickUpLocation"].ToString();
                    obj.DropLocation = r["DropLocation"].ToString();
                    obj.PickUpTime = r["PickUpTime"].ToString();
                    obj.DropTime = r["DropTime"].ToString();
                    //obj.Address = r["Address"].ToString();
                    lst.Add(obj);
                    
                }
                model.VisitorList = lst;
            }
            return View(model);
        }
        public ActionResult PrintVisitor(string Id, string VId)
        {
            Master newdata = new Master();
            List<Master> lstvisitor = new List<Master>();
            newdata.VisitorId = Crypto.Decrypt(Id);
            newdata.VisitorDetailId = Crypto.Decrypt(VId);
            DataSet ds = newdata.VisitorListById();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.PK_VisitorID = ds.Tables[0].Rows[0]["PK_VisitorMasterID"].ToString();
                ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.CategoryName = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                //ViewBag.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                ViewBag.VisitDate = ds.Tables[0].Rows[0]["VisitDate"].ToString();
                //ViewBag.Image = ds.Tables[0].Rows[0]["VisitorImage"].ToString();
                ViewBag.VehicleDetails = ds.Tables[0].Rows[0]["VehicleDetails"].ToString();
                ViewBag.PickUpLocation = ds.Tables[0].Rows[0]["PickUpLocation"].ToString();
                ViewBag.DropLocation = ds.Tables[0].Rows[0]["DropLocation"].ToString();
                ViewBag.PickUpTime = ds.Tables[0].Rows[0]["PickUpTime"].ToString();
                ViewBag.DropTime = ds.Tables[0].Rows[0]["DropTime"].ToString();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Master obj = new Master();
                        obj.Name = r["Name"].ToString();
                        obj.MobileNo = r["Mobile"].ToString();
                        obj.Address = r["Address"].ToString();
                        lstvisitor.Add(obj);
                    }
                }
                newdata.VisitorList = lstvisitor;
            }
            return View(newdata);
        }

        public ActionResult UserTypeMaster(string Id)
        {
            Master model = new Master();
            model.UserTypeId = Id;
            if (model.UserTypeId != null)
            {
                DataSet ds = model.GetUserTypeDeatils();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //model.UserTypeId = ds.Tables[0].Rows[0]["PK_UserTypeId"].ToString();
                    model.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();
                    //model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UserTypeMaster")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult UserTypeMaster(Master model)
        {
            try
            {
                if (model.UserTypeId == null)
                {
                    model.AddedBy = Session["UserID"].ToString();
                    DataSet ds = model.SaveUserType();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Error"] = "Designation save Successfully";
                        }
                        else
                        {
                            TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    model.AddedBy = Session["UserID"].ToString();
                    DataSet ds1 = model.UpdateUserType();
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Error"] = "Designation Update Successfully";
                        }
                        else
                        {
                            TempData["Error"] = ds1.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("UserTypeMaster", "Master");
        }

        public ActionResult GetDetailsUserType()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetUserTypeDeatils();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.UserTypeId = r["PK_UserTypeId"].ToString();
                    obj.UserType = r["UserType"].ToString();
                    //obj.Description = r["Description"].ToString();
                    lst.Add(obj);
                }
                model.lstUserType = lst;
            }
            return View(model);
        }


        public ActionResult DeleteUserType(string Id)
        {
            Master model = new Master();
            try
            {
                model.UserTypeId = Id;
                model.AddedBy = Session["UserID"].ToString();
                DataSet ds = model.DeleteUserType();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Designation deleted Successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("GetDetailsUserType", "Master");
        }


        public ActionResult DeleteMaster(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.Pk_CategoryId = Id;
                DataSet ds = model.DeleteCategory();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Category Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("CategoryList", "Master");
        }


        public ActionResult DeleteInterActionMaster(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.PK_InterActionId = Id;
                DataSet ds = model.DeleteInterAction();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Record Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("InterActionList", "Master");
        }



        public ActionResult DeleteDataSource(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.Pk_SourceId = Id;
                DataSet ds = model.DeleteDataSource();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Data Source Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("DataSourceList", "Master");
        }



        public ActionResult DeleteProspectActivityMaster(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.Pk_ActivityId = Id;
                DataSet ds = model.DeleteProspectActivity();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Prospect Activity Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("ProspectActivityList", "Master");
        }


        public ActionResult DeleteBusinessChanceMaster(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.Pk_BusinessChanceId = Id;
                DataSet ds = model.DeleteBusinessChance();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Business Chance Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("BusinessChanceList", "Master");
        }


        public ActionResult DeleteProductCategoryMaster(string Id)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.Pk_ProductCategoryId = Id;
                DataSet ds = model.DeleteProductCategory();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Product Category Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("ProductCategoryList", "Master");
        }


        public ActionResult DeleteVisitorMaster(string Id,string VId)
        {
            Master model = new Master();
            try
            {
                model.AddedBy = Session["UserID"].ToString();
                model.VisitorId = Crypto.Decrypt(Id);
                model.VisitorDetailId = Crypto.Decrypt(VId);
                DataSet ds = model.DeleteVisitor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Category"] = "Visitor Details Delete Successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Category"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Category"] = ex.Message;
            }
            return RedirectToAction("GetVisitorDetails", "Master");
        }


    }
}
