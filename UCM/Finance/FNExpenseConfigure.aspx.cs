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
    public partial class FNExpenseConfigure : UCMLoggedInPage
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
            //UCMFinance finOps = new UCMFinance();

            //dropHeadExpense.DataSource = finOps.GetExpenseHeads();
            //dropHeadExpense.DataValueField = "ID";
            //dropHeadExpense.DataTextField = "ExpName";
            //dropHeadExpense.DataBind();

            dropExpType.Items.Add(new ListItem("HEAD", "HEAD"));
            dropExpType.Items.Add(new ListItem("NATURE", "NATURE"));
            dropExpType.Items.Insert(0, new ListItem("- Select -", ""));
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;


            if (lnkButton.CommandName == "SELECT")
            {
                UCMFinance finOps = new UCMFinance();

                DataTable dtExpNature = finOps.GetExpenseMasterByID(DataFormatter.SafeInt(lnkButton.CommandArgument));

                txtExpMasterName.Text = dtExpNature.Rows[0]["ExpName"].ToString();
                hidExpID.Value = dtExpNature.Rows[0]["ID"].ToString();
                lblMessage.Text = string.Empty;

                butAddExpenseMaster.Text = "Update Exp. Detail";

            }

        }

        protected void butAddExpenseMaster_Click(object sender, EventArgs e)
        {
            if ((txtExpMasterName.Text.Trim() == string.Empty)
                || (dropExpType.SelectedValue == string.Empty))
            {
                lblMessage.Text = "Kindly fill all the details.";
                return;
            }

            UCMFinance finOps = new UCMFinance();
            string retVal = string.Empty;

            if (DataFormatter.SafeInt(hidExpID.Value) <= 0)
            {
                finOps.AddUpdateExpense("ADD", default(int), dropExpType.SelectedValue, txtExpMasterName.Text.Trim(),  ref retVal);

                lblMessage.Text = "Expense Details Added Successfully.";
            }
            else
            {
                finOps.AddUpdateExpense("UPDATE", DataFormatter.SafeInt(hidExpID.Value), dropExpType.SelectedValue, txtExpMasterName.Text.Trim(), ref retVal);

                lblMessage.Text = "Expense Details Update Successfully.";
            }

            if (retVal != string.Empty)
                lblMessage.Text = "Expense Details Already Present.";
            else
                dropExpType_SelectedIndexChanged(sender, e);
        }


        protected void dropExpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMFinance finOps = new UCMFinance();

            txtExpMasterName.Text =string.Empty;
            butAddExpenseMaster.Text = "Add Exp. Detail";

            if (dropExpType.SelectedValue != string.Empty)
            {
                DataTable dtExpMaster = finOps.GetExpenseMasterByType(dropExpType.SelectedValue);

                if (dtExpMaster.Rows.Count > 0)
                {
                    gridExpMaster.DataSource = dtExpMaster;
                    gridExpMaster.DataBind();

                    gridExpMaster.Visible = true;
                    lblSearchResult.Text = dropExpType.SelectedItem.Text;
                }
                else
                {
                    lblSearchResult.Text = "No Entries";
                    gridExpMaster.Visible = false;
                }
            }
            else
            {
                lblSearchResult.Text = "No Entries";
                gridExpMaster.Visible = false;
            }

        }
    }
}