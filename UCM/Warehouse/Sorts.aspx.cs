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
    public partial class Sorts : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindFabricList();
            }

        }

        private void bindFabricList()
        {
            UCMHelper.UCMWarehouse whOps = new UCMHelper.UCMWarehouse();
            DataTable dtFabricSort = new DataTable();

            dtFabricSort = whOps.GetFabricSort(null);

            gridFabric.DataSource = dtFabricSort;
            gridFabric.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtWarp.Text.Trim() == string.Empty ||
                txtWeft.Text.Trim() == string.Empty ||
                txtReed.Text.Trim() == string.Empty ||
                txtPic.Text.Trim() == string.Empty ||
                txtWidth.Text.Trim() == string.Empty ||
                txtFabricName.Text.Trim() == string.Empty)
            {

                lblMessage.Text = "Kindly key in all the values.";
                return;
            }

            UCMHelper.UCMWarehouse whOps = new UCMHelper.UCMWarehouse();
            string retValue = default(string);

            if (lblID.Text == string.Empty)
            {
                whOps.AddUpdateFabricSort("ADD", null,txtFabricName.Text.Trim(), txtWarp.Text.Trim(), txtWeft.Text.Trim(), txtReed.Text.Trim(), txtPic.Text.Trim(),
                    txtWidth.Text.Trim(), txtGSM.Text.Trim(), ref retValue);

                if (retValue.ToUpper() == "EXIST")
                {
                    lblMessage.Text = "Fabric Sort already Exists.";
                    return;
                }

                lblMessage.Text = "Fabric Sort added successfully.";

            }
            else
            {
                whOps.AddUpdateFabricSort("UPDATE", DataFormatter.SafeInt(lblID.Text), txtFabricName.Text.Trim(), txtWarp.Text.Trim(), txtWeft.Text.Trim(), txtReed.Text.Trim(),
                    txtPic.Text.Trim(), txtWidth.Text.Trim(), txtGSM.Text.Trim(), ref retValue);

                lblMessage.Text = "Fabric Sort updated successfully.";

                btnSubmit.Text = "Add Details";

            }





            txtWarp.Text = txtWeft.Text = txtReed.Text = txtPic.Text = txtWidth.Text
                = txtGSM.Text = lblID.Text = txtFabricName.Text
                = string.Empty;

            bindFabricList();
        }

        protected void gridFabric_SelectedIndexChanged(object sender, EventArgs e)
        {


            UCMHelper.UCMWarehouse whOps = new UCMHelper.UCMWarehouse();

            DataTable dtFabricSort = whOps.GetFabricSort(DataFormatter.SafeInt(gridFabric.SelectedValue));

            if (dtFabricSort.Rows.Count > 0)
            {
                DataRow rw = dtFabricSort.Rows[0];

                lblID.Text = DataFormatter.SafeString(rw["ID"].ToString());
                txtFabricName.Text = DataFormatter.SafeString(rw["Name"].ToString());
                txtWarp.Text = DataFormatter.SafeString(rw["WarpCount"].ToString());
                txtWeft.Text = DataFormatter.SafeString(rw["WeftCount"].ToString());
                txtReed.Text = DataFormatter.SafeString(rw["Reed"].ToString());
                txtPic.Text = DataFormatter.SafeString(rw["Pic"].ToString());
                txtWidth.Text = DataFormatter.SafeString(rw["Width"].ToString());
                txtGSM.Text = DataFormatter.SafeString(rw["GSM"].ToString());

                btnSubmit.Text = "Update Details";

            }
            else
            {
                lblMessage.Text = "Invalid Selection.";
                return;
            }
        }
    }
}