using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UCMHelper;

namespace UCM
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Text = Request.QueryString.Get("msg");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty)
            {
                lblMessage.Text = "User Name and Password cannot be empty.";
                return;
            }

            UCMUserOperations userOp = new UCMUserOperations();
            DataTable dtUserData = new DataTable();

            dtUserData = userOp.UserLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (dtUserData.Rows.Count > 0)
            {

                if (txtUserName.Text.Trim() != dtUserData.Rows[0]["UserName"].ToString() ||
                    txtPassword.Text.Trim() != dtUserData.Rows[0]["UserPassword"].ToString())
                {
                    lblMessage.Text = "Invalid Login Credentials.";
                    return;
                }

                UCMUser user = new UCMUser();
                user.UserID = int.Parse(dtUserData.Rows[0]["ID"].ToString());
                user.UserName = dtUserData.Rows[0]["UserName"].ToString();
                user.DisplayName = dtUserData.Rows[0]["Name"].ToString();
                user.UserRoles = dtUserData.Rows[0]["Roles"].ToString()
                    .Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<String>();


                UCMConstants ucmConstants = new UCMConstants();
                UCMCommonOperations commonOps = new UCMCommonOperations();
                ucmConstants.FabricQuality = commonOps.GetFabricGrade();
                ucmConstants.InwardOrigin = commonOps.GetInwardOrigin();
                ucmConstants.Looms = commonOps.GetLooms();
                ucmConstants.PackageType = commonOps.GetPackageType();
                ucmConstants.FabricDefects = commonOps.GetFabricDefects();


                Session.Clear();
                Session.Add("USER", user);
                Session.Add("UCMConstants", ucmConstants);
                Response.Redirect("Index.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid Login Credentials.";
                return;
            }
        }
    }
}