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
    public class UCMUserOperations
    {
        private DataTable UserLoginOperation(string functionality, int?  userID, string userName, string userPassword, string userDepartment, string userRoles)
        {
            DataSet retDS = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_UserLogin");
            db.AddInParameter(cmd, "Function", DbType.String, functionality);
            db.AddInParameter(cmd, "ID", DbType.Int16, userID);
            db.AddInParameter(cmd, "UserName", DbType.String, userName);
            db.AddInParameter(cmd, "UserPassword", DbType.String, userPassword);
            db.AddInParameter(cmd, "Department", DbType.String, userDepartment);
            db.AddInParameter(cmd, "Roles", DbType.String, userRoles);

            retDS = db.ExecuteDataSet(cmd);

            if (retDS.Tables != null && retDS.Tables.Count > 0)
                return retDS.Tables[0];
            else

                return new DataTable();

        }

        public DataTable UserLogin(string userName, string userPassword)
        {
            return UserLoginOperation("AUTH", null, userName, userPassword, string.Empty, string.Empty);
        }

        public void UserPasswordChange(int  userID, string userPassword)
        {
            UserLoginOperation("CHANGEPWD", userID, string.Empty, userPassword, string.Empty, string.Empty);
        }
    }
}
