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
            UCMFinance finOps = new UCMFinance();

            dropHeadExpense.DataSource = finOps.GetExpenseHeads();
            dropHeadExpense.DataValueField = "ID";
            dropHeadExpense.DataTextField = "ExpName";
            dropHeadExpense.DataBind();

            dropHeadExpense.Items.Insert(0, new ListItem("- Select -", ""));
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnkButton = (LinkButton)sender;


            if (lnkButton.CommandName == "SELECT")
            {
                UCMFinance finOps = new UCMFinance();

                DataTable dtExpNature = finOps.GetExpenseByID(DataFormatter.SafeInt(lnkButton.CommandArgument));

                txtNatureOfExp.Text = dtExpNature.Rows[0]["ExpName"].ToString();
                hidExpID.Value = dtExpNature.Rows[0]["ID"].ToString();
                lblMessage.Text = string.Empty;

                butAddNatureOfExp.Text = "Update Exp. Detail";

            }

        }

        protected void butAddNatureOfExp_Click(object sender, EventArgs e)
        {
            if ((txtNatureOfExp.Text.Trim() == string.Empty)
                || (dropHeadExpense.SelectedValue == string.Empty))
            {
                lblMessage.Text = "Kindly fill all the details.";
                return;
            }

            UCMFinance finOps = new UCMFinance();
            string retVal = string.Empty;

            if (DataFormatter.SafeInt(hidExpID.Value) <= 0)
            {
                finOps.AddUpdateExpense("ADD", default(int), "NATURE", txtNatureOfExp.Text.Trim(), DataFormatter.SafeInt(dropHeadExpense.SelectedValue), ref retVal);

                lblMessage.Text = "Expense Details Added Successfully.";
            }
            else
            {
                finOps.AddUpdateExpense("UPDATE", DataFormatter.SafeInt(hidExpID.Value), "NATURE", txtNatureOfExp.Text.Trim(), DataFormatter.SafeInt(dropHeadExpense.SelectedValue), ref retVal);

                lblMessage.Text = "Expense Details Update Successfully.";
            }

            if (retVal != string.Empty)
                lblMessage.Text = "Expense Details Already Present.";
            else
                dropHeadExpense_SelectedIndexChanged(sender, e);
        }

        protected void dropHeadExpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCMFinance finOps = new UCMFinance();

            txtNatureOfExp.Text = hidExpID.Value = string.Empty;
            butAddNatureOfExp.Text = "Add Exp. Detail";

            if (dropHeadExpense.SelectedValue != string.Empty)
            {
                DataTable dtExpNature = finOps.GetExpenseNature(DataFormatter.SafeInt(dropHeadExpense.SelectedValue));

                if (dtExpNature.Rows.Count > 0)
                {
                    gridNatureOfExp.DataSource = finOps.GetExpenseNature(DataFormatter.SafeInt(dropHeadExpense.SelectedValue));
                    gridNatureOfExp.DataBind();

                    gridNatureOfExp.Visible = true;
                    lblSearchResult.Text = dropHeadExpense.SelectedItem.Text;
                }
                else
                {
                    lblSearchResult.Text = "No Entries";
                    gridNatureOfExp.Visible = false;
                }
            }
            else
            {
                lblSearchResult.Text = "No Entries";
                gridNatureOfExp.Visible = false;
            }

        }
    }
}