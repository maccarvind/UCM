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

            DataTable dtModifiedData = new DataTable();
            DataRow row;

            dtModifiedData.Columns.Add(new DataColumn("Nature / Head"));
            foreach (DataRow heads in dtReportData.DefaultView.ToTable(true, "ExpHeadName").Rows)
            {
                dtModifiedData.Columns.Add(new DataColumn(heads["ExpHeadName"].ToString()));
            }
            dtModifiedData.Columns.Add(new DataColumn("Total"));
            dtModifiedData.AcceptChanges();

            foreach (DataRow nature in dtReportData.DefaultView.ToTable(true, "ExpNatureName").Rows)
            {
                row = dtModifiedData.NewRow();
                row["Nature / Head"] = nature["ExpNatureName"].ToString();
                foreach (DataRow heads in dtReportData.DefaultView.ToTable(true, "ExpHeadName").Rows)
                {
                    row[heads["ExpHeadName"].ToString()] = dtReportData.Compute("SUM(TOTAL)", "ExpHeadName='" + heads["ExpHeadName"].ToString() +
                                        "' AND ExpNatureName='" + nature["ExpNatureName"].ToString() + "'");
                }
                row["Total"] = dtReportData.Compute("SUM(TOTAL)", "ExpNatureName='" + nature["ExpNatureName"].ToString() + "'");

                dtModifiedData.Rows.Add(row);
            }


            row = dtModifiedData.NewRow();
            row["Nature / Head"] = "Total";
            foreach (DataRow heads in dtReportData.DefaultView.ToTable(true, "ExpHeadName").Rows)
            {
                row[heads["ExpHeadName"].ToString()] = dtReportData.Compute("SUM(TOTAL)", "ExpHeadName='" + heads["ExpHeadName"].ToString() + "'");
            }
            row["Total"] = dtReportData.Compute("SUM(TOTAL)", string.Empty);
            dtModifiedData.Rows.Add(row);

            dtModifiedData.AcceptChanges();
            gridPettyCashReport.DataSource = dtModifiedData;
            gridPettyCashReport.DataBind();

           

        }

        protected void gridPettyCashReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 1; i < e.Row.Cells.Count-1; i++)
                {
                    e.Row.Cells[i].CssClass = "fn-pettycash-report-data";
                    e.Row.Cells[i].Text = DataFormatter.SafeDouble(e.Row.Cells[i].Text).ToString("N");
                }
                e.Row.Cells[e.Row.Cells.Count - 1].CssClass = "fn-pettycash-report-total";
                e.Row.Cells[e.Row.Cells.Count - 1].Text = DataFormatter.SafeDouble(e.Row.Cells[e.Row.Cells.Count - 1].Text).ToString("N");

            }

            
        }

        protected void gridPettyCashReport_PreRender(object sender, EventArgs e)
        {
            if (gridPettyCashReport.Rows.Count > 1)
                gridPettyCashReport.Rows[gridPettyCashReport.Rows.Count - 1].Attributes.Add("class", "fn-pettycash-report-total");
        }

    }
}