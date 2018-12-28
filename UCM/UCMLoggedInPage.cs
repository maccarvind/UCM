using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCMHelper;


namespace UCM
{
    public class UCMLoggedInPage : System.Web.UI.Page
    {
        public UCMUser LoggedInUser;
        public UCMConstants UCMConst;
        public UCMLoggedInPage()
        {
            
            
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
                LoggedInUser = (UCMUser)Session["USER"];
                
                if (LoggedInUser.UserID <= 0)
                    throw new Exception();

                UCMConst = (UCMConstants)Session["UCMConstants"];
                
            }
            catch (Exception ex)
            {
                Response.Redirect("/Logout.aspx?msg=Session Expired");
            }
            
            base.OnInit(e);
        }
    }
}