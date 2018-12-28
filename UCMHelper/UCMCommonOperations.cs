using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace UCMHelper
{
    public class UCMCommonOperations
    {
        private DataTable getNameValue(string entityValue)
        {
            DataSet retDS = new DataSet();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("sp_GetNameValue");
            db.AddInParameter(cmd, "Entity", DbType.String, entityValue);

            retDS = db.ExecuteDataSet(cmd);

            if (retDS.Tables != null && retDS.Tables.Count > 0)
                return retDS.Tables[0];
            else

                return new DataTable();
        }

        public DataTable GetFabricGrade()
        {
            return getNameValue("FabricQuality");
        }

        public DataTable GetFabricDefects()
        {
            return getNameValue("FabricDefects");
        }

        public DataTable GetPackageType()
        {
            return getNameValue("PackageType");
        }

        public DataTable GetLooms()
        {
            return getNameValue("Looms");
        }

        public DataTable GetInwardOrigin()
        {
            return getNameValue("InwardOrigin");
        }

    }
}
