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
    public partial class FNPettyCashEntry : UCMLoggedInPage
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

            dropCashInCompany.DataSource = dtCompanies;
            dropCashInCompany.DataValueField = "ID";
            dropCashInCompany.DataTextField = "Company";
            dropCashInCompany.DataBind();
            dropCashInCompany.Items.Insert(0, new ListItem("- Company -", ""));

            dropCashOutCompany.DataSource = dtCompanies;
            dropCashOutCompany.DataValueField = "ID";
            dropCashOutCompany.DataTextField = "Company";
            dropCashOutCompany.DataBind();
            dropCashOutCompany.Items.Insert(0, new ListItem("- Company -", ""));

            dropHeadOfExp.DataSource = finOps.GetExpenseMasterByType("HEAD");
            dropHeadOfExp.DataValueField = "ID";
            dropHeadOfExp.DataTextField = "ExpName";
            dropHeadOfExp.DataBind();
            dropHeadOfExp.Items.Insert(0, new ListItem("- Exp. Heads -", ""));

            dropNatureOfExp.DataSource = finOps.GetExpenseMasterByType("NATURE"); ;
            dropNatureOfExp.DataValueField = "ID";
            dropNatureOfExp.DataTextField = "ExpName";
            dropNatureOfExp.DataBind();
            dropNatureOfExp.Items.Insert(0, new ListItem("- Exp. Nature -", ""));

        }

        protected void lnkGetDate_Click(object sender, EventArgs e)
        {
            bindPettyCashEntries();
        }

        private void bindPettyCashEntries()
        {
            if (txtTranDate.Text != string.Empty)
            {

                lblDate.Text = txtTranDate.Text;

                UCMFinance finOps = new UCMFinance();

                DataTable dtPettyCashEntries = finOps.PettyCashGetByTranDate(DataFormatter.SafeDate(txtTranDate.Text));

                if (dtPettyCashEntries.Rows.Count > 0)
                {
                    lblEntryMessasge.Text = string.Empty;
                    gridPettyCashEntries.DataSource = dtPettyCashEntries;
                    gridPettyCashEntries.DataBind();
                    gridPettyCashEntries.Visible = true;

                    lblEntryMessasge.Text = "Cr. " + DataFormatter.SafeDouble( dtPettyCashEntries.Compute("SUM(Amount)", "CrDr = 'C'")).ToString("F")  
                        + ", Dr. " + DataFormatter.SafeDouble( dtPettyCashEntries.Compute("SUM(Amount)", "CrDr = 'D'")).ToString("F")  ;
                }
                else
                {
                    lblEntryMessasge.Text = "No Entries.";

                    gridPettyCashEntries.Visible = false;
                }
            }
            else
            {
                lblDate.Text = "Kindly Select a Date";
            }
        }

        protected void butCashIn_Click(object sender, EventArgs e)
        {
            if ((dropCashInCompany.SelectedValue == string.Empty) || (DataFormatter.SafeDouble(txtCashInAmount.Text) < 0))
            {
                lblCashInMessage.Text = "Kindly input all the fields.";
                return;
            }

            UCMFinance finOps = new UCMFinance();

            if (DataFormatter.SafeInt(hidEntryID.Value) <= 0)
            {
                finOps.PettyCashAddUpdate("ADD", default(int), DataFormatter.SafeDate(lblDate.Text), "C", DataFormatter.SafeInt(dropCashInCompany.SelectedValue),
                    default(int), default(int), string.Empty, DataFormatter.SafeDate(string.Empty), string.Empty,
                    txtCashInRemarks.Text.Trim(), DataFormatter.SafeDouble(txtCashInAmount.Text));

                lblCashInMessage.Text = "Cash In Entry Added Successfully.";
            }
            else
            {
                finOps.PettyCashAddUpdate("UPDATE", DataFormatter.SafeInt(hidEntryID.Value), DataFormatter.SafeDate(lblDate.Text), "C", DataFormatter.SafeInt(dropCashInCompany.SelectedValue),
                    default(int), default(int), string.Empty, DataFormatter.SafeDate(string.Empty), string.Empty,
                    txtCashInRemarks.Text.Trim(), DataFormatter.SafeDouble(txtCashInAmount.Text));

                lblCashInMessage.Text = "Cash In Entry Updated Successfully.";

            }

            clearForm();
            butCashIn.Text = "Add Entry";

            bindPettyCashEntries();
        }

        protected void butCashOut_Click(object sender, EventArgs e)
        {
            if ((dropCashOutCompany.SelectedValue == string.Empty) ||
                (dropHeadOfExp.SelectedValue == string.Empty) ||
                (dropNatureOfExp.SelectedValue == string.Empty) ||
                (
                    (txtVendor.Text.Trim() == string.Empty) &&
                    (txtBillNo.Text.Trim() == string.Empty) &&
                    (txtCashOutRemarks.Text.Trim() == string.Empty)
                ) ||
                (DataFormatter.SafeDouble(txtCashInAmount.Text) < 0))
            {
                lblCashOutMessage.Text = "Kindly input all the fields.";
                return;
            }

            UCMFinance finOps = new UCMFinance();

            if (DataFormatter.SafeInt(hidEntryID.Value) <= 0)
            {
                finOps.PettyCashAddUpdate("ADD", default(int), DataFormatter.SafeDate(lblDate.Text), "D", DataFormatter.SafeInt(dropCashOutCompany.SelectedValue),
                    DataFormatter.SafeInt(dropHeadOfExp.SelectedValue), DataFormatter.SafeInt(dropNatureOfExp.SelectedValue),
                    txtVendor.Text.Trim(), DataFormatter.SafeDate(txtBillDate.Text), txtBillNo.Text.Trim(),
                    txtCashOutRemarks.Text.Trim(), DataFormatter.SafeDouble(txtCashOutAmount.Text));

                lblCashOutMessage.Text = "Cash Out Entry Added Successfully.";
            }
            else
            {
                finOps.PettyCashAddUpdate("UPDATE", DataFormatter.SafeInt(hidEntryID.Value), DataFormatter.SafeDate(lblDate.Text), "D", DataFormatter.SafeInt(dropCashOutCompany.SelectedValue),
                   DataFormatter.SafeInt(dropHeadOfExp.SelectedValue), DataFormatter.SafeInt(dropNatureOfExp.SelectedValue),
                   txtVendor.Text.Trim(), DataFormatter.SafeDate(txtBillDate.Text), txtBillNo.Text.Trim(),
                   txtCashOutRemarks.Text.Trim(), DataFormatter.SafeDouble(txtCashOutAmount.Text));

                lblCashOutMessage.Text = "Cash Out Entry Updated Successfully.";
            }
           
            clearForm();
            butCashIn.Text = "Add Entry";

            bindPettyCashEntries();
        }

        private void clearForm()
        {
            hidEntryID.Value = string.Empty;


            dropCashInCompany.ClearSelection();
            dropCashOutCompany.ClearSelection();
            dropHeadOfExp.ClearSelection();
            dropNatureOfExp.ClearSelection();

            txtCashInRemarks.Text = txtCashInAmount.Text = txtBillDate.Text = txtBillNo.Text
                = txtVendor.Text = txtCashOutRemarks.Text = txtCashOutAmount.Text
                = string.Empty;
        }

        private void fillPettyCashEntryDetails(int id)
        {

            clearForm();

            lblCashInMessage.Text = lblCashOutMessage.Text = string.Empty;

            hidEntryID.Value = id.ToString();

            UCMFinance finOps = new UCMFinance();
            DataTable dtPettyCashEntry = finOps.PettyCashGetByID(id);

            if (dtPettyCashEntry.Rows.Count > 0)
            {
                DataRow pCashRw = dtPettyCashEntry.Rows[0];

                butCashIn.Enabled = butCashOut.Enabled =
                    pCashRw["Approved"].ToString() == "1" ? false : true;

                if (pCashRw["CrDr"].ToString().ToUpper() == "C")
                {
                    if (dropCashInCompany.Items.FindByValue(pCashRw["CompanyID"].ToString()) != null)
                        dropCashInCompany.Items.FindByValue(pCashRw["CompanyID"].ToString()).Selected = true;

                    txtCashInRemarks.Text = pCashRw["Remarks"].ToString();
                    txtCashInAmount.Text = DataFormatter.SafeDouble(pCashRw["Amount"].ToString()).ToString("F");

                    butCashIn.Text = "Update Entry";

                }
                else
                {
                    if (dropCashOutCompany.Items.FindByValue(pCashRw["CompanyID"].ToString()) != null)
                        dropCashOutCompany.Items.FindByValue(pCashRw["CompanyID"].ToString()).Selected = true;

                    if (dropHeadOfExp.Items.FindByValue(pCashRw["ExpHeadID"].ToString()) != null)
                        dropHeadOfExp.Items.FindByValue(pCashRw["ExpHeadID"].ToString()).Selected = true;

                    if (dropNatureOfExp.Items.FindByValue(pCashRw["ExpNatureID"].ToString()) != null)
                        dropNatureOfExp.Items.FindByValue(pCashRw["ExpNatureID"].ToString()).Selected = true;

                    if (DataFormatter.SafeDate(pCashRw["BillDate"].ToString()) != DataFormatter.SafeDate("1/1/1900"))
                        txtBillDate.Text = DataFormatter.SafeDate(pCashRw["BillDate"].ToString()).ToString("dd-MMM-yyyy");
                    txtBillNo.Text = pCashRw["BillNo"].ToString();
                    txtVendor.Text = pCashRw["Vendor"].ToString();
                    txtCashOutRemarks.Text = pCashRw["Remarks"].ToString();
                    txtCashOutAmount.Text = DataFormatter.SafeDouble(pCashRw["Amount"].ToString()).ToString("F");

                    butCashOut.Text = "Update Entry";

                }
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;


            if (lnkButton.CommandName == "SELECT")
                fillPettyCashEntryDetails(DataFormatter.SafeInt(lnkButton.CommandArgument));
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "DELETE")
            {
                UCMFinance finOps = new UCMFinance();
                finOps.PettyCashDeleteByID(DataFormatter.SafeInt(lnkButton.CommandArgument));
                
                clearForm();
                bindPettyCashEntries();
            }
        }
    }
}