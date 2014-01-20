using System;
using System.Data;
using System.Configuration;


public partial class ErrorMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
        this.Master.TitleTwo.Text = "Application Event Message";

        if (!(String.IsNullOrEmpty(Request.QueryString["Expired"])))
        {

            if (Request.QueryString["Expired"].ToString() == "1")
            {
                this.lblErrorMessage.Text = "Your session has expired. Please select 'Log In' from the left panel to resume application use.";
                this.Master.Hyper_Logout.Text = "Log In";
                this.Master.MasterLeftColumn(false, false, false, true, false, false, false);
                Session.Abandon();
            }
            else if (Request.QueryString["Expired"].ToString() == "2")
            {
                this.lblErrorMessage.Text = Session["ErrorMessage"].ToString(); 
                this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                Session.Abandon();
            }
        }
        else
        {
            // LIZ HELP
            //this.lblErrorMessage.Text = Session["ErrorMessage"].ToString();
            //this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
            //Session.Abandon();
            
        }      
        
    }
}
