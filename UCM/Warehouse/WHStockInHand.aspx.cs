using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCMHelper;

namespace UCM.Warehouse
{
    public partial class WHStockInHand : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                generateStockInHandReport();
            }
        }

        private void generateStockInHandReport()
        {
            DataTable dtSIHReport;

            UCMWarehouse whOps = new UCMWarehouse();
            dtSIHReport = whOps.GetFabricSort(null);

            dtSIHReport.Columns.Add(new DataColumn("Sno", typeof(int)));
            dtSIHReport.Columns.Add(new DataColumn("Fresh", typeof(int)));
            foreach (DataRow grade in UCMConst.FabricQuality.Rows)
            {
                dtSIHReport.Columns.Add(new DataColumn(grade["ID"].ToString(), typeof(int)));
                dtSIHReport.Columns.Add(new DataColumn("Count:" + grade["ID"].ToString(), typeof(int)));
            }

            dtSIHReport.Columns.Add(new DataColumn("Packed", typeof(int)));

            dtSIHReport.AcceptChanges();

            DataSet dsSIH = whOps.GetFabricStockInHand();
            DataRow[] selectedRows;
            int sno = default(int);

            if (dsSIH.Tables.Count > 0)
            {
                if (dsSIH.Tables[0] != null)
                {
                    // Update Fresh Meters 
                    foreach (DataRow freshRW in dsSIH.Tables[0].Rows)
                    {
                        selectedRows = dtSIHReport.Select("ID=" + freshRW["ID"].ToString());
                        if (selectedRows.Length > 0)
                            selectedRows[0]["Fresh"] = freshRW["TotalMtrs"].ToString();
                    }
                }

                if (dsSIH.Tables[1] != null)
                {
                    DataTable checkedTable = dsSIH.Tables[1];


                    foreach (DataRow sorts in dtSIHReport.Rows)
                    {
                        sorts["Sno"] = ++sno;

                        if (DataFormatter.SafeInt(sorts["Fresh"].ToString()) <= 0)
                            sorts["Fresh"] = "0";

                        // Update Checked Meters by Grade
                        foreach (DataRow grade in UCMConst.FabricQuality.Rows)
                        {
                            selectedRows = dtSIHReport.Select("ID=" + sorts["ID"].ToString());
                            if (selectedRows.Length > 0)
                            {
                                selectedRows[0][grade["ID"].ToString()]
                                    = DataFormatter.SafeInt(checkedTable.Compute("Sum(TotalMtrs)", "ID=" + sorts["ID"].ToString()
                                        + " AND GradeID=" + grade["ID"].ToString()
                                        + " AND PackStatus='Checked'"));
                                selectedRows[0]["Count:" + grade["ID"].ToString()]
                                    = DataFormatter.SafeInt(checkedTable.Compute("Sum(TotalPieces)", "ID=" + sorts["ID"].ToString()
                                        + " AND GradeID=" + grade["ID"].ToString()
                                        + " AND PackStatus='Checked'"));
                            }
                        }

                        // Update Packed Meters by Grade
                        selectedRows = dtSIHReport.Select("ID=" + sorts["ID"].ToString());
                        selectedRows[0]["Packed"]
                            = DataFormatter.SafeInt(checkedTable.Compute("Sum(TotalMtrs)", "ID=" + sorts["ID"].ToString()
                                        + " AND PackStatus='Packed'"));
                    }


                }
            }
            dtSIHReport.AcceptChanges();


            rptSIH.DataSource = dtSIHReport;
            rptSIH.DataBind();
        }
    }
}