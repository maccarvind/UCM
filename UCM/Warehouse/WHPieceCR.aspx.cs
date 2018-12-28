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
    public partial class WHPieceCR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formInitialize();

                if (DataFormatter.SafeInt(Request.QueryString.Get("ID")) > 0)
                {
                    if (dropInwardMaster.Items.FindByValue(Request.QueryString.Get("ID")) != null)
                        dropInwardMaster.Items.FindByValue(Request.QueryString.Get("ID")).Selected = true;

                    btnGetDetails_Click(sender, e);
                }
            }
        }

        private void formInitialize()
        {
            UCMWarehouse whOps = new UCMWarehouse();

            DataTable dtInwardMaster = whOps.SearchInwardMaster(string.Empty, DataFormatter.SafeDate(string.Empty), DataFormatter.SafeDate(string.Empty),
                default(int), default(int), default(int));

            dropInwardMaster.Items.Clear();

            if (dtInwardMaster.Rows.Count > 0)
            {
                foreach (DataRow rw in dtInwardMaster.Rows)
                {
                    dropInwardMaster.Items.Add(
                        new ListItem(DataFormatter.SafeDate(rw["InwdDate"]).ToString("dd-MMM-yyyy") + " / " +
                           rw["InwdRef"].ToString() + " / " + rw["Name"].ToString() + " / " + rw["VenName"].ToString(),
                           rw["ID"].ToString()));
                }
            }
            dropInwardMaster.Items.Insert(0, new ListItem(" - Select Inward Entry -", "0"));

            UCMCommonOperations commonOps = new UCMCommonOperations();

            dropGrades.DataSource = commonOps.GetFabricGrade();
            dropGrades.DataValueField = "ID";
            dropGrades.DataTextField = "EntName";
            dropGrades.DataBind();
            dropGrades.Items.Insert(0, new ListItem(" - Grades -", "0"));

            dropDefect.DataSource = commonOps.GetFabricDefects();
            dropDefect.DataValueField = "ID";
            dropDefect.DataTextField = "EntName";
            dropDefect.DataBind();
            dropDefect.Items.Insert(0, new ListItem(" - Defects -", "0"));

            lnkAddButton.Visible = false;
        }


        private void bindInwardItems()
        {
            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtItems = whOps.GetInwardItems(DataFormatter.SafeInt(dropInwardMaster.SelectedValue));

            if (dtItems.Rows.Count > 0)
            {
                lblInwardMasterDetails.Text = dropInwardMaster.SelectedItem.Text;
                lnkAddButton.Visible = DataFormatter.SafeInt(dtItems.Rows[0]["ItemMasterApproved"].ToString()) > 0 ? false : true;

                gridInwardItems.Visible = true;
                gridInwardItems.DataSource = dtItems;
                gridInwardItems.DataBind();
            }
            else
            {
                lnkAddButton.Visible = DataFormatter.SafeInt(dropInwardMaster.SelectedValue) > 0 ? true : false;
                lblInwardMasterDetails.Text = dropInwardMaster.SelectedItem.Text + " : Records Not Found.";
                gridInwardItems.Visible = false;
            }

        }

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            bindInwardItems();
        }

        protected void lnkAddButton_Click(object sender, EventArgs e)
        {
            UCMWarehouse whOps = new UCMWarehouse();

            whOps.AddInwardItem(DataFormatter.SafeInt(dropInwardMaster.SelectedValue));

            bindInwardItems();
        }


        protected void deleteInwardItem(int inwardItemID)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.DeleteInwardItem(inwardItemID);
            btnGetDetails_Click(new object(), new EventArgs());
        }

        protected void editInwardItem(int inwardItemID)
        {

            txtInwardItemID.Value = inwardItemID.ToString();

            txtCheckingDate.Text = string.Empty;
            txtDetails.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtCRMtrs.Text = string.Empty;
            dropDefect.ClearSelection();
            txtLength.Text = string.Empty;
            txtPieceMark.Text = string.Empty;
            dropGrades.ClearSelection();
            lblMessage.Text = string.Empty;


            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtInwardItem = whOps.GetInwardItem(inwardItemID);

            if (dtInwardItem.Rows.Count > 0)
            {
                txtCheckingDate.Text = (DataFormatter.SafeDate(dtInwardItem.Rows[0]["CheckingDate"]) == DataFormatter.SafeDate(string.Empty))
                    ? string.Empty : DataFormatter.SafeDate(dtInwardItem.Rows[0]["CheckingDate"]).ToString("dd-MMM-yyyy");
                txtDetails.Text = dtInwardItem.Rows[0]["Details"].ToString();
                txtRemarks.Text = dtInwardItem.Rows[0]["Remarks"].ToString();
                chkApprove.Checked = DataFormatter.SafeInt(dtInwardItem.Rows[0]["Approved"]) > 0 ? true : false;

                lnkAddUpdateCR.Visible = chkApprove.Checked ? false : true;
                lnkAddUpdatePieceMtrs.Visible = chkApprove.Checked ? false : true;

                bindCheckingReport();

                bindPieces();

                btnUpdateEntry.Enabled = chkApprove.Checked ? false : true;
            }

        }

        protected void btnUpdateEntry_Click(object sender, EventArgs e)
        {
            if (txtCheckingDate.Text == string.Empty)
            {
                lblMessage.Text = "Checking Date in not valid.";
                return;
            }

            UCMWarehouse whOps = new UCMWarehouse();
            whOps.UpdateInwardItem(DataFormatter.SafeInt(txtInwardItemID.Value), DataFormatter.SafeDate(txtCheckingDate.Text),
                txtDetails.Text.Trim(), txtRemarks.Text.Trim(), (chkApprove.Checked ? 1 : 0));

            if (chkApprove.Checked)
            {
                lnkAddUpdateCR.Visible = false;
                lnkAddUpdatePieceMtrs.Visible = false;

                bindCheckingReport();

                bindPieces();

                btnUpdateEntry.Enabled = false;

            }
            lblMessage.Text = "Inward Item Details Updated.";
        }

        protected void lnkAddUpdateCR_Click(object sender, EventArgs e)
        {
            if ((txtCRMtrs.Text.Trim() == string.Empty) || (DataFormatter.SafeInt(dropDefect.SelectedValue) <= 0))
                return;

            string[] indvMtrs = (txtCRMtrs.Text + ",").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToArray();

            UCMWarehouse whOps = new UCMWarehouse();

            foreach (string indvMtr in indvMtrs)
            {
                if (DataFormatter.SafeInt(indvMtr) > 0)
                {
                    whOps.AddCheckingReport(DataFormatter.SafeInt(txtInwardItemID.Value),
                        DataFormatter.SafeInt(indvMtr), DataFormatter.SafeInt(dropDefect.SelectedValue));
                }
            }


            bindCheckingReport();
        }

        protected void gridCheckingReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.DeleteCheckingReport(DataFormatter.SafeInt(gridCheckingReport.SelectedDataKey.Value));

            bindCheckingReport();

        }

        private void bindCheckingReport()
        {
            UCMWarehouse whOps = new UCMWarehouse();
            gridCheckingReport.DataSource = whOps.GetCheckingReport(DataFormatter.SafeInt(txtInwardItemID.Value));
            gridCheckingReport.Columns[gridCheckingReport.Columns.Count - 1].Visible = !chkApprove.Checked;
            gridCheckingReport.DataBind();
        }

        protected void gridPieces_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.DeletePiece(DataFormatter.SafeInt(gridPieces.SelectedDataKey.Value));

            bindPieces();

        }

        protected void lnkAddUpdatePieceMtrs_Click(object sender, EventArgs e)
        {
            if ((DataFormatter.SafeInt(txtLength.Text) <= 0) ||
                (DataFormatter.SafeInt(dropGrades.SelectedValue) <= 0))
                return;

            UCMWarehouse whOps = new UCMWarehouse();
            whOps.AddPiece(DataFormatter.SafeInt(txtInwardItemID.Value), txtPieceMark.Text.Trim(),
                DataFormatter.SafeInt(dropGrades.SelectedValue), DataFormatter.SafeInt(txtLength.Text));


            bindPieces();
        }

        private void bindPieces()
        {
            UCMWarehouse whOps = new UCMWarehouse();
            gridPieces.DataSource = whOps.GetPiece(DataFormatter.SafeInt(txtInwardItemID.Value));
            gridPieces.Columns[gridPieces.Columns.Count - 1].Visible = !chkApprove.Checked;
            gridPieces.DataBind();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "DELETE")
                deleteInwardItem(DataFormatter.SafeInt(lnkButton.CommandArgument));

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "SELECT")
                editInwardItem(DataFormatter.SafeInt(lnkButton.CommandArgument));

        }




    }
}