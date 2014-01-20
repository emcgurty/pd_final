using System;
using System.Configuration;





public partial class Index : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Master.TitleOne.Text = "CG PD Site";
        this.Master.TitleTwo.Text = "Instructions";
        this.hyperAdobe.NavigateUrl = ConfigurationManager.AppSettings["AdobeURL"];
        Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
    }  // closes the function



    protected void btnEnterApplication_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default.aspx", false);
        }
            catch (Exception ex)
        {
            Session["ErrorMessage"]  = "Error on Response.Redirect to Default.aspx: "  + ex.Message ;
            Response.Redirect("ErrorMessage.aspx");
            return;
        }
    }
}











