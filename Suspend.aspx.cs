using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Suspend : System.Web.UI.Page
{
     string strFile2;
    protected void Page_Load(object sender, EventArgs e)
    {

      
        try
        {
            string PDPath = Server.MapPath("docs");
            string[] PF_Files;

            PF_Files = Directory.GetFiles(@PDPath, Session["PDF_name"] + "1*.pdf");
            try
            {
                foreach (string strfile in PF_Files)
                { strFile2 = strfile;
                  File.Delete(strfile); }
            }
            catch(SystemException sexp)

            {
                string sysMessage = "Disregard!" + sexp.Message + " " + sexp.InnerException + "For File " + strFile2;
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + sysMessage + " ');</script>");
                // Do nothing
            }

            PF_Files = Directory.GetFiles(@PDPath, Session["PDF_name"] + "2*.pdf");
            try
            {
                foreach (string strfile in PF_Files)
                { strFile2 = strfile; 
                  File.Delete(strfile); }
            }
            catch(SystemException sexp)
            {

                string sysMessage = "Disregard!" + sexp.Message + " " + sexp.InnerException + "For File " + strFile2; ;
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + sysMessage + " ');</script>");
                // Do nothing
            }

            PF_Files = Directory.GetFiles(@PDPath, Session["PDF_name"] + "3*.pdf");
            try
            {
                foreach (string strfile in PF_Files)
                { File.Delete(strfile); }
            }
            catch
            {// Do nothing
            }

            PF_Files = Directory.GetFiles(@PDPath, Session["PDF_name"] + "4*.pdf");
            try
            {
                foreach (string strfile in PF_Files)
                { File.Delete(strfile); }
            }
            catch
            { // Do nothing
            }

            PF_Files = Directory.GetFiles(@PDPath, Session["PDF_name"] + "5*.pdf");
            try
            {
                foreach (string strfile in PF_Files)
                { File.Delete(strfile); }
            }
            catch
            { // Do nothing
            }
        }
        catch (SystemException secx)
        {
            Session["ErrorMessage"]  = "The delete of temporary files: EMMOUTPUT*.pdf under the PDs web-site failed. Please try again or delete these files manually." + secx.Message + " " + secx.StackTrace;
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }

         if (!(String.IsNullOrEmpty(Request.QueryString["Build"].ToString())))
        {

            if (Request.QueryString["Build"].ToString() == "0")
            {
                Response.Redirect("AddToQueue.aspx?build=0", false);
                return;

            }
            else if (Request.QueryString["Build"].ToString() == "1")
                {
                    Response.Redirect("ShowLetter.aspx?Product_Compliance_ID=1", false);
                    return;

                }
        }
        else if (String.IsNullOrEmpty(Request.QueryString["Build"].ToString()))
        {
            Response.Redirect("ShowLetter.aspx?Product_Compliance_ID=1", false);
            return;
        }
    }

   
}
