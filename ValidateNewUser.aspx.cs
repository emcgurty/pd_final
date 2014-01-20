using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text.RegularExpressions;

public partial class ValidateNewUser : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        HideDivs();

        if (!(Page.IsPostBack))
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1"); 
            this.Master.TitleOne.Text = "Product Compliance";
            this.Master.MasterLeftColumn(false, false, false, false, false,false, false);
            this.Master.TitleTwo.Text = "New User Registration";
            this.cmdSubmitNewUser.Text = "Validate";
            this.Master.DisplayUserName.Text = "Registering Guest";
            

        }
    }


    protected void HideDivs()
    {

        this.divPassword.Attributes.Add("display", "inline");
        this.divDuplicate.Attributes.Add("display", "inline");
        this.divemail.Attributes.Add("display", "inline"); 
        this.divDuplicate.Style["display"] = "none";
        this.divPassword.Style["display"] = "none";
        this.divemail.Style["display"] = "none";
    }

    
    
    protected void cmdSubmitNewUser_Click(object sender, EventArgs e)
    {

        HideDivs();
        
        int retVal = 0;
        bool retBool = false;
        
        string retPassword = String.Empty;
        retPassword = this.txtNewUserPassword.Text;
        
        string retPasswordCopy = String.Empty;
        retPasswordCopy = this.txtReEnter.Text;
        
        string retUserName = String.Empty;
        retUserName = this.txtNewUserName.Text;


        if (String.IsNullOrEmpty(retUserName))
        {
            this.divDuplicate.Style["display"] = "inline";
            this.lblDuplicate.Text = "Please provide a user name.";
            return;
        }
        
        if (!(String.IsNullOrEmpty(retUserName)))
        {
            if (((retUserName.Length) < 6) || ((retUserName.Length) > 12))
            {

                this.divDuplicate.Style["display"] = "inline";
                this.lblDuplicate.Text = "User name length min 6 and max 12 characters.";
                return;
            }
           
        }



        if ((String.IsNullOrEmpty(retPassword))  ||  (String.IsNullOrEmpty(retPasswordCopy)))
        {
            this.divPassword.Style["display"] = "inline";
            this.lblPassword.Text = "Please provide a password and a copy of that password.";
            return;
        }

        if (!(retPassword == retPasswordCopy))
        {
            this.divPassword.Style["display"] = "inline";
            this.lblPassword.Text = "Your password and the re-entry of that password must match.";
            return;
        }
            
        if ((retPassword.Length <6)  ||  (retPassword.Length > 12))
        {
            this.divPassword.Style["display"] = "inline";
            this.lblPassword.Text = "Password must have length 6 characters minimium and fewer than 12 characters.";
            return;
        }

        if ((!(String.IsNullOrEmpty(retUserName))) && (!(String.IsNullOrEmpty(retPassword))) && (!(String.IsNullOrEmpty(retPasswordCopy))))
                {
                    retPassword = AuthenticateUserAndPassword(); 
                    retBool = PerformValidation();

                    if (!(retBool))
                    {
                        // Create new user record
                        UserInformationDB udb = new UserInformationDB();
                        retVal = udb.InsertNewUser(this.txtNewUserName.Text.ToString().Trim(),
                                                retPassword,
                                                this.txtemail.Text.ToString().Trim(), 1);
                        Session["UserVar"] = udb.GetUser(this.txtNewUserName.Text.ToString().Trim(), retPassword);
                        Session["UserNameVar"] = this.txtNewUserName.Text.ToString().Trim();
                        if (!(retVal > 0))
                        {
                            Session["ErrorMessage"] = "There was an error in updating your User Informatio, please try again.";
                            Response.Redirect("ErrorMessage.aspx", false);
                            return;

                        }
                        else
                        {

                            retVal =  GenerateEmail(this.txtNewUserName.Text.ToString(), txtemail.Text.ToString());

                            if (!(retVal == 1))
                            {
                                Response.Redirect("ErrorMessage.aspx", false);
                                return;
                            }
                            else
                            {
                                try
                                {
                                    Response.Redirect("CheckYourEmail.aspx?uid=" + Session["UserVar"] , false);
                                    return;
                                }
                                catch (Exception exception)
                                {
                                    Session["ErrorMessage"] = "Error in Response.Redirect to CheckYourEmail.aspx" + exception.Message;
                                    Response.Redirect("ErrorMessage.aspx");
                                    return;
                                }

                            }
                        }

                    }
       }
    }

    private int GenerateEmail(string NUs, string NEm)
    {
        int retVal = 1;
        
        try
        {

            string CompanyLogoPath = ConfigurationManager.AppSettings["CompanyLogoPath"];
            string URL = ConfigurationManager.AppSettings["NewUserURL"];
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
            string fromName = ConfigurationManager.AppSettings["fromName"];
            string toAddress = @NEm;
            string subject = ConfigurationManager.AppSettings["email_subject"];
            string body = "<html><head><title>Verified Email</title></head><body>"
                          + "<table cellpadding=0 cellspacing=0 width=100%><tr>"
                          + "<td style='width: 15px'></td>"
                          + "<td style='vertical-align: middle; text-align: left; width: 769px;' >"
                          + "<img alt=\'\' hspace=0 src=\'cid:imageId\' align=baseline border=0>" 
                          + "</td> "
                          + "<td>&nbsp; </td>"
                          + "</tr>"
                          + "<tr><td style='width: 75px'></td> "
                          + "<td style='width: 769px'></td>"
                          + "<td>&nbsp; </td>"
                          + "</tr><tr>"
                          + "<td style='width: 75px'></td> "
                          + "<td style='font-size: 16px; width: 769px; color: #006600; font-family: Times New Roman, Calibri, Arial'>"
                          + "Thank you '"
                          + @NUs
                          + "' for Registering </td>"
                          + "<td>&nbsp; </td>"
                          + "</tr><tr>"
                          + "<td style='width: 15px'></td>"
                          + "<td colspan='2' style='height: 21px; font-size: 16px; font-family: Times New Roman, Calibri, Arial;'> At the CG PDs Site</td><td style='width: 587px; height: 21px'>"
                          + "</td></tr>"
                          + "<tr>"
                          + "<td colspan=3>&nbsp;"
                          + "</td></tr>"
                          + "<tr><td style='width: 15px; height: 21px;font-size: 16px; font-family: Times New Roman, Calibri, Arial;'></td>"
                          + "<td colspan='2' style='height: 21px; font-size: 16px; font-family: Times New Roman, Calibri, Arial;' >"
                          + "Now that your email address has been validated, please feel free to return to the link below where you may 'Login' with your user name and password, and then complete the registration process:</td><td style='width: 587px; height: 21px'>"
                          + "</td></tr>"
                          + "<tr>"
                          + "<td colspan=3>&nbsp;</td>"
                          + "</tr>"
                          + "<tr>"
                          + " <td style='width: 15px'></td><td style='width: 769px; height: 21px'>"
                          + "<a href=" + URL  + " >" + URL + "</a>"
                          + "</td><td>&nbsp;</td>"
                          + "</tr>"
                          + "<tr><td colspan=3>&nbsp;</td></tr>"
                          + "<tr><td >&nbsp;</td><td colspan=2 rowspan='3' style='font-size: 14px; width: 769px; color: black; font-family: Times New Roman, Calibri, Arial'>"
                          +  GlobalClass.Email_Disclaimer() + "</tr>"
                          + "</table></body></html>";

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

            try
            {
            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            // Send the mail message
            
                //  Forcing a delay
                for (int jj = 0; jj < 1000; jj++)
                { }
                mSmtpClient.Send(msg);
            }
            catch (SmtpException se)
            {
                Session["ErrorMessage"]  = "Your registration did not complete because your email address could not be processed, please try again."
                                       + "With the more technical detail of {" + se.Message + "}";
                UserInformationDB udb = new UserInformationDB();
                retVal = udb.DeleteUserInformation(Convert.ToInt16(Session["UserVar"]));
                retVal = 0;
                udb = null;
            }

        }
        catch (SmtpException se)
        {

            Session["ErrorMessage"]  = "Your registration did not complete because of an error in delivering to your email address, please try again."
                                       + "With the more technical detail of {" + se.Message + "}";
            UserInformationDB udb = new UserInformationDB();
            retVal = udb.DeleteUserInformation(Convert.ToInt16(Session["UserVar"]));
            retVal = 0;
            udb = null;

        }
        return retVal;
       
    }
      
    
    protected bool PerformValidation()
    {
        
        bool boolFoundMissing = false;
        int retVal = 0;
        HideDivs();


        UserInformationDB udb = new UserInformationDB();
        // if this come back non-zero then the user name is a duplicate
        retVal = udb.FindUserIDFromUserName(this.txtNewUserName.Text);
        udb = null;

        if (retVal > 0 )
        {
            this.divDuplicate.Style["display"] = "inline";
            this.lblDuplicate.Text = "Duplicate user name found.";
            return true;
            
        }

        /// Check that pasword and reenter are identical
       if (!(GlobalClass.IsAlphaNumeric(this.txtNewUserPassword.Text.ToString())))
         {
            this.divPassword.Style["display"] = "inline";
            this.lblPassword.Text = "A valid user password must contain one capital letter and one numeric.";
            return true;
         }

        if (!(GlobalClass.Validate_Password(this.txtNewUserPassword.Text.ToString(),this.txtReEnter.Text.ToString())))
        {
            this.divPassword.Style["display"] = "inline";
            boolFoundMissing = true;
        }

        if (String.IsNullOrEmpty(this.txtemail.Text.ToString()))
        {
            this.divemail.Style["display"] = "inline";
            boolFoundMissing = true;
        }
        else
        {
           // if (!(this.txtemail.Text.IndexOf("@") > 0))
            if (!(IsValidEmailAddress(this.txtemail.Text.ToString())))
            {
                this.divemail.Style["display"] = "inline";
                boolFoundMissing = true;
            }
        }
        
        return (boolFoundMissing);

    }


    protected int? StoredPasswordHashCode
    {
        get
        {
            if (ViewState["TypedPassword"] != null)
            {
                return Convert.ToInt32(ViewState["TypedPassword"]);
            }
            return null;
        }
        set
        {
            ViewState["TypedPassword"] = value;
            
        }
    }

    protected string AuthenticateUserAndPassword()
    {

        string newPassword = String.Empty;
        this.txtNewUserPassword.Text.GetHashCode();
        newPassword = this.txtNewUserPassword.Text;
        StoredPasswordHashCode = newPassword.GetHashCode();
        return newPassword;
    }

    public static bool IsValidEmailAddress(string sEmail)
    {
        if (sEmail == null)
        {
            return false;
        }

        int nFirstAT = sEmail.IndexOf('@');
        int nLastAT = sEmail.LastIndexOf('@');

        if ((nFirstAT > 0) && (nLastAT == nFirstAT) && (nFirstAT < (sEmail.Length - 1)))
        {
            // address is ok regarding the single @ sign
            return (Regex.IsMatch(sEmail, @"(\w+)@(\w+)\.(\w+)"));
        }
        else
        {
            return false;
        }
    }

          
}

  

        


