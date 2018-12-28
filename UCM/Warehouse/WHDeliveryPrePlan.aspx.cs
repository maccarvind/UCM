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
    public partial class WHDeliveryPlan : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                formInitialize();
            }
        }

        private void formInitialize()
        {

            dropDeliveryPlan.Items.Insert(0, new ListItem("- Delivery Plan - ", "0"));

            dropInvoice.Items.Insert(0, new ListItem("- Invoice - ", "0"));

        }

        protected void btnAddEntry_Click(object sender, EventArgs e)
        {
            if ((txtDeliveryPlanNo.Text == string.Empty))
            {
                lblMessage.Text = "Kindly fill all the details.";
                return;
            }

            if (chkApprove.Checked)
            {
                if (lstPackageSelection.GetSelectedIndices().Count() <= 0)
                {
                    lblMessage.Text = "Packages cannot be empty.";
                    return;
                }
            }

            UCMWarehouse whOps = new UCMWarehouse();
            string retValue = string.Empty;

            if (DataFormatter.SafeInt(hidDeliveryPlanID.Value) <= 0)
            {
                whOps.AddUpdateDeliveryPlanDetails("ADD", default(int), txtDeliveryPlanNo.Text.Trim(),
                    txtDeliveryRemarks.Text.Trim(), default(int), ref retValue);

            }
            else
            {
                whOps.AddUpdateDeliveryPlanDetails("UPDATE", DataFormatter.SafeInt(hidDeliveryPlanID.Value), txtDeliveryPlanNo.Text.Trim(),
                    txtDeliveryRemarks.Text.Trim(), (chkApprove.Checked ? 1 : 0), ref retValue);

            }


            updatePackagesWithDeliveryPlanID(DataFormatter.SafeInt(retValue) > 0 ? DataFormatter.SafeInt(retValue) : DataFormatter.SafeInt(hidDeliveryPlanID.Value));

            editDeliveryPlanDetails(DataFormatter.SafeInt(retValue) > 0 ? DataFormatter.SafeInt(retValue) : DataFormatter.SafeInt(hidDeliveryPlanID.Value));

            if (DataFormatter.SafeInt(retValue) > 0)
            {
                lblMessage.Text = "Package Details Created Successfully.";
            }
            else
            {
                lblMessage.Text = "Package Details Update Successfully.";

            }


        }

        private void updatePackagesWithDeliveryPlanID(int deliveryPlanID)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.ClearPackagesByDeliveryPlanID(deliveryPlanID);

            foreach (int i in lstPackageSelection.GetSelectedIndices())
            {
                whOps.UpdatePackagesByDeliveryPlanID(DataFormatter.SafeInt(lstPackageSelection.Items[i].Value.Split(":".ToCharArray())[0]), deliveryPlanID);
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblInvoice.Text = string.Empty;
            txtDeliveryPlanNo.Text = string.Empty;
            txtDeliveryRemarks.Text = string.Empty;
            hidDeliveryPlanID.Value = string.Empty;
            btnAddEntry.Text = "Add Entry";

            divApprove.Visible = false;

            bindApprovedPackages();
        }

        private void bindApprovedPackages()
        {
            lstPackageSelection.ClearSelection();
            lstPackageSelection.Items.Clear();

            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtPieces = whOps.GetApprovedPackages();

            foreach (DataRow rw in dtPieces.Rows)
            {
                lstPackageSelection.Items.Add(new ListItem(DataFormatter.SafeDate(rw["PkDate"]).ToString("dd-MMM-yyyy") + " / " + rw["PackType"].ToString()
                        + " / " + rw["PkDetails"].ToString() + " / " + rw["TotalMeters"].ToString() + " Mtrs (" + rw["NoOfPieces"].ToString() + ")"
                    , rw["ID"].ToString()));

            }

            if (DataFormatter.SafeInt(hidDeliveryPlanID.Value) > 0)
            {
                dtPieces = whOps.GetPackagesByDeliveryPlanID(DataFormatter.SafeInt(hidDeliveryPlanID.Value));
                ListItem lstItem;
                foreach (DataRow rw in dtPieces.Rows)
                {
                    lstItem = new ListItem();
                    lstItem.Value = rw["ID"].ToString();
                    lstItem.Text = DataFormatter.SafeDate(rw["PkDate"]).ToString("dd-MMM-yyyy") + " / " + rw["PackType"].ToString()
                        + " / " + rw["PkDetails"].ToString() + " / " + rw["TotalMeters"].ToString() + " Mtrs (" + rw["NoOfPieces"].ToString() + ")";
                    lstItem.Selected = true;

                    lstPackageSelection.Items.Add(lstItem);

                }
            }

        }

        protected void editDeliveryPlanDetails(int packingID)
        {
            lblMessage.Text = string.Empty;
            lblInvoice.Text = string.Empty;
            txtDeliveryPlanNo.Text = string.Empty;
            txtDeliveryRemarks.Text = string.Empty;
            hidDeliveryPlanID.Value = packingID.ToString() ;
            btnAddEntry.Text = "Update Entry";

            divApprove.Visible = true;

            bindApprovedPackages();

            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtInwardItem = whOps.GetDeliveryPlanDetail(packingID);

            if (dtInwardItem.Rows.Count > 0)
            {

                txtDeliveryPlanNo.Text = dtInwardItem.Rows[0]["DPNo"].ToString();
                txtDeliveryRemarks.Text = dtInwardItem.Rows[0]["Remarks"].ToString();

                lblInvoice.Text = dtInwardItem.Rows[0]["InvNo"].ToString();

                chkApprove.Checked = DataFormatter.SafeInt(dtInwardItem.Rows[0]["Approved"]) > 0 ? true : false;

                btnAddEntry.Enabled = !chkApprove.Checked;
            }
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            bindDeliveryPlanSearch();
        }

        private void bindDeliveryPlanSearch()
        {
            UCMWarehouse whOps = new UCMWarehouse();
            DataTable dtDeliveryPlanItems = whOps.SearchDeliveryPlanDetails(DataFormatter.SafeInt( dropDeliveryPlan.SelectedValue), 
                DataFormatter.SafeInt(dropInvoice.SelectedValue), txtSearchRemarks.Text );

            if (dtDeliveryPlanItems.Rows.Count > 0)
            {
                lblSearchResult.Text = string.Empty;
                gridDeliveryPlan.Visible = true;
                gridDeliveryPlan.DataSource = dtDeliveryPlanItems;
                gridDeliveryPlan.DataBind();
            }
            else
            {
                gridDeliveryPlan.Visible = false;
                lblSearchResult.Text = "Details not found.";
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "SELECT")
                editDeliveryPlanDetails(DataFormatter.SafeInt(lnkButton.CommandArgument));
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;
            if (lnkButton.CommandName == "DELETE")
                deleteDeliveryDetails(DataFormatter.SafeInt(lnkButton.CommandArgument));
        }

        private void deleteDeliveryDetails(int deliveryPlanID)
        {
            UCMWarehouse whOps = new UCMWarehouse();
            whOps.DeleteDeliveryPlanDetail(deliveryPlanID);
            butSearch_Click(new object(), new EventArgs());
        }

    }
}