using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UCM
{
    public partial class ChangePassword : UCMLoggedInPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtOldPassword.Text.Trim() == string.Empty ||
                txtNewPassword.Text.Trim() == string.Empty ||
                txtRetypeNewPassword.Text.Trim() == string.Empty)
            {
                lblMessage.Text = "Kindly fill all the details.";
                return;
            }

            if (txtNewPassword.Text.Trim() != txtRetypeNewPassword.Text.Trim())
            {
                lblMessage.Text = "New Passwords doesn't match.";
                return;
            }

            UCMHelper.UCMUserOperations userOps = new UCMHelper.UCMUserOperations();

            DataTable dtUserData = new DataTable();
            dtUserData = userOps.UserLogin(LoggedInUser.UserName, txtOldPassword.Text.Trim());

            if (dtUserData.Rows.Count > 0)
            {

                if (txtOldPassword.Text.Trim() != dtUserData.Rows[0]["UserPassword"].ToString())
                {
                    lblMessage.Text = "Invalid Login Credentials.";
                    return;
                }

                userOps.UserPasswordChange(LoggedInUser.UserID, txtNewPassword.Text.Trim());

                lblMessage.Text = "Password Changed Successfully.";
            }
            else
            {
                lblMessage.Text = "Invalid Login Credentials.";
                return;
            }
        }
    }
}