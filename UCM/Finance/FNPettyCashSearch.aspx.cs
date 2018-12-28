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
    public partial class FNPettyCashSearch : UCMLoggedInPage
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

            dropHeadOfExp.DataSource = finOps.GetExpenseHeads();
            dropHeadOfExp.DataValueField = "ID";
            dropHeadOfExp.DataTextField = "ExpName";
            dropHeadOfExp.DataBind();
            dropHeadOfExp.Items.Insert(0, new ListItem("- Heads -", ""));

            dropHeadOfExp_SelectedIndexChanged(new object(), new EventArgs());
            butApprove.Visible = false;

        }

        protected void dropHeadOfExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMFinance finOps = new UCMFinance();

            dropNatureOfExp.Items.Clear();

            if (dropHeadOfExp.SelectedValue != string.Empty)
            {
                DataTable dtExpNature = finOps.GetExpenseNature(DataFormatter.SafeInt(dropHeadOfExp.SelectedValue));
                dropNatureOfExp.DataSource = dtExpNature;
                dropNatureOfExp.DataValueField = "ID";
                dropNatureOfExp.DataTextField = "ExpName";
                dropNatureOfExp.DataBind();
            }

            dropNatureOfExp.Items.Insert(0, new ListItem("- Nature -", ""));
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            bindPettyCashEntries();
        }

        private void bindPettyCashEntries()
        {
            UCMFinance finOps = new UCMFinance();

            hidSelected.Value = string.Empty;

            DataTable dtPettyCashEntries = finOps.PettyCashSearch(DataFormatter.SafeDate(txtFromDate.Text), DataFormatter.SafeDate(txtToDate), dropCrDr.SelectedValue,
                DataFormatter.SafeInt(dropCompany.SelectedValue), DataFormatter.SafeInt(dropHeadOfExp.SelectedValue), DataFormatter.SafeInt(dropNatureOfExp.SelectedValue),
                txtRemarks.Text.Trim(), DataFormatter.SafeDouble(txtAmount.Text));

            if (dtPettyCashEntries.Rows.Count > 0)
            {
                gridPettyCashEntries.DataSource = dtPettyCashEntries;
                gridPettyCashEntries.DataBind();
                gridPettyCashEntries.Visible = true;
                butApprove.Visible = true;
                lblEntryMessasge.Text = "Cr. " + DataFormatter.SafeDouble(dtPettyCashEntries.Compute("SUM(Amount)", "CrDr = 'C'")).ToString("F")
                        + ", Dr. " + DataFormatter.SafeDouble(dtPettyCashEntries.Compute("SUM(Amount)", "CrDr = 'D'")).ToString("F");

            }
            else
            {
                gridPettyCashEntries.Visible = false;
                butApprove.Visible = false;
                lblEntryMessasge.Text = "No Entries";

            }
        }

        protected void butApprove_Click(object sender, EventArgs e)
        {
            string[] selectedEntries = (hidSelected.Value + ",").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();

            UCMFinance finOps = new UCMFinance();

            foreach (string selectedID in selectedEntries)
            {
                finOps.PettyCashApproveByID(DataFormatter.SafeInt(selectedID));
            }

            bindPettyCashEntries();
        }
    }
}