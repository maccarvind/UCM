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
    public class UCMFinance
    {
        public void PettyCashAddUpdate(string function, int ID, DateTime tranDate, string crDr, int companyID, int expHeadId, int expNatureID, string vendor,
            DateTime billDate, string billNo, string remarks, double amount)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashOperation");

            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "TranDate", DbType.Date, tranDate);
            db.AddInParameter(cmd, "TranToDate", DbType.Date, tranDate);
            db.AddInParameter(cmd, "CrDr", DbType.String, crDr);
            db.AddInParameter(cmd, "CompanyID", DbType.Int16, companyID);
            db.AddInParameter(cmd, "ExpHeadID", DbType.Int16, expHeadId);
            db.AddInParameter(cmd, "ExpNatureID", DbType.Int16, expNatureID);
            db.AddInParameter(cmd, "Vendor", DbType.String, vendor);
            db.AddInParameter(cmd, "BillDate", DbType.Date, billDate);
            db.AddInParameter(cmd, "BillNo", DbType.String, billNo);
            db.AddInParameter(cmd, "Remarks", DbType.String, remarks);
            db.AddInParameter(cmd, "Amount", DbType.Double, amount);
            db.AddInParameter(cmd, "Approved", DbType.String, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public DataTable PettyCashGetByID(int ID)
        {

            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "TranDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TranToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "CrDr", DbType.String, string.Empty);
            db.AddInParameter(cmd, "CompanyID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpHeadID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpNatureID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "Vendor", DbType.String, string.Empty);
            db.AddInParameter(cmd, "BillDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "BillNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Amount", DbType.Double, default(double));
            db.AddInParameter(cmd, "Approved", DbType.String, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable PettyCashGetByTranDate(DateTime tranDate)
        {
            return PettyCashSearch(tranDate, tranDate, string.Empty, default(int), default(int), default(int), string.Empty, default(double));
        }

        public void PettyCashApproveByID(int ID)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "APPROVE");
            db.AddInParameter(cmd, "ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "TranDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TranToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "CrDr", DbType.String, string.Empty);
            db.AddInParameter(cmd, "CompanyID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpHeadID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpNatureID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "Vendor", DbType.String, string.Empty);
            db.AddInParameter(cmd, "BillDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "BillNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Amount", DbType.Double, default(double));
            db.AddInParameter(cmd, "Approved", DbType.String, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public void PettyCashDeleteByID(int ID)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "DELETE");
            db.AddInParameter(cmd, "ID", DbType.Int32, ID);
            db.AddInParameter(cmd, "TranDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "TranToDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "CrDr", DbType.String, string.Empty);
            db.AddInParameter(cmd, "CompanyID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpHeadID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "ExpNatureID", DbType.Int16, default(int));
            db.AddInParameter(cmd, "Vendor", DbType.String, string.Empty);
            db.AddInParameter(cmd, "BillDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "BillNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Amount", DbType.Double, default(double));
            db.AddInParameter(cmd, "Approved", DbType.String, default(int));

            db.ExecuteNonQuery(cmd);
        }

        public DataTable PettyCashSearch(DateTime tranFromDate, DateTime tranToDate, string crDr, int companyID, int expHeadId, int expNatureID, string remarks,
            double amount)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "SEARCH");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "TranDate", DbType.Date, DataFormatter.SafeDate(tranFromDate));
            db.AddInParameter(cmd, "TranToDate", DbType.Date, DataFormatter.SafeDate(tranToDate));
            db.AddInParameter(cmd, "CrDr", DbType.String, crDr);
            db.AddInParameter(cmd, "CompanyID", DbType.Int16, companyID);
            db.AddInParameter(cmd, "ExpHeadID", DbType.Int16, expHeadId);
            db.AddInParameter(cmd, "ExpNatureID", DbType.Int16, expNatureID);
            db.AddInParameter(cmd, "Vendor", DbType.String, string.Empty);
            db.AddInParameter(cmd, "BillDate", DbType.Date, DataFormatter.SafeDate(string.Empty));
            db.AddInParameter(cmd, "BillNo", DbType.String, string.Empty);
            db.AddInParameter(cmd, "Remarks", DbType.String, remarks);
            db.AddInParameter(cmd, "Amount", DbType.Double, amount);
            db.AddInParameter(cmd, "Approved", DbType.String, default(int));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable PettyCashGetExpenseReport(int companyID, DateTime tranFromDate, DateTime tranToDate)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNPettyCashReport");

            db.AddInParameter(cmd, "CompanyID", DbType.Int16, companyID);
            db.AddInParameter(cmd, "FromDate", DbType.Date, DataFormatter.SafeDate(tranFromDate));
            db.AddInParameter(cmd, "ToDate", DbType.Date, DataFormatter.SafeDate(tranToDate));

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetCompanies()
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNCompanyGet");

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }


        public DataTable GetExpenseHeads()
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNExpenseMasterOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "GETHEAD");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ExpType", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ExpName", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetExpenseNature(int headId)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNExpenseMasterOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "GETNATURE");
            db.AddInParameter(cmd, "ID", DbType.Int32, default(int));
            db.AddInParameter(cmd, "ExpType", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ExpName", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, headId);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetExpenseByID(int id)
        {
            DataSet retDs = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNExpenseMasterOperation");

            db.AddInParameter(cmd, "Function", DbType.String, "GET");
            db.AddInParameter(cmd, "ID", DbType.Int32, id);
            db.AddInParameter(cmd, "ExpType", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ExpName", DbType.String, string.Empty);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, default(int));
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            retDs = db.ExecuteDataSet(cmd);

            if (retDs.Tables != null && retDs.Tables.Count > 0)
                return retDs.Tables[0];
            else

                return new DataTable();
        }

        public void AddUpdateExpense(string function, int id, string expType, string expName, int headId, ref string retParamVal)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_FNExpenseMasterOperation");

            db.AddInParameter(cmd, "Function", DbType.String, function);
            db.AddInParameter(cmd, "ID", DbType.Int32, id);
            db.AddInParameter(cmd, "ExpType", DbType.String, expType);
            db.AddInParameter(cmd, "ExpName", DbType.String, expName);
            db.AddInParameter(cmd, "ParentID", DbType.Int32, headId);
            db.AddOutParameter(cmd, "retVal", DbType.String, 100);

            db.ExecuteNonQuery(cmd);

            retParamVal = db.GetParameterValue(cmd, "retVal").ToString();
        }
    }
}
