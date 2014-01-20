using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;



public partial class _Default : System.Web.UI.Page
{
    string newPassword = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        bool retVal = false;
        HttpCookie ucookie; // = Request.Cookies["EMMUserName"];
        HttpCookie pcookie; // = Request.Cookies["EMMPassWordName"];
        this.btnRegister.Visible = true;
        this.lblCurrent.Text = "Current Password:";
        
        if (!(String.IsNullOrEmpty(Request.QueryString["Logout"])))
            {
                this.Master.DisplayUserName.Text = "Welcome!";
                UserInformationDB udb = new UserInformationDB();
                retVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Log Out", 0);
                udb = null;
                ucookie = Request.Cookies["EMMUserName"];
                if ((!(ucookie == null)))
                {
                    txtUserName.Text = Request.Cookies["EMMUserName"].Value;
                }

                ucookie = null;
                pcookie = null;
                Session.Contents.RemoveAll();
                Response.Redirect("Default.aspx", false);
                return;


            }

       // Session.Contents.RemoveAll();
                
        if (!(Page.IsPostBack))
        {
            Response.AppendHeader("Refresh", Convert.ToString(Session.Timeout * 60) + "; URL=ErrorMessage.aspx?Expired=1");
            string CurrentUserName = string.Empty;
            string strSQL = string.Empty;
            
            this.Master.TitleOne.Text = "PDs";
            this.Master.TitleTwo.Text = "User Login";
            this.divNotFound.Attributes.Add("Display", "inline");
            this.divCurrentPassword.Attributes.Add("Display", "inline");
            this.divNotFound.Attributes.Add("Display", "inline");
            this.divShowPasswordChange.Attributes.Add("display", "inline");
            this.divShowPasswordChange.Style["display"] = "none";
            this.divForgot.Attributes.Add("display", "inline");
            this.divForgot.Style["display"] = "inline";
            this.divBothButtons.Attributes.Add("display", "inline");
            this.divBothButtons.Style["display"] = "inline";
            this.divShowOneButton.Attributes.Add("display", "inline");
            this.divShowOneButton.Style["display"] = "none";
            


            if (!(String.IsNullOrEmpty(Request.QueryString["Is_Blocked"])))
            {
                if (Request.QueryString["Is_Blocked"] == "1")
                {
                    this.divShowPasswordChange.Style["display"] = "none";
                    this.hyperForgot.Visible = false;
                    this.divBothButtons.Style["display"] = "none";
                    this.divShowOneButton.Style["display"] = "inline";
                    this.chkRememberMe.Visible = false;
                    Session["UserVar"] = "0";
                    Session["UserNameVar"] = String.Empty;
                    this.Master.DisplayUserName.Text = "Welcome!";
                    this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                    this.divForgot.Style["display"] = "none";
                    this.lblWelcome.Text = "You have returned to this application to complete your registration process.  Please enter your user name and password, and then click 'Complete Registration'.";
                }
            }
            else if (!(String.IsNullOrEmpty(Request.QueryString["ChangePassword"])))
            {
                if (Request.QueryString["ChangePassword"] == "1")
                {
                    this.divShowPasswordChange.Style["display"] = "inline";
                    this.chkRememberMe.Visible = false;
                    Session["UserVar"] = "0";
                    Session["UserNameVar"] = String.Empty;
                    this.Master.DisplayUserName.Text = "Welcome!";
                    this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                    this.divForgot.Style["display"] = "none";
                    this.lblWelcome.Text = "You have returned to this application login because you reported a forgotten user name or password.  Here you are required to enter the temporary password as provided in your email, and then change your password to something more familiar to you.  On successful completion you will asked to login again.";
                    this.btnRegister.Visible = false;
                    this.lblCurrent.Text = "Temporary Password:";
                }
            }
           
            else
            {   // domain
                ucookie = Request.Cookies["EMMUserName"];
                pcookie = Request.Cookies["EMMPassWordName"];

                if (((!(ucookie == null))) && (!((pcookie == null))))
                {
                    txtUserName.Text = Request.Cookies["EMMUserName"].Value;
                    //txtCurrentPassword.Text = Request.Cookies["EMMPassWordName"].Value;
                    string connectionString = ConfigurationManager.AppSettings["EMM_Connection"];
                    SqlConnection myConnection = new SqlConnection(connectionString);

                    strSQL = "GetUserInfoFromUserNameUserPassword";
                    SqlCommand sql_Cmd = new SqlCommand(strSQL);
                   
                    using (sql_Cmd = myConnection.CreateCommand())
                    {
                        sql_Cmd.CommandText = strSQL;
                        sql_Cmd.Parameters.Add(new SqlParameter("@User_Name", Request.Cookies["EMMUserName"].Value));
                        sql_Cmd.Parameters.Add(new SqlParameter("@User_Password", Request.Cookies["EMMPassWordName"].Value));
                        sql_Cmd.CommandType = CommandType.StoredProcedure;

                        myConnection.Open();
                        using (SqlDataReader reader1 = sql_Cmd.ExecuteReader())
                        {
                            if (reader1.Read())
                            {
                                CurrentUserName = reader1["Full_Name"].ToString().Trim();
                                Session["UserNameVar"] = CurrentUserName;
                                Session["UserVar"] = reader1["User_Information_ID"].ToString();
                            }
                        }

                    }
                    myConnection.Close();
                    ucookie = null;
                    pcookie = null;

                    this.Master.DisplayUserName.Text = "Welcome!" + "\r\n" + CurrentUserName;

                    if (Convert.ToInt16(Session["UserVar"]) != 0)
                    {
                        this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                    }
                    else
                    {
                        this.Master.MasterLeftColumn(false, false, false, false, false, false, false);
                    }

                    this.chkRememberMe.Visible = true;
                }
                // The user is not a cookie known user
                else
                {

                    Session["UserVar"] = "0";
                    Session["UserNameVar"] = String.Empty;
                    this.Master.DisplayUserName.Text = "Welcome!";
                    this.chkRememberMe.Visible = true;
                    this.Master.MasterLeftColumn(false, false, false, false, false, false, false);


                }
            }// close if logout

        }  // Close if postback
    
    }  // closes the function



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

    protected string AuthenticatePassword()
    {

        HttpCookie ucookie = Request.Cookies["EMMUserName"];
        HttpCookie pcookie = Request.Cookies["EMMPassWordName"];

        if (((!(ucookie == null))) && (!((pcookie == null))) && (String.IsNullOrEmpty(this.txtCurrentPassword.Text)) && (Request.Cookies["EMMUserName"].Value.Trim() == this.txtUserName.Text.ToString().Trim()))
        {

            newPassword = Request.Cookies["EMMPassWordName"].Value.Trim();
            StoredPasswordHashCode = newPassword.GetHashCode();
        }

        else
        {

            this.txtCurrentPassword.Text.GetHashCode();
            newPassword = this.txtCurrentPassword.Text.Trim();
            StoredPasswordHashCode = newPassword.GetHashCode();
        }
        pcookie = null;
        ucookie = null;
        return newPassword;
    }
    
    
    protected void cmdUserLogin_Click(object sender, EventArgs e)
    {
       int retVal = 0;
       bool boolretVal = false;
       string LoginUserName = string.Empty;
       string LoginUserPassword = string.Empty;
       UserInformationDB udb = new UserInformationDB(); 
       HttpCookie ucookie = Request.Cookies["EMMUserName"];
       AuthenticatePassword();
       HttpCookie pcookie = Request.Cookies["EMMPassWordName"];
       this.divNotFound.Style["display"] = "none";
       
       

       if (((!(ucookie == null))) && (!((pcookie == null))) && (!(String.IsNullOrEmpty(this.txtUserName.Text.ToString().Trim()))) && (Request.Cookies["EMMUserName"].Value.Trim() == this.txtUserName.Text.ToString().Trim())) 
       {
          
               LoginUserName = Request.Cookies["EMMUserName"].Value;
               LoginUserPassword = newPassword; // Request.Cookies["EMMPassWordName"].Value;
          
       } 
       else if ((String.IsNullOrEmpty(this.txtUserName.Text.ToString().Trim())) || (String.IsNullOrEmpty(this.txtCurrentPassword.Text.ToString().Trim())))
       {
            this.divNotFound.Style["display"] = "inline";
            this.lblUserName.Text = "Either your user name or password are not complete.";
            udb = null;
            ucookie = null;
            pcookie = null; 
            return;
       }
      else if ((!((String.IsNullOrEmpty(this.txtUserName.Text.ToString().Trim())))) && (! (String.IsNullOrEmpty(this.txtCurrentPassword.Text.ToString().Trim()))))
      {
                LoginUserName = this.txtUserName.Text.ToString().Trim();
                LoginUserPassword = newPassword; // AuthenticatePassword();  //this.txtCurrentPassword.Text.ToString().Trim();
             

      }

      ucookie = null;
      pcookie = null; 
      Session["UserVar"] = udb.GetUser(LoginUserName, LoginUserPassword);
      int UID = Convert.ToInt16(Session["UserVar"]);
      if (UID == 0)
      {
          this.divNotFound.Style["display"] = "inline";
          this.lblUserName.Text = "The CG database does not recognize your user name and password.";
          udb = null;
          ucookie = null;
          pcookie = null; 
          return;

      }
     
        
                 // Learn if the user is Customer Rep
                 boolretVal = udb.FindCustomerRep(LoginUserName, LoginUserPassword);
                 if (boolretVal)
                 {
                     int intDBvalue = udb.DeleteTechnicalRepQueue(UID);
                     Session["UserNameVar"] = udb.GetUserProperName(UID);
                     bool boolDBvalue = udb.InsertUserActivity(Session["UserVar"].ToString(), "Login", 0);
                     udb = null;
                     Response.Redirect("SelectCustomer.aspx", false);
                     return;
                 }
         
                 //Learn if the user if bloacked
                 boolretVal = udb.FindBlock(LoginUserName, LoginUserPassword);
                 if ((boolretVal) && (!(String.IsNullOrEmpty(Request.QueryString["Is_Blocked"]))))
                 {
                     try
                     {
                         if ((Request.QueryString["Is_Blocked"].ToString() == "1"))
                         {
                             Response.Redirect("RegisterNewUser.aspx?Status=New", false);
                             return;
                         }
                     }
                     catch
                     {
                         Session["ErrorMessage"]  = "You have not accessed this application properly.";
                         Response.Redirect("ErrorMessage.aspx", false);
                         return;
                     }
                }
                else if (((boolretVal)) && ((String.IsNullOrEmpty(Request.QueryString["Is_Blocked"])) ))
                {
                    Session["ErrorMessage"]  = "You have not accessed this application properly.";
                    Response.Redirect("ErrorMessage.aspx", false);
                    return;
                }
 
        if (!(String.IsNullOrEmpty(Request.QueryString["ChangePassword"]))  )
        {
            if (this.divShowPasswordChange.Style["display"] == "inline")
            {
                if (GlobalClass.Validate_Password(this.txtNewPassword.Text.ToString().Trim(), this.txtConfirmPassword.Text.ToString().Trim()))
                {
                    if (GlobalClass.IsAlphaNumeric(this.txtNewPassword.Text.ToString().Trim()))
                    {
                        // Change the Password
                        retVal = udb.UpdateUserPassword(UID, this.txtNewPassword.Text.ToString().Trim());

                        if (!(retVal > 0))
                        {
                            Session["ErrorMessage"]  = "Error in cmdUserLogin_Click(object sender, EventArgs e) in updating registered user password";
                            Response.Redirect("ErrorMessage.aspx", false);
                            return;
                        }
                        else
                        {
                           
                            ClearCookies();
                            Response.Redirect("Default.aspx", false);
                            return;

                        }
                    }
                }
                else
                {
                    // Do nothing, they elected to not change password
                }
            }
        }
        else
        {
           if (UID == 0)
            {
                Session["ErrorMessage"]  = "The combination of your user name and/or password were not found on the CG database, please try again.";
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }
            try
            {
                Session["UserNameVar"] = udb.GetUserProperName(UID);
                
            }
            catch (SqlException se)
            {
                Session["ErrorMessage"]  = "Could not find your name on the CG database due to error: " + se.Message;
                Response.Redirect("ErrorMessage.aspx", false);
                return;

            }  // Closes try/catch


            try
            {
                bool boolDbretVal = udb.InsertUserActivity(Session["UserVar"].ToString(), "Login", 0);
                udb = null;
                Response.Redirect("ProductSearchCriteria.aspx", false);
                return;
            }
            catch (Exception ex)
            {
                Session["ErrorMessage"]  = "Error in Response.Redirect to ProductSearchCriteria.aspx: " + ex.Message ;
                Response.Redirect("ErrorMessage.aspx", false);
                return;
            }
        }
    }// Closes function


    protected void cmdNewUserRegistration_Click(object sender, EventArgs e)
    {

        ClearCookies();
        try
        {
            Response.Redirect("ValidateNewUser.aspx", false);
            return;
        }
        catch (Exception ex)
        {
            Session["ErrorMessage"] = "Error in Response.Redirect to ValidateNewUser.aspx: " + ex.Message;
            Response.Redirect("ErrorMessage.aspx", false);
            return;
        }
    }


    protected void chkRememberMe_CheckedChanged(object sender, EventArgs e)
    {

        this.divNotFound.Style["display"] = "none";
        
        if (this.chkRememberMe.Checked)
        {
            if ((String.IsNullOrEmpty(this.txtUserName.Text.ToString().Trim())) || (String.IsNullOrEmpty(this.txtCurrentPassword.Text.ToString().Trim())))
            {
                this.divNotFound.Style["display"] = "inline";
                this.lblUserName.Text = "Your user name and password must be complete to be remembered here.";
                return;
            }

            if ((!(String.IsNullOrEmpty(this.txtUserName.Text.ToString().Trim()))) && (!(String.IsNullOrEmpty(this.txtCurrentPassword.Text.ToString().Trim()))))
            {

                int RetVal = -1;
                UserInformationDB udb = new UserInformationDB();
                RetVal = Convert.ToInt16(udb.GetUser(this.txtUserName.Text.ToString().Trim(), this.txtCurrentPassword.Text.ToString().Trim()));

                if (RetVal != 0)
                {
                    if (this.chkRememberMe.Checked)
                    {

                        HttpCookie EMMCookie = new HttpCookie("EMMUserName", this.txtUserName.Text.ToString().Trim());
                        EMMCookie.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(EMMCookie);

                        EMMCookie = new HttpCookie("EMMPassWordName", this.txtCurrentPassword.Text.ToString().Trim());
                        EMMCookie.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(EMMCookie);
                        EMMCookie = null;
                    }
                }
                else
                {
                    this.divNotFound.Style["display"] = "inline";
                    this.lblUserName.Text = "You are not a registered user to this application.";
                    return;
                }
            }
        }
    }

    protected void ClearCookies()
    {
        // Clear Cookies
        try
        {

            HttpCookie ckp = new HttpCookie("EMMPassWordName");
            ckp.Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies.Add(ckp);


            ckp = new HttpCookie("EMMUserName");
            ckp.Expires = DateTime.Now.AddSeconds(-1);
            Response.Cookies.Add(ckp);
            ckp = null;

        }

        catch
        {
            // Do nothing, cookies were just never established
        }

    }
  

}











