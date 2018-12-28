using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace UCMHelper
{
    public class UCMWarehouse
    {
        public void AddUpdateFabricSort(string function, int? fabricSortID, string fabricName, string warp, string weft, string reed, string pic, string width, string gsm, ref string retParamVal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FabricSortOperation");
            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int16, fabricSortID);
            db.AddInParameter(cmd, "Name", DbType.String, fabricName);
            db.AddInParameter(cmd, "WarpCount", DbType.String, warp);
            db.AddInParameter(cmd, "WeftCount", DbType.String, weft);
            db.AddInParameter(cmd, "Reed", DbType.String, reed);
            db.AddInParameter(cmd, "Pic", DbType.String, pic);
            db.AddInParameter(cmd, "Width", DbType.String, width);
            db.AddInParameter(cmd, "GSM", DbType.String, gsm);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

            retParamVal = db.GetParameterValue(cmd, "retVal").ToString();
        }

        public DataTable GetFabricSort(int? fabricSortID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FabricSortOperation");
            db.AddInParameter(cmd, "Function", DbType.String, (fabricSortID == null) ? "GETALL" : "GET");
            db.AddInParameter(cmd, "ID", DbType.Int16, fabricSortID);
            db.AddInParameter(cmd, "Name", DbType.String, string.Empty);
            db.AddInParameter(cmd, "WarpCount", DbType.String, string.Empty);
            db.AddInParameter(cmd, "WeftCount", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Reed", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Pic", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Width", DbType.String, string.Empty);
            db.AddInParameter(cmd, "GSM", DbType.String, string.Empty);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();

        }

        public void AddUpdateVendor(string function, int? vendorID, string name, string address, string city, string phone, string tin, string gst, ref string retParamVal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_VendorOperation");
            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int16, vendorID);
            db.AddInParameter(cmd, "VenName", DbType.String, name);
            db.AddInParameter(cmd, "VenAddress", DbType.String, address);
            db.AddInParameter(cmd, "VenCity", DbType.String, city);
            db.AddInParameter(cmd, "VenPhone", DbType.String, phone);
            db.AddInParameter(cmd, "VenTIN", DbType.String, tin);
            db.AddInParameter(cmd, "VenGST", DbType.String, gst);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

            retParamVal = db.GetParameterValue(cmd, "retVal").ToString();
        }

        public DataTable GetVendor(int? vendorID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_VendorOperation");
            db.AddInParameter(cmd, "Function", DbType.String, (vendorID == null) ? "GETALL" : "GET");
            db.AddInParameter(cmd, "ID", DbType.Int16, vendorID);
            db.AddInParameter(cmd, "VenName", DbType.String, string.Empty);
            db.AddInParameter(cmd, "VenAddress", DbType.String, string.Empty);
            db.AddInParameter(cmd, "VenCity", DbType.String, string.Empty);
            db.AddInParameter(cmd, "VenPhone", DbType.String, string.Empty);
            db.AddInParameter(cmd, "VenTIN", DbType.String, string.Empty);
            db.AddInParameter(cmd, "VenGST", DbType.String, string.Empty);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }


        public void AddUpdateInwardMaster(string function, int? inwardID, string inwdRef, DateTime inwdDate,
            int vendorID, int sortID, int originID, int approxMtrs, int @noOfItems, int approved, ref string retParamVal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardMasterOperation");
            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int16, inwardID);
            db.AddInParameter(cmd, "InwdRef", DbType.String, inwdRef);
            db.AddInParameter(cmd, "InwdDate", DbType.Date, DataFormatter.SafeDate(inwdDate));
            db.AddInParameter(cmd, "InwdDateTo", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "VendorID", DbType.Int32, vendorID);
            db.AddInParameter(cmd, "SortID", DbType.Int32, sortID);
            db.AddInParameter(cmd, "Origin", DbType.Int32, originID);
            db.AddInParameter(cmd, "ApproxMtrs", DbType.Int32, approxMtrs);
            db.AddInParameter(cmd, "NoOfItems", DbType.Int32, noOfItems);
            db.AddInParameter(cmd, "Approved", DbType.Int32, approved);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

            retParamVal = db.GetParameterValue(cmd, "retVal").ToString();
        }

        public DataTable GetInwardMaster(int inwardID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardMasterOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int16, inwardID);
            db.AddInParameter(cmd, "InwdRef", DbType.String, default(string));
            db.AddInParameter(cmd, "InwdDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "InwdDateTo", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "VendorID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "SortID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Origin", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ApproxMtrs", DbType.Int32, default(int));
            db.AddInParameter(cmd, "NoOfItems", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }


        public DataTable SearchInwardMaster(string inwardRef, DateTime inwardFromDate, DateTime inwardToDate,
            int vendorID, int sortID, int originID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardMasterOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "SEARCH");
            db.AddInParameter(cmd, "ID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "InwdRef", DbType.String, inwardRef);
            db.AddInParameter(cmd, "InwdDate", DbType.Date, DataFormatter.SafeDate(inwardFromDate));
            db.AddInParameter(cmd, "InwdDateTo", DbType.Date, DataFormatter.SafeDate(inwardToDate));
            db.AddInParameter(cmd, "VendorID", DbType.Int32, vendorID);
            db.AddInParameter(cmd, "SortID", DbType.Int32, sortID);
            db.AddInParameter(cmd, "Origin", DbType.Int32, originID);
            db.AddInParameter(cmd, "ApproxMtrs", DbType.Int32, default(int));
            db.AddInParameter(cmd, "NoOfItems", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetInwardItems(int inwardMasterID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardItemsOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GETBYMASTERID");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdID", DbType.Int32, inwardMasterID);
            db.AddInParameter(cmd, "CheckingDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "Details", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Approved", DbType.Int16, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetInwardItem(int inwardItemID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardItemsOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "WHInwdID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "CheckingDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "Details", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Approved", DbType.Int16, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public void AddInwardItem(int inwardMasterID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardItemsOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "ADD");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdID", DbType.Int32, inwardMasterID);
            db.AddInParameter(cmd, "CheckingDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "Details", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Approved", DbType.Int16, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public void UpdateInwardItem(int inwardItemID, DateTime checkingDate, string details, string remarks, int approved)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardItemsOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "UPDATE");
            db.AddInParameter(cmd, "ID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "WHInwdID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "CheckingDate", DbType.Date, DataFormatter.SafeDate(checkingDate));
            db.AddInParameter(cmd, "Details", DbType.String, details);
            db.AddInParameter(cmd, "Remarks", DbType.String, remarks);
            db.AddInParameter(cmd, "Approved", DbType.Int16, approved);

            db.ExecuteNonQuery(cmd);
        }

        public void DeleteInwardItem(int inwardItemID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHInwardItemsOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "WHInwdID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "CheckingDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "Details", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Approved", DbType.Int16, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public DataTable GetCheckingReport(int inwardItemID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHCROperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "Meter", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DefectID", DbType.Int32, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public void AddCheckingReport(int inwardItemID, int meter, int defectID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHCROperation");
            db.AddInParameter(cmd, "Function", DbType.String, "ADD");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "Meter", DbType.Int32, meter);
            db.AddInParameter(cmd, "DefectID", DbType.Int32, defectID);

            db.ExecuteNonQuery(cmd);
        }

        public void DeleteCheckingReport(int crID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHCROperation");
            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int32, crID);
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Meter", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DefectID", DbType.Int32, default(int));

            db.ExecuteNonQuery(cmd);
        }


        public DataTable GetPiece(int inwardItemID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "PieceMark", DbType.String, default(int));
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }
        public void AddPiece(int inwardItemID, string pieceMark, int gradeID, int length)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "ADD");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, inwardItemID);
            db.AddInParameter(cmd, "PieceMark", DbType.String, pieceMark);
            db.AddInParameter(cmd, "GradeID", DbType.Int32, gradeID);
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, length);
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public void DeletePiece(int pieceID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int32, pieceID);
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PieceMark", DbType.String, string.Empty);
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public DataTable GetApprovedPieces()
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GETAPPROVED");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PieceMark", DbType.String, default(int));
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetPieceByPackageID(int packageID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GETBYPACKAGE");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PieceMark", DbType.String, default(int));
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, packageID);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public void ClearPieceByPackageID(int packageID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "CLEARPACKAGE");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PieceMark", DbType.String, default(int));
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, packageID);

            db.ExecuteNonQuery(cmd);
        }

        public void UpdatePieceByPackageID(int pieceID, int packageID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPieceOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "UPDATEPACKAGE");
            db.AddInParameter(cmd, "ID", DbType.Int32, pieceID);
            db.AddInParameter(cmd, "WHInwdItemID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PieceMark", DbType.String, default(int));
            db.AddInParameter(cmd, "GradeID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ActualLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "AdjustedLength", DbType.Int32, default(int));
            db.AddInParameter(cmd, "PackageID", DbType.Int32, packageID);

            db.ExecuteNonQuery(cmd);
        }

        public DataTable SearchPackingDetails(DateTime packFromDate, DateTime packToDate,
            int packTypeID, string packDetails, int sortID, int deliveryPlanID, int invoiceID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "SEARCH");
            db.AddInParameter(cmd, "ID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "PkDetails", DbType.String, packDetails);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(packFromDate));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(packToDate));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, packTypeID);
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, sortID);
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, invoiceID);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetPackingDetail(int packingID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, packingID);
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.String, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public void AddUpdatePackingDetails(string function, int packID, string packDetails,
            DateTime packDate, int packTypeID, int packTare, int approved, ref string retParamVal)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int16, packID);
            db.AddInParameter(cmd, "PkDetails", DbType.String, packDetails);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(packDate));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, packTypeID);
            db.AddInParameter(cmd, "Tare", DbType.Int32, packTare);
            db.AddInParameter(cmd, "Approved", DbType.Int32, approved);
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

            retParamVal = db.GetParameterValue(cmd, "retVal").ToString();
        }


        public void DeletePackingDetail(int packID)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int16, packID);
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

        }

        public void ClearPackagesByDeliveryPlanID(int deliveryPlanID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "CLEARDELIVERYPLAN");
            db.AddInParameter(cmd, "ID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);
        }

        public void UpdatePackagesByDeliveryPlanID(int packageID, int deliveryPlanID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "UPDATEDELIVERYPLAN");
            db.AddInParameter(cmd, "ID", DbType.Int16, packageID);
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);
        }

        public DataTable GetApprovedPackages()
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GETAPPROVED");
            db.AddInParameter(cmd, "ID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetPackagesByDeliveryPlanID(int deliveryPlanID)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHPackingOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "GETBYDELIVERYPLAN");
            db.AddInParameter(cmd, "ID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "PkDetails", DbType.String, string.Empty);
            db.AddInParameter(cmd, "PkDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "PkToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TypeId", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Tare", DbType.Int32, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "FabricID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "DeliveryPlanID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable SearchDeliveryPlanDetails(int deliveryPlanID, int invoiceID, string remarks)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHDeliveryPlanOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "SEARCH");
            db.AddInParameter(cmd, "ID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "DPNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, remarks);
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, invoiceID);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetDeliveryPlanDetail(int deliveryPlanID)
        {
            return SearchDeliveryPlanDetails(deliveryPlanID, default(int), string.Empty);
        }

        public void AddUpdateDeliveryPlanDetails(string function, int deliveryPlanID, string deliveryPlanNo,
            string remarks, int approved, ref string retParamVal)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHDeliveryPlanOperation");
            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "DPNo", DbType.String, deliveryPlanNo);
            db.AddInParameter(cmd, "Remarks", DbType.String, remarks);
            db.AddInParameter(cmd, "Approved", DbType.Int32, approved);
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

        }

        public void DeleteDeliveryPlanDetail(int deliveryPlanID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHDeliveryPlanOperation");
            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int32, deliveryPlanID);
            db.AddInParameter(cmd, "DPNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, default(int));
            db.AddInParameter(cmd, "Approved", DbType.Int32, default(int));
            db.AddInParameter(cmd, "InvoiceID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);
        }


        public DataSet GetFabricStockInHand()
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_WHFabricStockInHand");
           
            retDs = db.ExecuteDataSet(cmd);

            return retDs;
        }
    }
}
