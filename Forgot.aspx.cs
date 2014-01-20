using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class Forgot : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
        this.lblResponse.Visible = false;
        this.Master.TitleTwo.Text = "User forgot user name and/or password";
        
    }

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        this.lblResponse.Visible = false;
        this.lblResponse.Text = String.Empty;

        if ((String.IsNullOrEmpty(this.txtFirstName.Text.ToString())) || (String.IsNullOrEmpty(this.txtLastName.Text.ToString())) || (String.IsNullOrEmpty(this.txtemail.Text.ToString())))
        {
            this.lblResponse.Visible = true;
            this.lblResponse.Text = "You must complete all required fields.";
            return;
        }
            
        
        int retVal = 0;
        int UID = 0;
        string strSQL = string.Empty;
        UserInformationDB udb = new UserInformationDB();

        UID = udb.GetUserInfoFromFirstLastEmail(this.txtFirstName.Text.ToString(), this.txtLastName.Text.ToString(), this.txtemail.Text.ToString());

        if (UID == 0)
        {
            udb = null;
            Session["ErrorMessage"]  = "The combination of your First Name, Last Name and email address were not found on the CG database.";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }

        ClearCookies();
        string strUserName = udb.FindUserName(UID);
        if (String.IsNullOrEmpty(strUserName))
        {
            strUserName = "User name not known.";
        }
        string newPassword = "G" + Membership.GeneratePassword(10,0) + "1";
        retVal = udb.UpdateUserPassword(UID, newPassword);

        if (!(retVal > 0 ))
        {
            udb = null;
            Session["ErrorMessage"]  = "Error in Forgot.aspx, function cmdSubmit_Click(), in updating user password";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }

                try
                {
                    
                    string ReturnURL = ConfigurationManager.AppSettings["UserChangePassword"];
                    string CompanyLogoPath = ConfigurationManager.AppSettings["CompanyLogoPath"];
                    string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
                    string fromName = ConfigurationManager.AppSettings["fromName"];
                    string toAddress = this.txtemail.Text;
                    string subject = "Requested Information from CG";
                    string body = "<html><head><title>Information from CG</title></head><body><table cellpadding=0 cellspacing=0 width=100%><tr><td style='width: 24px'>&nbsp;</td>"
                       + "<td style='vertical-align: middle; text-align: left'>"
                       + "<img alt=\'\' hspace=0 src=\'cid:imageId\' align=baseline border=0>" 
                       + "</td></tr><tr><td style='width: 24px'></td><td >&nbsp;</td></tr><tr><td style='width: 24px'></td><td style='font-size: 16px; width: 769px; color: #006600; font-family: Times New Roman, Calibri, Arial'> &nbsp;</td></tr><tr><td style='width: 24px'></td>"
                       + "<td style='width: 769px; font-size: 16px; font-family: Times New Roman, Calibri, Arial; vertical-align: middle; text-align: left;'> Your user name is:  &nbsp;"
                       +  strUserName
                       + " </td></tr>"
                       + "<tr><td style='width: 24px'></td><td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial;"
                       + "text-align: left'>  Your temporary password is:&nbsp;"
                       + newPassword
                       + "</td>&nbsp<tr><td style='width: 24px'></td><td style='width: 587px'>&nbsp;"
                       + "</td></tr><tr><td style='width: 24px'></td>"
                       + "<td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial; text-align: left'>"
                       + "Please return to the application with the following link:</td>  </tr>"
                       + "<tr><td style='width: 24px; height: 21px'></td><td style='width: 293px; height: 21px'>"
                       + "&nbsp;</td></tr><tr><td style='width: 24px; height: 21px'></td><td style='width: 293px; height: 21px'>"  
                       + "<span style='color: #0000ff; text-decoration: underline'>"
                       + "<a href=" + ReturnURL + ">" + ReturnURL + "</a>" + "</span></td></tr><tr><td style='width: 24px; height: 21px'>"
                       + "</td><td style='width: 293px; height: 21px'>&nbsp;</td></tr><tr><td style='width: 24px; height: 21px'></td>"
                       + "<td rowspan='3' style='font-size: 14px; width: 769px; color: black; font-family: Times New Roman, Calibri, Arial'>"
                       + GlobalClass.Email_Disclaimer()
                       + "</td></tr><tr><td style='width: 24px; height: 21px'></td></tr><tr><td style='width: 24px; height: 21px'></td></tr> </table></body></html>";
  

                    MailAddress fromAdd = new MailAddress(fromAddress, fromName);
                    MailAddress toAdd = new MailAddress(toAddress);
                    MailMessage msg = new MailMessage(fromAdd, toAdd);
                    msg.Body = body;
                    msg.IsBodyHtml = true;
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    LinkedResource imagelink = new LinkedResource(Server.MapPath(".") + @"\docs\Company_Logo.jpg", "image/jpg");
                    imagelink.ContentId = "imageId";
                    imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                    htmlView.LinkedResources.Add(imagelink);
                    msg.AlternateViews.Add(htmlView);
                    msg.Subject = subject;
                    SmtpClient mSmtpClient = new SmtpClient();
                    try
                    {
                        mSmtpClient.Send(msg);
                        mSmtpClient = null;

                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        Session["ErrorMessage"]  = "Failure to deliver message with error: " + ex.Message;
                        Response.Redirect("ErrorMessage.aspx", false);
                        return;
 
                    }
                    Response.Redirect("CheckYourEmail.aspx?uid=0", false);
                    return;

                }
                catch (SmtpFailedRecipientsException ex)
                {
                    Session["ErrorMessage"]  = "Error in cmdSubmit_Click(sender,e): " + ex.Message;
                    Response.Redirect("ErrorMessage.aspx", false);
                    return;
                }
            }

    protected void ClearCookies()
    {
        // Clear Cookies
        HttpCookie ckp = new HttpCookie("EMMPassWordName");
        
        
        try
        {

            ckp.Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies.Add(ckp);
            ckp = new HttpCookie("EMMUserName");        
            ckp.Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies.Add(ckp);
            ckp = null;

        }

        catch
        {
            ckp = null;
            // Do nothing, cookies were just never established
        }
        


    }

   
}
    



          




