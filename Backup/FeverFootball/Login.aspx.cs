using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using YAF.Classes;
using YAF.Core;
using YAF.Core.Services;
using YAF.Types;
using YAF.Types.Constants;
using YAF.Types.EventProxies;
using YAF.Types.Interfaces;
using YAF.Utils;
using YAF.Utils.Helpers;
using YAF.Providers.Membership;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginUser(object sender, EventArgs e)
    {
        string userName = txtUsername.Text.Trim();
        string password = txtPassword.Text.Trim();

        YafMembershipProvider item = new YafMembershipProvider();
        
        // validate userName and password...
        try
        {
            
            //if (item.ValidateUser(userName, password))
            //{
            //    lblMessage.Text = "logged in";
            //    Session["UserID"] = FF_Classes.UserExtras.getUserIDbyUsername(userName);
            //    Session["UserName"] = userName;
            //}

            //else
            //{
            //    lblMessage.Text = "Invalid username or password.";
            //}
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
           

    }


}
