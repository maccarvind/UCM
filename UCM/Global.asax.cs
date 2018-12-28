﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace UCM
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }
    }
}