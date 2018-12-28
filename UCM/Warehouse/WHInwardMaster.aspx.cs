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
    public partial class WHInwardMaster : UCMLoggedInPage
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
            dropOrigin.DataSource = UCMConst.InwardOrigin;
            dropOrigin.DataValueField = "ID";
            dropOrigin.DataTextField = "EntName";
            dropOrigin.DataBind();
            dropOrigin.Items.Insert(0, new ListItem("- Origin - ", "0"));

            dropSearchOrigin.DataSource = UCMConst.InwardOrigin;
            dropSearchOrigin.DataValueField = "ID";
            dropSearchOrigin.DataTextField = "EntName";
            dropSearchOrigin.DataBind();
            dropSearchOrigin.Items.Insert(0, new ListItem("- Origin - ", "0"));

            UCMWarehouse whOps = new UCMWarehouse();

            DataTable dtFabricSorts = whOps.GetFabricSort(null);
            dropFabricSort.DataSource = dtFabricSorts;
            dropFabricSort.DataValueField = "ID";
            dropFabricSort.DataTextField = "Name";
            dropFabricSort.DataBind();
            dropFabricSort.Items.Insert(0, new ListItem("- Fabric Sort - ", "0"));

            dropSearchFabricSort.DataSource = dtFabricSorts;
            dropSearchFabricSort.DataValueField = "ID";
            dropSearchFabricSort.DataTextField = "Name";
            dropSearchFabricSort.DataBind();
            dropSearchFabricSort.Items.Insert(0, new ListItem("- Fabric Sort - ", "0"));

            DataTable dtVendors = whOps.GetVendor(null);
            dropVendor.DataSource = dtVendors;
            dropVendor.DataValueField = "ID";
            dropVendor.DataTextField = "VenName";
            dropVendor.DataBind();
            dropVendor.Items.Insert(0, new ListItem("- Vendor - ", "0"));

            dropSearchVendor.DataSource = dtVendors;
            dropSearchVendor.DataValueField = "ID";
            dropSearchVendor.DataTextField = "VenName";
            dropSearchVendor.DataBind();
            dropSearchVendor.Items.Insert(0, new ListItem("- Vendor - ", "0"));
        }


        protected void butSearch_Click(object sender, EventArgs e)
        {
            UCMWarehouse whOps = new UCMWarehouse();

            DataTable dtInwardDetails = whOps.SearchInwardMaster(txtSearchRef.Text.Trim(), DataFormatter.SafeDate(txtFromDate.Text),
                DataFormatter.SafeDate(txtToDate.Text), DataFormatter.SafeInt(dropSearchVendor.SelectedValue),
                DataFormatter.SafeInt(dropSearchFabricSort.SelectedValue), DataFormatter.SafeInt(dropSearchOrigin.SelectedValue));

            gridInward.DataSource = dtInwardDetails;
            gridInward.DataBind();

        }

        protected void gridInward_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMWarehouse whOps = new UCMWarehouse();

            DataTable dtInwardDetails = whOps.GetInwardMaster(DataFormatter.SafeInt(gridInward.SelectedValue));

            if (dtInwardDetails.Rows.Count > 0)
            {
                txtID.Value = gridInward.SelectedValue.ToString();
                txtDate.Text = DataFormatter.SafeDate(dtInwardDetails.Rows[0]["InwdDate"]).ToString("dd-MMM-yyyy");

                dropOrigin.ClearSelection();
                if (dropOrigin.Items.FindByValue(dtInwardDetails.Rows[0]["Origin"].ToString()) != null)
                    dropOrigin.Items.FindByValue(dtInwardDetails.Rows[0]["Origin"].ToString()).Selected = true;

                dropFabricSort.ClearSelection();
                if (dropFabricSort.Items.FindByValue(dtInwardDetails.Rows[0]["SortID"].ToString()) != null)
                    dropFabricSort.Items.FindByValue(dtInwardDetails.Rows[0]["SortID"].ToString()).Selected = true;

                dropVendor.ClearSelection();
                if (dropVendor.Items.FindByValue(dtInwardDetails.Rows[0]["VendorID"].ToString()) != null)
                    dropVendor.Items.FindByValue(dtInwardDetails.Rows[0]["VendorID"].ToString()).Selected = true;

                txtReference.Text = dtInwardDetails.Rows[0]["InwdRef"].ToString();
                txtNoOfItems.Text = dtInwardDetails.Rows[0]["NoOfItems"].ToString();
                txtNoOfItems.Enabled = false;

                txtApproxMts.Text = dtInwardDetails.Rows[0]["ApproxMtrs"].ToString();
                chkApprove.Checked = DataFormatter.SafeInt(dtInwardDetails.Rows[0]["Approved"].ToString()) == 0 ? false : true;

                lblMessage.Text = string.Empty;

                btnAddEntry.Text = "Update Entry";
                btnAddEntry.Enabled = DataFormatter.SafeInt(dtInwardDetails.Rows[0]["Approved"].ToString()) == 0 ? true : false;
                divapprove.Visible = true;

            }

        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            txtID.Value = string.Empty;
            btnAddEntry.Text = "Add Entry";

            txtDate.Text = string.Empty;
            dropOrigin.ClearSelection();
            dropFabricSort.ClearSelection();
            dropVendor.ClearSelection();
            txtReference.Text = string.Empty;
            txtNoOfItems.Text = string.Empty;
            txtNoOfItems.Enabled = true;
            txtApproxMts.Text = string.Empty;
            
            divapprove.Visible = false;
            btnAddEntry.Visible = true;
        }

        protected void btnAddEntry_Click(object sender, EventArgs e)
        {
            if ((txtDate.Text == string.Empty) || (dropOrigin.SelectedValue == "0") ||
                (dropFabricSort.SelectedValue == "0") ||
                ((DataFormatter.SafeInt(txtNoOfItems.Text) <= 0) && (txtID.Value.Trim() == string.Empty))
                )
            {
                lblMessage.Text = "Kindly fill all the details including No. of Items";
                return;
            }
            UCMWarehouse whOps = new UCMWarehouse();
            string returnVal = string.Empty;

            if (txtID.Value == string.Empty)
            {

                whOps.AddUpdateInwardMaster("ADD", null, txtReference.Text.Trim(), DataFormatter.SafeDate(txtDate.Text),
                    DataFormatter.SafeInt(dropVendor.SelectedValue), DataFormatter.SafeInt(dropFabricSort.SelectedValue),
                    DataFormatter.SafeInt(dropOrigin.SelectedValue), DataFormatter.SafeInt(txtApproxMts.Text),
                    DataFormatter.SafeInt(txtNoOfItems.Text), 0, ref returnVal);

                lblMessage.Text = "Inward Details Created Successfully.";
            }
            else
            {
                whOps.AddUpdateInwardMaster("UPDATE", DataFormatter.SafeInt(txtID.Value), txtReference.Text.Trim(), DataFormatter.SafeDate(txtDate.Text),
                     DataFormatter.SafeInt(dropVendor.SelectedValue), DataFormatter.SafeInt(dropFabricSort.SelectedValue),
                     DataFormatter.SafeInt(dropOrigin.SelectedValue), DataFormatter.SafeInt(txtApproxMts.Text),
                     DataFormatter.SafeInt(txtNoOfItems.Text), (chkApprove.Checked ? 0 : 1), ref returnVal);

                lblMessage.Text = "Inward Details Updated Successfully.";

                txtID.Value = string.Empty;
                btnAddEntry.Text = "Add Entry";
            }


            txtDate.Text = string.Empty;
            dropOrigin.ClearSelection();
            dropFabricSort.ClearSelection();
            dropVendor.ClearSelection();
            txtReference.Text = string.Empty;
            txtNoOfItems.Text = string.Empty;
            txtApproxMts.Text = string.Empty;
        }


    }
}