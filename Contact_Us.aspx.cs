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

public partial class Contact_Us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
            string strSQL = string.Empty;
            this.Master.TitleOne.Text = "PDs";
            this.Master.TitleTwo.Text = Session["UserNameVar"] + " Contact Us";

            if (Convert.ToInt16(Session["UserVar"]) != 0) // Not a guest
            {
                this.Master.MasterLeftColumn(true, true, true, true, false, false, false);
            }
            else  // in case the guest is able to  contact us, otherwise this is unnecessary
            {
                // emm 0311
                Session["ErrorMessage"] = "Either you have not logged in or you have not completed the registration process.";
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }


            this.Master.DisplayUserName.Text = Session["UserNameVar"].ToString();
            string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
            SqlConnection myConnection = new SqlConnection(connectionString);

            using (SqlCommand cmd = myConnection.CreateCommand())
            {
                myConnection.Open();


                try
                {



                    if (Convert.ToInt16(Session["IsCustomerRep"]) == 0)
                    {  // if
                        //reader1 = (SqlDataReader)udb.GetContactUs(Convert.ToInt16(Session["UserVar"]));
                        string sel = "GetContactUs";
                        cmd.Parameters.Add(new SqlParameter("@User_Information_ID", Convert.ToInt16(Session["UserVar"])));
                        cmd.CommandText = sel;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader1 = cmd.ExecuteReader())
                        {
                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    // Fill the form
                                    this.lblCompanyName1.Text = reader1["Company_Name"].ToString().Trim();
                                    this.lblFullName1.Text = reader1["Full_Name"].ToString().Trim() + " ";
                                    this.lblUserEmail.Text = reader1["email"].ToString().Trim();
                                }
                            }
                            reader1.Close();
                        }

                    } // if
                    else
                    {  //// beginning else


                        string sel = "GetRepContactUs";
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@Rep_ID", Convert.ToInt16(Session["IsCustomerRep"])));
                        cmd.CommandText = sel;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader1 = cmd.ExecuteReader())
                        {
                            //reader1 = (SqlDataReader)udb.GetRepContactUs(Convert.ToInt16(Session["IsCustomerRep"]));

                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    // Fill the form
                                    this.lblCompanyName1.Text = "Internal CG";
                                    this.lblFullName1.Text = "Technical Service Representative " + reader1["Full_Name"].ToString().Trim();
                                    this.lblUserEmail.Text = reader1["email"].ToString().Trim();
                                }
                            }
                            reader1.Close();
                        }


                    }  //// end else
                }
                catch (SqlException se)
                {
                    Session["ErrorMessage"] = "Error in 'Contact Us' : " + se.Message;
                    Response.Redirect("errorMessage.aspx", false);
                    return;
                }
                myConnection.Close();
            }
        }  // Close if post back
    }  // Close Page Load
    
    
    
    protected void btnSubmitComments_Click(object sender, EventArgs e)
    {
        ////  Send EMM User Commment  
        this.lblProcessing.Visible = true;
        this.lblProcessing.Text = "Your request is being processed.";

        try
        {
            string fromAddress = ConfigurationManager.AppSettings["fromAddress"];
            string fromName = ConfigurationManager.AppSettings["fromName"];
            string toAddress = this.lblUserEmail.Text;
            string CompanyLogoPath = ConfigurationManager.AppSettings["CompanyLogoPath"];
            string subject = "Contact from EMM Registered User";
            string body = "<html><head><title>User Comments</title></head><body>"
                            + "<table cellpadding=0 cellspacing=0 width=100%><tr>"
                            + "<td style='width: 196px'></td>"
                            + "<td style='vertical-align: middle; text-align: left; width: 769px;' >"
                            + "<img alt=\'\' hspace=0 src=\'cid:imageId\' align=baseline border=0>" 
                            + "</td><td style='width: 293px'></td><td style='width: 587px'></td></tr><tr>"
                            + "<td style='width: 196px'></td><td style='width: 769px ></td><td style='width: 293px'></td><td style='width: 587px'></td></tr><tr><td style='width: 196px'></td><td style='font-size: 16px; width: 769px; color: #006600; font-family: Times New Roman, Calibri, Arial'> &nbsp;</td><td style='width: 293px'></td><td style='width: 587px'></td></tr><tr><td style='width: 196px'></td><td style='width: 769px; font-size: 16px; font-family: Times New Roman, Calibri, Arial; vertical-align: middle; text-align: left;'> Comment submitted by &nbsp;"
                            + this.lblFullName1.Text.ToString().Trim()
                            + "</td><td style='width: 293px'></td><td style='width: 587px'></td></tr>"
                            + "<tr><td style='width: 196px'></td><td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial;"
                            + "text-align: left'>   of the Company:&nbsp;"
                            + this.lblCompanyName1.Text.ToString().Trim()
                            + " </td>  <td style='width: 293px'>  </td>  <td style='width: 587px'>"
                            + "</td>  </tr>  <tr>  <td style='width: 196px'>  </td>  <td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial;"
                            + "text-align: left'> At the CG PDs Site</td>  <td style='width: 293px'>  </td>  <td style='width: 587px'>  </td>"
                            + "</tr>  <tr>  <td style='width: 196px'>  </td>  <td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial; text-align: left'> </td>  <td style='width: 293px'>  </td>  <td style='width: 587px'>  </td>  </tr>  <tr>  <td style='width: 196px'>  </td>  <td style='font-size: 16px; vertical-align: middle; width: 769px; font-family: Times New Roman, Calibri, Arial; text-align: left'> Regarding:&nbsp; "
                            + this.cmbComments.SelectedItem.ToString().Trim()
                            + "</td>  <td style='width: 293px'>  </td>  <td style='width: 587px'>  </td>  </tr>  <tr><td style='width: 196px; height: 21px'></td><td colspan='2' style='height: 21px; font-size: 16px; font-family: Times New Roman, Calibri, Arial;'> </td><td style='width: 587px; height: 21px'></td></tr>  <tr>  <td style='width: 196px; height: 21px'>  </td>  <td rowspan='4' style='font-size: 16px; vertical-align: text-top; width: 769px; color: blue;"
                            + "font-family: Times New Roman, Calibri, Arial; text-align: left'> "
                            + this.txtUserComments.Text.ToString().Trim()
                            + "</td><td style='width: 293px; height: 21px '></td><td style='width: 587px; height: 21px'>  </td></tr>  <tr><td style='width: 196px; height: 21px'>  </td><td style='width: 293px; height: 21px'>  </td><td style='width: 587px; height: 21px'>  </td></tr>  <tr><td style='width: 196px; height: 21px'>  </td><td style='width: 293px; height: 21px'>  </td><td style='width: 587px; height: 21px'>  </td>  </tr>  <tr><td style='width: 196px; height: 21px'></td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr><tr><td style='width: 196px; height: 21px'></td><td style='width: 769px; height: 21px'> &nbsp;</td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr><tr><td style='width: 196px; height: 21px'></td><td style='width: 769px; height: 21px'> &nbsp;</td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr><tr><td style='width: 196px; height: 21px'></td><td rowspan='3' style='font-size: 14px; width: 769px; color: black; font-family: Times New Roman, Calibri, Arial'> "
                            + GlobalClass.Email_Disclaimer() + "</td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr><tr><td style='width: 196px; height: 21px'></td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr><tr><td style='width: 196px; height: 21px'></td><td style='width: 293px; height: 21px'></td><td style='width: 587px; height: 21px'></td></tr> </table></body></html>";

            MailMessage msg = new MailMessage();
            MailAddress fromAdd = new MailAddress(fromAddress, fromName);
            try
            {
                MailAddress toAdd = new MailAddress(toAddress);
                msg = new MailMessage(fromAdd, toAdd);
            }
            catch (FormatException formatexc)
            {
                Session["ErrorMessage"]  = "User email address is not properly formatted. With the exception: " + formatexc.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }

            msg.Body = body;
            msg.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            LinkedResource imagelink = new LinkedResource(Server.MapPath(".") + @"\docs\Company_Logo.jpg", "image/jpg");
            imagelink.ContentId = "imageId";
            imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            htmlView.LinkedResources.Add(imagelink);
            msg.AlternateViews.Add(htmlView);
            msg.Subject = subject;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            // Send the mail message
            try
            {

                mSmtpClient.Send(msg);
                mSmtpClient = null;
            }
            catch (InvalidOperationException se)
            {
                Session["ErrorMessage"]  = "Perhaps your email address is no longer valid. The application issue of {" + se.Message + "} arose.";
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }
        }
        catch (SmtpException se)
        {
            Session["ErrorMessage"]  = "Perhaps your email address is no longer valid. The application issue of {" + se.Message + "} arose.";
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }
        this.lblProcessing.Text = "Your request has been processed, please resume your activity from the left navigation panel.";
        UserInformationDB udb = new UserInformationDB();
        bool bool_retVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Contact Us",0);
        udb = null;
    }
    
  
    
}
