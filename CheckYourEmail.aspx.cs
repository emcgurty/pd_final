using System;


public partial class CheckYourEmail : System.Web.UI.Page
{
    string UID = String.Empty;
         
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!(Page.IsPostBack))
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
            string strProperName = string.Empty;
            if (!(String.IsNullOrEmpty(Request.QueryString["UID"].ToString())))
            {
                UID = Request.QueryString["UID"];
            }
            else
            {
                UID = "0";
            }

            if (!(UID == "0"))
            {
                UserInformationDB SAL = new UserInformationDB();
                strProperName = SAL.GetUserProperName(Convert.ToInt32(UID));

                if (String.IsNullOrEmpty(strProperName.Trim()))
                {
                    this.lblUser.Text = "Welcome New User: " + Session["UserNameVar"];
                }
                else
                {
                    this.lblUser.Text = strProperName;
                }
                this.lblAcknowledgeNewRegistration.Text = "Thank you for completing the first step of your registration.";
                this.btnLogout.Text = "Exit"; 
                SAL = null;
            }
            else
            {
                this.lblUser.Text = "User forgot user name or password";
                this.lblAcknowledgeNewRegistration.Text = "A new password has been delivered to your email. You are required to 'Logout' from this application.  On your return, please use the email provided password, which you may change. Otherwise retain your new password.";
                this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                this.btnLogout.Text = "Exit";

            }

            Session.Clear();
            Session.Abandon();
            Session.Timeout = 1;
        }
    }
    protected void cmdLogoutUser_Click(object sender, EventArgs e)
    {
        if (!(UID == "0"))
        {
            //Response.Redirect("Default.aspx?Is_Blocked=1");
            Session["UserNameVar"] = String.Empty;
            Session["UserVar"] = "0";
            Response.Redirect("Index.aspx", false);
            return;
            
        }
        else
        {
            //Response.Redirect("Default.aspx?ChangePassword=1");

            Session["UserNameVar"] = String.Empty;
            Session["UserVar"] = "0";
            Response.Redirect("Default.aspx", false);
            return;
        }
    }
}
