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
    public partial class WHPacking : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formInitialize();

                lnkButton_Click(sender, e);
            }
        }

        private void formInitialize()
        {
            dropPackageType.DataSource = UCMConst.PackageType;
            dropPackageType.DataTextField = "EntName";
            dropPackageType.DataValueField = "ID";
            dropPackageType.DataBind();
            dropPackageType.Items.Insert(0, new ListItem("- Package Type -", "0"));

            dropSearchPackageType.DataSource = UCMConst.PackageType;
            dropSearchPackageType.DataTextField = "EntName";
            dropSearchPackageType.DataValueField = "ID";
            dropSearchPackageType.DataBind();
            dropSearchPackageType.Items.Insert(0, new ListItem("- Package Type -", "0"));

            UCMWarehouse whOps = new UCMWarehouse();

            DataTable dtFabricSorts = whOps.GetFabricSort(null);
            dropSearchFabricSort.DataSource = dtFabricSorts;
            dropSearchFabricSort.DataValueField = "ID";
            dropSearchFabricSort.DataTextField = "Name";
            dropSearchFabricSort.DataBind();
            dropSearchFabricSort.Items.Insert(0, new ListItem("- Fabric Sort - ", "0"));

            dropDeliveryPlan.Items.Insert(0, new ListItem("- Delivery Plan - ", "0"));

            dropInvoice.Items.Insert(0, new ListItem("- Invoice - ", "0"));


        }

        protected void btnAddEntry_Click(object sender, EventArgs e)
        {
            if ((txtDate.Text == string.Empty) || (dropPackageType.SelectedValue == "0")
                || (txtPackageDetails.Text.Trim() == string.Empty)
                || (DataFormatter.SafeInt(txtTare.Text) <= 0))
            {
                lblMessage.Text = "Kindly fill all the details.";
                return;
            }

            if (chkApprove.Checked)
            {
                if (lstPieceSelection.GetSelectedIndices().Count() <= 0)
                {
                    lblMessage.Text = "Pieces cannot be empty.";
                    return;
                }
            }

            UCMWarehouse whOps = new UCMWarehouse();
            string retValue = string.Empty;

            if (DataFormatter.SafeInt(txtPackingID.Value) <= 0)
            {
                whOps.AddUpdatePackingDetails("ADD", default(int), txtPackageDetails.Text.Trim(), DataFormatter.SafeDate(txtDate.Text),
                    DataFormatter.SafeInt(dropPackageType.SelectedValue), DataFormatter.SafeInt(txtTare.Text), default(int), ref retValue);

            }
            else
            {
                whOps.AddUpdatePackingDetails("UPDATE", DataFormatter.SafeInt(txtPackingID.Value), txtPackageDetails.Text.Trim(), DataFormatter.SafeDate(txtDate.Text),
                    DataFormatter.SafeInt(dropPackageType.SelectedValue), DataFormatter.SafeInt(txtTare.Text), (chkApprove.Checked ? 1 : 0), ref retValue);

            }


            updatePiecesWithPackageID(DataFormatter.SafeInt(retValue) > 0 ? DataFormatter.SafeInt(retValue) : DataFormatter.SafeInt(txtPackingID.Value));

            editPackingDetails(DataFormatter.SafeInt(retValue) > 0 ? DataFormatter.SafeInt(retValue) : DataFormatter.SafeInt(txtPackingID.Value));

            if (DataFormatter.SafeInt(retValue) > 0)
            {
                lblMessage.Text = "Package Details Created Successfully.";
            }
            else
            {
                lblMessage.Text = "Package Details Update Successfully.";

            }


        }

        private void updatePiecesWithPackageID(int packageID)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.ClearPieceByPackageID(packageID);

            foreach (int i in lstPieceSelection.GetSelectedIndices())
            {
                whOps.UpdatePieceByPackageID(DataFormatter.SafeInt(lstPieceSelection.Items[i].Value.Split(":".ToCharArray())[0]), packageID);
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblInvoice.Text = string.Empty;
            txtPackageDetails.Text = string.Empty;
            txtPackingID.Value = string.Empty;
            btnAddEntry.Text = "Add Entry";

            txtDate.Text = string.Empty;
            dropPackageType.ClearSelection();
            txtTare.Text = string.Empty;

            divApprove.Visible = false;

            bindApprovedPieces();
        }

        private void bindApprovedPieces()
        {
            lstPieceSelection.ClearSelection();
            lstPieceSelection.Items.Clear();

            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtPieces = whOps.GetApprovedPieces();

            foreach (DataRow rw in dtPieces.Rows)
            {
                lstPieceSelection.Items.Add(new ListItem(rw["Name"].ToString() + " / " + rw["Grade"].ToString()
                        + " / " + rw["PieceMark"].ToString() + " / " + rw["ActualLength"].ToString()
                    , rw["ID"].ToString() + ":" + rw["ActualLength"].ToString()));

            }

            if (DataFormatter.SafeInt(txtPackingID.Value) > 0)
            {
                dtPieces = whOps.GetPieceByPackageID(DataFormatter.SafeInt(txtPackingID.Value));
                ListItem lstItem;
                foreach (DataRow rw in dtPieces.Rows)
                {
                    lstItem = new ListItem();
                    lstItem.Value = rw["ID"].ToString() + ":" + rw["ActualLength"].ToString();
                    lstItem.Text = rw["Name"].ToString() + " / " + rw["Grade"].ToString()
                       + " / " + rw["PieceMark"].ToString() + " / " + rw["ActualLength"].ToString();
                    lstItem.Selected = true;

                    lstPieceSelection.Items.Add(lstItem);

                }
            }

        }

        protected void editPackingDetails(int packingID)
        {
            lblMessage.Text = string.Empty;
            lblInvoice.Text = string.Empty;
            txtPackageDetails.Text = string.Empty;
            txtPackingID.Value = packingID.ToString();
            btnAddEntry.Text = "Update Entry";

            txtDate.Text = string.Empty;
            dropPackageType.ClearSelection();
            txtTare.Text = string.Empty;

            divApprove.Visible = true;

            bindApprovedPieces();

            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtInwardItem = whOps.GetPackingDetail(packingID);

            if (dtInwardItem.Rows.Count > 0)
            {

                txtDate.Text = (DataFormatter.SafeDate(dtInwardItem.Rows[0]["PkDate"]) == DataFormatter.SafeDate(string.Empty))
                    ? string.Empty : DataFormatter.SafeDate(dtInwardItem.Rows[0]["PkDate"]).ToString("dd-MMM-yyyy");
                txtPackageDetails.Text = dtInwardItem.Rows[0]["PkDetails"].ToString();
                txtTare.Text = dtInwardItem.Rows[0]["Tare"].ToString();

                lblInvoice.Text = dtInwardItem.Rows[0]["DPNo"].ToString()
                    + (dtInwardItem.Rows[0]["InvNo"].ToString() == string.Empty ? string.Empty : " / " + dtInwardItem.Rows[0]["InvNo"].ToString());

                chkApprove.Checked = DataFormatter.SafeInt(dtInwardItem.Rows[0]["Approved"]) > 0 ? true : false;

                if (dropPackageType.Items.FindByValue(dtInwardItem.Rows[0]["TypeID"].ToString()) != null)
                    dropPackageType.Items.FindByValue(dtInwardItem.Rows[0]["TypeID"].ToString()).Selected = true;

                btnAddEntry.Enabled = !chkApprove.Checked;
            }
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            bindPackageSearch();
        }

        private void bindPackageSearch()
        {
            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtPackingItems = whOps.SearchPackingDetails(DataFormatter.SafeDate(txtFromDate.Text), DataFormatter.SafeDate(txtToDate.Text),
                DataFormatter.SafeInt(dropSearchPackageType.SelectedValue), txtSearchDetails.Text.Trim(), DataFormatter.SafeInt(dropSearchFabricSort.SelectedValue),
                DataFormatter.SafeInt(dropDeliveryPlan.SelectedValue), DataFormatter.SafeInt(dropInvoice.SelectedValue));

            if (dtPackingItems.Rows.Count > 0)
            {
                lblSearchResult.Text = string.Empty;
                gridPackage.Visible = true;
                gridPackage.DataSource = dtPackingItems;
                gridPackage.DataBind();
            }
            else
            {
                gridPackage.Visible = false;
                lblSearchResult.Text = "Details not found.";
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "SELECT")
                editPackingDetails(DataFormatter.SafeInt(lnkButton.CommandArgument));
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "DELETE")
                deletePackingDetails(DataFormatter.SafeInt(lnkButton.CommandArgument));
        }

        private void deletePackingDetails(int packingID)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.DeletePackingDetail(packingID);
            butSearch_Click(new object(), new EventArgs());
        }
    }
}