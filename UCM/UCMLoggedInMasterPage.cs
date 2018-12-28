using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCMHelper;

namespace UCM
{
    public class UCMLoggedInMasterPage : System.Web.UI.MasterPage
    {
        public UCMUser LoggedInUser;
        public UCMLoggedInMasterPage()
        {
            
            
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                LoggedInUser = (UCMUser)Session["USER"];

                if (LoggedInUser.UserID <= 0)
                    throw new Exception();
            }
            catch (Exception ex)
            {
                Response.Redirect("/Logout.aspx?msg=Session Expired");
            }
            
            base.OnInit(e);
        }
    }
    
}