using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCMHelper;

namespace UCM.Finance
{
    public partial class FNPettyCashReport : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initializeForm();
            }

        }

        private void initializeForm()
        {
            UCMFinance finOps = new UCMFinance();

            DataTable dtCompanies;
            dtCompanies = finOps.GetCompanies();

            dropCompany.DataSource = dtCompanies;
            dropCompany.DataValueField = "ID";
            dropCompany.DataTextField = "Company";
            dropCompany.DataBind();
            dropCompany.Items.Insert(0, new ListItem("- Company -", ""));

        }

        protected void butGenerateReport_Click(object sender, EventArgs e)
        {
            UCMFinance finOps = new UCMFinance();

            DataTable dtReportData = finOps.PettyCashGetExpenseReport(DataFormatter.SafeInt(dropCompany.SelectedValue),
                DataFormatter.SafeDate(txtFromDate.Text), DataFormatter.SafeDate(txtToDate.Text));

            DataTable dtOrgReportData = new DataTable();

            if (dtReportData.Rows.Count > 0)
            {

                dtOrgReportData = dtReportData.Clone();
                DataRow dtRow;

                foreach (DataRow cashInRow in dtReportData.Select("CrDr = 'C'", "ExpName"))
                {
                    dtRow = dtOrgReportData.NewRow();
                    rowCopy(dtRow, cashInRow);
                    dtRow["ExpName"] = "Cash In";
                    dtOrgReportData.Rows.Add(dtRow);
                }

                foreach (DataRow headExpRow in dtReportData.Select("ExpType = 'HEAD'", "ExpName"))
                {
                    dtRow = dtOrgReportData.NewRow();
                    rowCopy(dtRow, headExpRow);
                    dtOrgReportData.Rows.Add(dtRow);
                    foreach (DataRow natureExpRow in dtReportData.Select("ExpType = 'NATURE' AND ParentID = " + headExpRow["ID"].ToString(), "ExpName"))
                    {
                        dtRow = dtOrgReportData.NewRow();
                        rowCopy(dtRow, natureExpRow);
                        dtOrgReportData.Rows.Add(dtRow);
                    }
                }

                dtOrgReportData.AcceptChanges(); 
                
                gridPettyCashReport.DataSource = dtOrgReportData;
                gridPettyCashReport.DataBind();
                lblReportMessasge.Text = "Cr. " + DataFormatter.SafeDouble(dtOrgReportData.Compute("SUM(Total)", "CrDr = 'C'")).ToString("F")
                       + ", Dr. " + DataFormatter.SafeDouble(dtOrgReportData.Compute("SUM(Total)", "ExpType = 'HEAD'")).ToString("F");
                gridPettyCashReport.Visible = true;
            }
            else
            {
                lblReportMessasge.Text = "No Data.";
                gridPettyCashReport.Visible = false;
            }




        }

        private void rowCopy(DataRow destRow, DataRow sourceRow)
        {
            for (int i = 0; i < destRow.ItemArray.Count(); ++i)
            {
                destRow[i] = sourceRow[i];
            }
        }
    }
}